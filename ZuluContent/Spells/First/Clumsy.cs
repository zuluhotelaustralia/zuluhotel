using Server.Targeting;

namespace Server.Spells.First
{
    public class ClumsySpell : MagerySpell
    {
        public ClumsySpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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

                SpellHelper.AddStatCurse(Caster, m, StatType.Dex);

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                m.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
                m.PlaySound(0x1DF);

                var percentage = (int) (SpellHelper.GetOffsetScalar(Caster, m, true) * 100);
                var length = SpellHelper.GetDuration(Caster, m);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly ClumsySpell m_Owner;

            public InternalTarget(ClumsySpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile mobile) m_Owner.Target(mobile);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}