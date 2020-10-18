using Server.Targeting;

namespace Server.Spells.Fourth
{
    public class LightningSpell : MagerySpell
    {
        public LightningSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override bool DelayedDamage
        {
            get { return false; }
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

                double damage = Utility.Random(12, 9);

                if (CheckResisted(m))
                {
                    damage *= 0.75;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                damage *= GetDamageScalar(m);

                m.BoltEffect(0);

                SpellHelper.Damage(damage, m, Caster, this);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly LightningSpell m_Owner;

            public InternalTarget(LightningSpell owner) : base(12, false, TargetFlags.Harmful)
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