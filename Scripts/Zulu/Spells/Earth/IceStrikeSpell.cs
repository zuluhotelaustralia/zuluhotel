using System;
using System.Collections;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace RunZH.Scripts.Zulu.Spells.Earth
{
    public class IceStrikeSpell : AbstractEarthSpell
    {
        public override SpellInfo GetSpellInfo() => m_Info;

        private static SpellInfo m_Info = new SpellInfo(
            "Ice Strike", "Geada Com Inverno",
            233, 9012,
            Reagent.Bone, Reagent.BatWing, Reagent.Brimstone
        );

        public override TimeSpan CastDelayBase { get; } = TimeSpan.Zero;


        public override double RequiredSkill { get; } = 120.0;

        public override int RequiredMana { get; } = 20;

        public IceStrikeSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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

            double damage = Caster.Skills[DamageSkill].Value * 0.5;
            //m.Damage( (int)damage, Caster, ElementalType.Water );
            SpellHelper.Damage(this, TimeSpan.Zero, m, Caster, damage, ElementalType.Water);

            Caster.DoHarmful(m);

            m.PlaySound(0x0117);
            m.PlaySound(0x0118);
            m.FixedParticles(0x3789, 10, 30, 5052, EffectLayer.Waist);

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
                m_Target.EndAction(typeof(IceStrikeSpell));
            }
        }

        private class InternalTarget : Target
        {
            private IceStrikeSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(IceStrikeSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile mobile)
                    m_Owner.Target(mobile);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}