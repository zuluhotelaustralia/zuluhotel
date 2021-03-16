using System;
using System.Collections;
using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fifth
{
    public class PoisonFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public PoisonFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

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

            bool eastToWest;

            if (rx >= 0 && ry >= 0)
                eastToWest = false;
            else if (rx >= 0)
                eastToWest = true;
            else if (ry >= 0)
                eastToWest = true;
            else
                eastToWest = false;

            Effects.PlaySound(point, Caster.Map, 0x20B);

            var itemId = eastToWest ? 0x3915 : 0x3922;

            var power = 2.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));

            var seconds = Caster.Skills[SkillName.Magery].Value / 5.0 + 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref seconds));
            var duration = TimeSpan.FromSeconds(seconds);

            for (var i = -2; i <= 2; ++i)
            {
                var loc = new Point3D(eastToWest ? point.X + i : point.X, eastToWest ? point.Y : point.Y + i, point.Z);
                var item = new PoisonFieldItem(itemId, loc, Caster, (int)power, duration, i);
            }
        }

        private static void ApplyPoison(Mobile caster, Mobile target, int power)
        {
            if (caster == null)
                return;
            
            caster.DoHarmful(target);

            var level = SpellHelper.TryResist(caster, target, SpellCircle.Fifth) 
                ? Utility.Dice(1, (uint)power, 0) 
                : power;

            
            var p = Poison.GetPoison(Math.Min(level, Poison.Poisons.Count - 1));
            target.PlaySound(0x474);
            
            if (target.ApplyPoison(caster, p) == ApplyPoisonResult.Poisoned)
                if (SpellHelper.CanRevealCaster(target))
                    caster.RevealingAction();
        }

        [DispellableField]
        public class PoisonFieldItem : Item
        {
            private Mobile m_Caster;
            private DateTime m_End;
            private Timer m_Timer;
            private int m_Power;

            public PoisonFieldItem(int itemId, Point3D loc, Mobile caster, int power, TimeSpan duration, int val) :
                base(itemId)
            {
                var canFit = SpellHelper.AdjustField(ref loc, caster.Map, 12, false);

                Visible = false;
                Movable = false;
                Light = LightType.Circle300;

                MoveToWorld(loc, caster.Map);

                m_Caster = caster;
                m_Power = power;

                m_End = DateTime.Now + duration;

                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
                m_Timer.Start();
            }

            public PoisonFieldItem(Serial serial) : base(serial)
            {
            }

            public override bool BlocksFit => true;

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                m_Timer?.Stop();
            }

            public override void Serialize(IGenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write(1); // version

                writer.Write(m_Caster);
                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(IGenericReader reader)
            {
                base.Deserialize(reader);

                var version = reader.ReadInt();

                switch (version)
                {
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
            }

            public override bool OnMoveOver(Mobile mobile)
            {
                if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, mobile) && m_Caster.CanBeHarmful(mobile, false))
                {
                    ApplyPoison(m_Caster, mobile, m_Power);
                }

                return true;
            }

            private class InternalTimer : Timer
            {
                private readonly bool m_InLos;
                private readonly bool m_CanFit;
                private readonly PoisonFieldItem m_Item;

                public InternalTimer(PoisonFieldItem item, TimeSpan delay, bool inLos, bool canFit) 
                    : base(delay, TimeSpan.FromSeconds(1.5))
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
                                10, 5040);
                        }
                    }
                    else if (DateTime.Now > m_Item.m_End)
                    {
                        m_Item.Delete();
                        Stop();
                    }
                    else
                    {
                        var map = m_Item.Map;
                        var caster = m_Item.m_Caster;

                        if (map != null && caster != null)
                        {
                            var eastToWest = m_Item.ItemID == 0x3915;
                            IPooledEnumerable eable = map.GetMobilesInBounds(
                                new Rectangle2D(
                                    m_Item.X - (eastToWest ? 0 : 1), 
                                    m_Item.Y - (eastToWest ? 1 : 0), 
                                    eastToWest ? 1 : 2, eastToWest ? 2 : 1
                                )
                            );

                            foreach (Mobile m in eable)
                            {
                                if (m.Z + 16 > m_Item.Z && m_Item.Z + 12 > m.Z &&
                                    SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
                                {
                                    caster.DoHarmful(m);

                                    ApplyPoison(m_Item.m_Caster, m, m_Item.m_Power);
                                    m.PlaySound(0x474);
                                }
                            }

                            eable.Free();
                        }
                    }
                }
            }
        }
    }
}