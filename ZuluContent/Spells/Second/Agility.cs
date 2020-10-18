using Server.Targeting;

namespace Server.Spells.Second
{
    public class AgilitySpell : MagerySpell
    {
        public AgilitySpell(Mobile caster, Item scroll) : base(caster, scroll)
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
            else if (CheckBSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.AddStatBonus(Caster, m, StatType.Dex);

                m.FixedParticles(0x375A, 10, 15, 5010, EffectLayer.Waist);
                m.PlaySound(0x1e7);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, false) * 100);
                var length = SpellHelper.GetDuration(Caster, m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly AgilitySpell m_Owner;

            public InternalTarget(AgilitySpell owner) : base(12, false, TargetFlags.Beneficial)
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