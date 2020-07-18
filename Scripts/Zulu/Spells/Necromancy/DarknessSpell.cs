using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace RunZH.Scripts.Zulu.Spells.Necromancy
{
    public class DarknessSpell : NecromancerSpell
    {
        public override SpellInfo GetSpellInfo() => m_Info;

        private static SpellInfo m_Info = new SpellInfo(
            "Darkness", "In Caligne Abditus",
            227, 9031,
            Reagent.Pumice,
            Reagent.PigIron
        );

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(2); }
        }

        public override double RequiredSkill
        {
            get { return 80.0; }
        }

        public override int RequiredMana
        {
            get { return 40; }
        }

        public DarknessSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckSequence())
            {
                goto Return;
            }

            SpellHelper.Turn(Caster, m);

            if (!m.BeginAction(typeof(DarknessSpell)))
            {
                goto Return;
            }

            int level = LightCycle.DungeonLevel;
            m.LightLevel = level;

            // TODO: Spell graphical and sound effects.
            m.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            m.PlaySound(0x1e4);
            Caster.DoHarmful(m);

            // TODO: Spell action ( buff/debuff/damage/etc. )

            new DarknessTimer(m).Start();

            Return:
            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Target;

            public InternalTimer(Mobile target, Mobile caster) : base(TimeSpan.FromSeconds(0))
            {
                m_Target = target;

                // TODO: Compute a reasonable duration, this is stolen from ArchProtection
                double time = caster.Skills[SkillName.Magery].Value * 1.2;
                if (time > 144)
                    time = 144;
                Delay = TimeSpan.FromSeconds(time);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Target.EndAction(typeof(DarknessSpell));
            }
        }

        private class InternalTarget : Target
        {
            private DarknessSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(DarknessSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile) o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }

        private class DarknessTimer : Timer
        {
            private Mobile m_Owner;

            public DarknessTimer(Mobile owner) : base(TimeSpan.FromMinutes(Utility.Random(25, 35)))
            {
                m_Owner = owner;
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Owner.EndAction(typeof(DarknessSpell));
                m_Owner.EndAction(typeof(LightCycle));
                m_Owner.LightLevel = 0;
            }
        }
    }
}