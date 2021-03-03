using System;
using Server.Targeting;

namespace Server.Spells.Fifth
{
    public class ParalyzeSpell : MagerySpell
    {
        public ParalyzeSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckHarmfulSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int) Circle, Caster, ref m);

                // Algorithm: ((20% of magery) + 7) seconds [- 50% if resisted]

                var duration = 7.0 + Caster.Skills[SkillName.Magery].Value * 0.2;

                if (CheckResisted(m))
                    duration *= 0.75;

                m.Paralyze(TimeSpan.FromSeconds(duration));

                m.PlaySound(0x204);
                m.FixedEffect(0x376A, 6, 1);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        public class InternalTarget : Target
        {
            private readonly ParalyzeSpell m_Owner;

            public InternalTarget(ParalyzeSpell owner) : base(12, false, TargetFlags.Harmful)
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
    }
}