using Server.Targeting;

namespace Server.Spells.Seventh
{
    public class ManaVampireSpell : MagerySpell
    {
        public ManaVampireSpell(Mobile caster, Item scroll) : base(caster, scroll)
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

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                var toDrain = 0;

                if (CheckResisted(m))
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                else
                    toDrain = m.Mana;

                if (toDrain > Caster.ManaMax - Caster.Mana)
                    toDrain = Caster.ManaMax - Caster.Mana;

                m.Mana -= toDrain;
                Caster.Mana += toDrain;

                m.FixedParticles(0x374A, 10, 15, 5054, EffectLayer.Head);
                m.PlaySound(0x1F9);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        public override double GetResistPercent(Mobile target)
        {
            return 98.0;
        }

        private class InternalTarget : Target
        {
            private readonly ManaVampireSpell m_Owner;

            public InternalTarget(ManaVampireSpell owner) : base(12, false, TargetFlags.Harmful)
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