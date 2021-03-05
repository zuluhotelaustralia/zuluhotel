using Server.Targeting;

namespace Server.Spells.Second
{
    public class StrengthSpell : MagerySpell
    {
        public StrengthSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
            else if (CheckBeneficialSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.AddStatBonus(Caster, m, StatType.Str);

                m.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
                m.PlaySound(0x1EE);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, false) * 100);
                var length = SpellHelper.GetDuration(Caster, m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly StrengthSpell m_Owner;

            public InternalTarget(StrengthSpell owner) : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile) m_Owner.Target((Mobile) o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}