using Server.Targeting;

namespace Server.Spells.Fifth
{
    public class MindBlastSpell : MagerySpell
    {
        public MindBlastSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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
                Mobile from = Caster, target = m;

                SpellHelper.Turn(from, target);

                SpellHelper.CheckReflect((int) Circle, ref from, ref target);

                // Algorithm: (highestStat - lowestStat) / 2 [- 50% if resisted]

                int highestStat = target.Str, lowestStat = target.Str;

                if (target.Dex > highestStat)
                    highestStat = target.Dex;

                if (target.Dex < lowestStat)
                    lowestStat = target.Dex;

                if (target.Int > highestStat)
                    highestStat = target.Int;

                if (target.Int < lowestStat)
                    lowestStat = target.Int;

                if (highestStat > 150)
                    highestStat = 150;

                if (lowestStat > 150)
                    lowestStat = 150;

                var damage = GetDamageScalar(m) * (highestStat - lowestStat) / 4; //less damage

                if (damage > 45)
                    damage = 45;

                if (CheckResisted(target))
                {
                    damage /= 2;
                    target.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                from.FixedParticles(0x374A, 10, 15, 2038, EffectLayer.Head);

                target.FixedParticles(0x374A, 10, 15, 5038, EffectLayer.Head);
                target.PlaySound(0x213);

                SpellHelper.Damage((int) damage, target, Caster, this);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly MindBlastSpell m_Owner;

            public InternalTarget(MindBlastSpell owner) : base(12, false, TargetFlags.Harmful)
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