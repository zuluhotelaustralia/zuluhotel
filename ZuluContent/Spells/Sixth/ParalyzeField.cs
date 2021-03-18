using System;
using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Sixth
{
    public class ParalyzeFieldSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public ParalyzeFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
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

            Effects.PlaySound(point, Caster.Map, 0x20B);

            var itemId = eastToWest ? 0x3967 : 0x3979;

            var power = 2.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref power));

            var seconds = Caster.Skills[SkillName.Magery].Value / 5.0 + 20.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref seconds));
            var duration = TimeSpan.FromSeconds(seconds);

            for (var i = -2; i <= 2; ++i)
            {
                var loc = new Point3D(eastToWest ? point.X + i : point.X, eastToWest ? point.Y : point.Y + i, point.Z);
                var canFit = SpellHelper.AdjustField(ref loc, Caster.Map, 12, false);

                if (!canFit)
                    continue;

                Item item = new ParalyzeFieldItem(Caster, itemId, loc, duration);
                item.ProcessDelta();

                Effects.SendLocationParticles(EffectItem.Create(loc, Caster.Map, EffectItem.DefaultDuration),
                    0x376A, 9, 10, 5048);
            }
        }

        [DispellableField]
        public class ParalyzeFieldItem : Item
        {
            private Mobile m_Caster;
            private DateTime m_End;
            private Timer m_Timer;

            public ParalyzeFieldItem(Mobile caster, int itemId, Point3D loc, TimeSpan duration) : base(itemId)
            {
                Visible = false;
                Movable = false;
                Light = LightType.Circle300;
                m_Caster = caster;

                MoveToWorld(loc, m_Caster.Map);

                if (caster.InLOS(this))
                    Visible = true;
                else
                    base.Delete();

                if (Deleted)
                    return;


                m_Timer = new InternalTimer(this, duration);
                m_Timer.Start();

                m_End = DateTime.Now + duration;
            }

            public ParalyzeFieldItem(Serial serial) : base(serial) { }

            public override bool BlocksFit => true;

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                m_Timer?.Stop();
            }

            public override void Serialize(IGenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write(0); // version

                writer.Write(m_Caster);
                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(IGenericReader reader)
            {
                base.Deserialize(reader);

                var version = reader.ReadInt();

                switch (version)
                {
                    case 0:
                    {
                        m_Caster = reader.ReadEntity<Mobile>();
                        m_End = reader.ReadDeltaTime();

                        m_Timer = new InternalTimer(this, m_End - DateTime.Now);
                        m_Timer.Start();

                        break;
                    }
                }
            }

            public override bool OnMoveOver(Mobile mobile)
            {
                if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, mobile) &&
                    m_Caster.CanBeHarmful(mobile, false))
                {
                    if (SpellHelper.CanRevealCaster(mobile))
                        m_Caster.RevealingAction();

                    m_Caster.DoHarmful(mobile);

                    var duration = 10.0;
                    m_Caster.FireHook(h => h.OnModifyWithMagicEfficiency(m_Caster, ref duration));

                    if (!SpellHelper.TryResist(m_Caster, mobile, SpellCircle.Fifth))
                    {
                        mobile.Paralyze(TimeSpan.FromSeconds(duration));
                        mobile.PlaySound(0x204);
                        mobile.FixedEffect(0x376A, 10, 16);
                    }
                }

                return true;
            }

            private class InternalTimer : Timer
            {
                private readonly Item m_Item;

                public InternalTimer(Item item, TimeSpan duration) : base(duration)
                {
                    Priority = TimerPriority.OneSecond;
                    m_Item = item;
                }

                protected override void OnTick()
                {
                    m_Item.Delete();
                }
            }
        }
    }
}