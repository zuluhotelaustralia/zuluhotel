using Server.Targeting;

namespace Server.Spells.First
{
    public class MagicArrowSpell : MagerySpell
    {
        public MagicArrowSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override bool DelayedDamageStacking
        {
            get { return true; }
        }

        public override bool DelayedDamage
        {
            get { return true; }
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
                var source = Caster;

                SpellHelper.Turn(source, m);

                SpellHelper.CheckReflect((int) Circle, ref source, ref m);

                double damage = Utility.Random(4, 4);

                if (CheckResisted(m))
                {
                    damage *= 0.75;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                damage *= GetDamageScalar(m);

                source.MovingParticles(m, 0x36E4, 5, 0, false, false, 3006, 0, 0);
                source.PlaySound(0x1E5);

                SpellHelper.Damage(damage, m, Caster, this);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly MagicArrowSpell m_Owner;

            public InternalTarget(MagicArrowSpell owner) : base(12, false, TargetFlags.Harmful)
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