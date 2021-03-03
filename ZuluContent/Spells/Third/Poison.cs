using Server.Targeting;

namespace Server.Spells.Third
{
    public class PoisonSpell : MagerySpell
    {
        public PoisonSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
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

                if (m.Spell != null)
                    m.Spell.OnCasterHurt();

                m.Paralyzed = false;

                if (CheckResisted(m))
                {
                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }
                else
                {
                    int level;

                    var total = Caster.Skills[SkillName.Magery].Value;

                    var dist = Caster.GetDistanceToSqrt(m);

                    if (dist >= 3.0)
                        total -= (dist - 3.0) * 10.0;

                    if (total >= 200.0 && 1 > Utility.Random(10))
                        level = 3;
                    else if (total > 170.0)
                        level = 2;
                    else if (total > 130.0)
                        level = 1;
                    else
                        level = 0;

                    m.ApplyPoison(Caster, Poison.GetPoison(level));
                }

                m.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
                m.PlaySound(0x205);

                HarmfulSpell(m);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly PoisonSpell m_Owner;

            public InternalTarget(PoisonSpell owner) : base(12, false, TargetFlags.Harmful)
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