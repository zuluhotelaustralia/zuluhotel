using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class EnergyBoltSpell : MagerySpell
    {
        public EnergyBoltSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
            else if (CheckHarmfulSequence(m))
            {
                var source = Caster;

                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int) Circle, ref source, ref m);

                double damage = Utility.Random(24, 18);

                if (CheckResisted(m))
                {
                    damage *= 0.75;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                // Scale damage based on evalint and resist
                damage *= GetDamageScalar(m);

                // Do the effects
                source.MovingParticles(m, 0x379F, 7, 0, false, true, 3043, 4043, 0x211);
                source.PlaySound(0x20A);

                // Deal the damage
                SpellHelper.Damage((int) damage, m, Caster, this);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly EnergyBoltSpell m_Owner;

            public InternalTarget(EnergyBoltSpell owner) : base(12, false, TargetFlags.Harmful)
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