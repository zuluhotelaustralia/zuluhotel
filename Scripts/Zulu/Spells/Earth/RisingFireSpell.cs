using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using RunZH.Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;

namespace RunZH.Scripts.Zulu.Spells.Earth
{
    public class RisingFireSpell : AbstractEarthSpell
    {
        public override SpellInfo GetSpellInfo() => m_Info;

        private static SpellInfo m_Info = new SpellInfo(
                            "Rising Fire", "Batida Do Fogo",
                            233, 9012,
                            Reagent.BatWing, Reagent.Brimstone, Reagent.VialOfBlood
                            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0); } }

        public override double RequiredSkill { get { return 100.0; } }
        public override int RequiredMana { get { return 15; } }

        public RisingFireSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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

            double range = 3.0;
            if (Caster.Spec.SpecName == SpecName.Mage)
            {
                range *= Caster.Spec.Bonus;
            }

            double dmg = Caster.Skills[DamageSkill].Value / 6.0;
            Map map = Caster.Map;
            if (map != null)
            {
                foreach (Mobile mob in m.GetMobilesInRange((int)range))
                {
                    if (Caster != mob &&
                     SpellHelper.ValidIndirectTarget(Caster, mob) &&
                     Caster.CanBeHarmful(mob, false) &&
                     m.InLOS(mob))
                    {

                        Caster.DoHarmful(mob);
                        mob.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                        SpellHelper.Damage(this, TimeSpan.Zero, mob, Caster, dmg, ElementalType.Fire);
                    }
                }
            }

            Caster.PlaySound(0x208);

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
                m_Target.EndAction(typeof(RisingFireSpell));
            }
        }

        private class InternalTarget : Target
        {
            private RisingFireSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget(RisingFireSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    m_Owner.Target((Mobile)o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }

    }
}
