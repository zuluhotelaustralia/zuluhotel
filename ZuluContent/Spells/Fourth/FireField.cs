using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fourth
{
    public class FireFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public FireFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);

            SpellHelper.Turn(Caster, point);

            var dx = Caster.Location.X - point.X;
            var dy = Caster.Location.Y - point.Y;
            var rx = (dx - dy) * 44;
            var ry = (dx + dy) * 44;

            var eastToWest = rx switch
            {
                >= 0 when ry >= 0 => false,
                >= 0 => true,
                _ => ry >= 0
            };

            Effects.PlaySound(point, Caster.Map, 0x20C);

            var power = 2.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));
            var damage = Utility.Dice((uint)power, 8, 0);

            var seconds = Caster.Skills[SkillName.Magery].Value / 5.0 + 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref seconds));
            var duration = TimeSpan.FromSeconds(seconds);

            for (var i = -2; i <= 2; ++i)
            {
                var loc = new Point3D(eastToWest ? point.X + i : point.X, eastToWest ? point.Y : point.Y + i, point.Z);

                Item item = new FireFieldItem(eastToWest ? 0x398C : 0x3996, loc, Caster, duration, i, damage);
                Effects.SendLocationParticles(item, 0x376A, 9, 10, 5025);
            }
        }

        private static void DamageMobile(Mobile caster, Mobile target, int damage)
        {
            if (SpellHelper.CanRevealCaster(target))
                caster.RevealingAction();

            caster.DoHarmful(target);

            if (target.ShilCheckSkill(SkillName.MagicResist, 30, 25))
            {
                damage = SpellHelper.GetDamageAfterResist(caster, target, damage);
                target.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
            }

            SpellHelper.Damage(damage, target, caster, null, null, ElementalType.Fire);
            target.PlaySound(0x208);
        }

        #region Item

        [DispellableField]
        public class FireFieldItem : Item
        {
            private Mobile m_Caster;
            private int m_Damage;
            private DateTime m_End;
            private Timer m_Timer;

            public FireFieldItem(int itemId, Point3D loc, Mobile caster, TimeSpan duration, int val, int damage) 
                : base(itemId)
            {
                var canFit = SpellHelper.AdjustField(ref loc, caster.Map, 12, false);

                Visible = false;
                Movable = false;
                Light = LightType.Circle300;

                MoveToWorld(loc, caster.Map);

                m_Caster = caster;
                m_Damage = damage;
                m_End = DateTime.Now + duration;
                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
                m_Timer.Start();
            }

            public FireFieldItem(Serial serial) : base(serial) { }

            public override bool BlocksFit => true;

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();
                m_Timer?.Stop();
            }

            public override void Serialize(IGenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write(2); // version

                writer.Write(m_Damage);
                writer.Write(m_Caster);
                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(IGenericReader reader)
            {
                base.Deserialize(reader);

                var version = reader.ReadInt();

                switch (version)
                {
                    case 2:
                    {
                        m_Damage = reader.ReadInt();
                        goto case 1;
                    }
                    case 1:
                    {
                        m_Caster = reader.ReadEntity<Mobile>();

                        goto case 0;
                    }
                    case 0:
                    {
                        m_End = reader.ReadDeltaTime();

                        m_Timer = new InternalTimer(this, TimeSpan.Zero, true, true);
                        m_Timer.Start();

                        break;
                    }
                }

                if (version < 2)
                    m_Damage = 2;
            }

            public override bool OnMoveOver(Mobile mobile)
            {
                if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, mobile) &&
                    m_Caster.CanBeHarmful(mobile, false))
                {
                    DamageMobile(m_Caster, mobile, m_Damage);
                }

                return true;
            }

            private class InternalTimer : Timer
            {
                private readonly bool m_InLos;
                private readonly bool m_CanFit;
                private readonly FireFieldItem m_Item;

                public InternalTimer(FireFieldItem item, TimeSpan delay, bool inLos, bool canFit)
                    : base(delay, TimeSpan.FromSeconds(1.0))
                {
                    m_Item = item;
                    m_InLos = inLos;
                    m_CanFit = canFit;

                    Priority = TimerPriority.FiftyMS;
                }

                protected override void OnTick()
                {
                    if (m_Item.Deleted)
                        return;

                    if (!m_Item.Visible)
                    {
                        if (m_InLos && m_CanFit)
                            m_Item.Visible = true;
                        else
                            m_Item.Delete();

                        if (!m_Item.Deleted)
                        {
                            m_Item.ProcessDelta();
                            Effects.SendLocationParticles(
                                EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), 0x376A, 9,
                                10, 5029);
                        }

                        return;
                    }

                    if (DateTime.Now > m_Item.m_End)
                    {
                        m_Item.Delete();
                        Stop();
                        return;
                    }

                    var caster = m_Item.m_Caster;

                    if (caster == null)
                        return;

                    var eable = m_Item.GetMobilesInRange(0);
                    
                    var mobiles = eable.Where(mobile => 
                            mobile.Z + 16 > m_Item.Z
                            && m_Item.Z + 12 > mobile.Z
                            && SpellHelper.ValidIndirectTarget(caster, mobile)
                            && caster.CanBeHarmful(mobile, false)
                    ).ToList();
                    
                    eable.Free();

                    foreach (var mobile in mobiles)
                    {
                        DamageMobile(caster, mobile, m_Item.m_Damage);
                    }
                }
            }
        }

        #endregion
    }
}