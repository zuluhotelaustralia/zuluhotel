using System;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
    public class WraithFormSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                            "Wraith Form", "Manes Sollicti Mihi Infundite",
                            227, 9031,
                            Reagent.DaemonBone, Reagent.Brimstone, Reagent.Bloodspawn
                            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(2); } }

        public override double RequiredSkill { get { return 120.0; } }
        public override int RequiredMana { get { return 100; } }

        private static Hashtable m_Timers = new Hashtable();
        public static Hashtable Timers { get { return m_Timers; } }

        public WraithFormSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (TransformationSpellHelper.UnderTransformation(Caster))
            {
                Caster.SendLocalizedMessage(1061633); // You cannot polymorph while in that form.
                return;
            }
            else if (DisguiseTimers.IsDisguised(Caster))
            {
                Caster.SendLocalizedMessage(502167); // You cannot polymorph while disguised.
                return;
            }
            else if (Caster.BodyMod == 183 || Caster.BodyMod == 184)
            {
                Caster.SendLocalizedMessage(1042512); // You cannot polymorph while wearing body paint
                return;
            }

            if (!CheckSequence())
            {
                goto Return;
            }

            if (!Caster.BeginAction(typeof(WraithFormSpell)))
            {
                Caster.SendLocalizedMessage(1005559); //this spell already in effect
                goto Return;
            }

            if (Caster is PlayerMobile)
            {
                Caster.HueMod = 0x482;
                Caster.BodyMod = 0x1a;
            }
            Caster.PlaySound(0x210);

            int interval = 0;
            double intermediate = 150.0 / Caster.Skills[CastSkill].Value;
            if (intermediate < 2)
            {
                interval = 4;
            }
            else
            {
                // should proc damage no faster than every 4 seconds
                interval = (int)intermediate;
            }

            //e.g. 130 seconds or whatever
            int duration = (int)Caster.Skills[DamageSkill].Value;

            double dmg = Utility.Dice(2, (int)(Caster.Skills[DamageSkill].Value / 20.0), 0);

            Timer t = new WraithFormTimer(Caster, TimeSpan.FromSeconds(interval), duration, (int)dmg);
            m_Timers[Caster] = t;
            t.Start();

        Return:
            FinishSequence();
        }

        public static void EndWraithForm(Mobile m)
        {
            if (!m.CanBeginAction(typeof(WraithFormSpell)))
            {
                m.BodyMod = 0;
                m.HueMod = -1;
                m.EndAction(typeof(WraithFormSpell));

                BaseArmor.ValidateMobile(m);
                BaseClothing.ValidateMobile(m);
            }

            StopTimers(m);
        }

        public static bool StopTimers(Mobile m)
        {
            WraithFormTimer t = m_Timers[m] as WraithFormTimer;

            if (t != null)
            {
                t.StopSubtimer();
                t.Stop();
                m_Timers.Remove(m);
            }
            return (t != null);
        }

        // this one lasts for the duration of the transformation, i.e. this is your polymorph
        private class WraithFormTimer : Timer
        {
            private Mobile m_Owner;
            private int m_Damage;
            private TimeSpan m_SubtimerDuration;
            private Subtimer m_Subtimer;

            public WraithFormTimer(Mobile owner, TimeSpan interval, int duration, int damage) :
            base(TimeSpan.FromSeconds(duration))
            {
                m_Owner = owner;
                Priority = TimerPriority.OneSecond;
                m_Damage = damage;
                m_SubtimerDuration = interval;
                m_Subtimer = new Subtimer(interval, this, owner, damage);
                m_Subtimer.Start();
            }

            protected override void OnTick()
            {
                m_Subtimer.Stop();
                EndWraithForm(m_Owner);
            }

            public void StopSubtimer()
            {
                m_Subtimer.Stop();
            }

            public void SubtimerCallback()
            {
                m_Subtimer.Stop();
                m_Subtimer = new Subtimer(m_SubtimerDuration, this, m_Owner, m_Damage);
                m_Subtimer.Start();
            }
        }

        // and this one actually procs your AOE necro damage
        private class Subtimer : Timer
        {
            private WraithFormTimer m_ParentTimer;
            private Mobile m_Caster;
            private int m_Damage;

            public Subtimer(TimeSpan duration, WraithFormTimer parent, Mobile caster, int dmg) : base(duration)
            {
                m_ParentTimer = parent;
                m_Caster = caster;
                m_Damage = dmg;
            }

            protected override void OnTick()
            {

                List<Mobile> targets = new List<Mobile>();
                Map map = m_Caster.Map;

                //build a target list
                if (map != null)
                {
                    foreach (Mobile m in m_Caster.GetMobilesInRange(1 + (int)(m_Caster.Skills.SpiritSpeak.Value / 15.0)))
                    {
                        if (m_Caster != m &&
                             SpellHelper.ValidIndirectTarget(m_Caster, m) &&
                             m_Caster.CanBeHarmful(m, false) &&
                             m_Caster.InLOS(m)
                             )
                        {
                            targets.Add(m);
                        }
                    }
                }

                // yeet on em
                foreach (Mobile m in targets)
                {
                    m_Caster.DoHarmful(m);

                    m.FixedParticles(0x374a, 10, 30, 5052, EffectLayer.LeftFoot);
                    m.PlaySound(0x1fa);
                    //m.Damage( m_Damage, m_Caster, DamageType.Necro );  //about 12 at spec 4
                    SpellHelper.Damage(null, TimeSpan.Zero, m, m_Caster, m_Damage, DamageType.Necro);

                    if ((m_Caster.Mana + m_Damage) > m_Caster.Int)
                    {
                        m_Caster.Mana = m_Caster.Int;
                    }
                    else
                    {
                        m_Caster.Mana += m_Damage;
                    }
                }

                m_ParentTimer.SubtimerCallback();
            }
        }
    }
}
