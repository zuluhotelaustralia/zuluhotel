using Server.Targeting;

namespace Server.Spells.Second
{
    public class CunningSpell : MagerySpell
    {
        public CunningSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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

                SpellHelper.AddStatBonus(Caster, m, StatType.Int);

                m.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
                m.PlaySound(0x1EB);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, false) * 100);
                var length = SpellHelper.GetDuration(Caster, m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly CunningSpell m_Owner;

            public InternalTarget(CunningSpell owner) : base(12, false, TargetFlags.Beneficial)
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