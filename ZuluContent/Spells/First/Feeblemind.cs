using Server.Targeting;

namespace Server.Spells.First
{
    public class FeeblemindSpell : MagerySpell
    {
        public FeeblemindSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int) Circle, Caster, ref m);

                SpellHelper.AddStatCurse(Caster, m, StatType.Int);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                m.FixedParticles(0x3779, 10, 15, 5004, EffectLayer.Head);
                m.PlaySound(0x1E4);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, true) * 100);
                var length = SpellHelper.GetDuration(Caster, m);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly FeeblemindSpell m_Owner;

            public InternalTarget(FeeblemindSpell owner) : base(12, false, TargetFlags.Harmful)
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