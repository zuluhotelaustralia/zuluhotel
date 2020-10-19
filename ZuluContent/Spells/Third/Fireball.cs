using Server.Targeting;

namespace Server.Spells.Third
{
    public class FireballSpell : MagerySpell
    {
        public FireballSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
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

                double damage = Utility.Random(10, 7);

                if (CheckResisted(m))
                {
                    damage *= 0.75;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                damage *= GetDamageScalar(m);

                source.MovingParticles(m, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160);
                source.PlaySound(0x44B);

                SpellHelper.Damage(damage, m, Caster, this);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly FireballSpell m_Owner;

            public InternalTarget(FireballSpell owner) : base(12, false, TargetFlags.Harmful)
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