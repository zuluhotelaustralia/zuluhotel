using System;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class ParalyzeFieldSpell : MagerySpell
    {
        public ParalyzeFieldSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(IPoint3D p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (SpellHelper.CheckTown(p, Caster) && CheckSequence())
            {
                SpellHelper.Turn(Caster, p);

                SpellHelper.GetSurfaceTop(ref p);

                var dx = Caster.Location.X - p.X;
                var dy = Caster.Location.Y - p.Y;
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

                Effects.PlaySound((Point3D)p, Caster.Map, 0x20B);

                var itemID = eastToWest ? 0x3967 : 0x3979;

                var duration = TimeSpan.FromSeconds(3.0 + Caster.Skills[SkillName.Magery].Value / 3.0);

                for (var i = -2; i <= 2; ++i)
                {
                    var loc = new Point3D(eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z);
                    var canFit = SpellHelper.AdjustField(ref loc, Caster.Map, 12, false);

                    if (!canFit)
                        continue;

                    Item item = new InternalItem(Caster, itemID, loc, Caster.Map, duration);
                    item.ProcessDelta();

                    Effects.SendLocationParticles(EffectItem.Create(loc, Caster.Map, EffectItem.DefaultDuration),
                        0x376A, 9, 10, 5048);
                }
            }

            FinishSequence();
        }

        [DispellableField]
        public class InternalItem : Item
        {
            private Mobile m_Caster;
            private DateTime m_End;
            private Timer m_Timer;

            public InternalItem(Mobile caster, int itemID, Point3D loc, Map map, TimeSpan duration) : base(itemID)
            {
                Visible = false;
                Movable = false;
                Light = LightType.Circle300;

                MoveToWorld(loc, map);

                if (caster.InLOS(this))
                    Visible = true;
                else
                    Delete();

                if (Deleted)
                    return;

                m_Caster = caster;

                m_Timer = new InternalTimer(this, duration);
                m_Timer.Start();

                m_End = DateTime.Now + duration;
            }

            public InternalItem(Serial serial) : base(serial)
            {
            }

            public override bool BlocksFit
            {
                get { return true; }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
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

            public override bool OnMoveOver(Mobile m)
            {
                if (Visible && m_Caster != null && SpellHelper.ValidIndirectTarget(m_Caster, m) &&
                    m_Caster.CanBeHarmful(m, false))
                {
                    if (SpellHelper.CanRevealCaster(m))
                        m_Caster.RevealingAction();

                    m_Caster.DoHarmful(m);

                    var duration = 7.0 + m_Caster.Skills[SkillName.Magery].Value * 0.2;

                    m.Paralyze(TimeSpan.FromSeconds(duration));

                    m.PlaySound(0x204);
                    m.FixedEffect(0x376A, 10, 16);

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

        private class InternalTarget : Target
        {
            private readonly ParalyzeFieldSpell m_Owner;

            public InternalTarget(ParalyzeFieldSpell owner) : base(12, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IPoint3D)
                    m_Owner.Target((IPoint3D) o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}