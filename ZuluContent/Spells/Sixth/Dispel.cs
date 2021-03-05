using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class DispelSpell : MagerySpell
    {
        public DispelSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }
        
        public void Target(Mobile attacker, Mobile defender)
        {

            var bc = defender as BaseCreature;

            if (!attacker.CanSee(defender))
            {
                attacker.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (bc == null || !bc.IsDispellable)
            {
                attacker.SendLocalizedMessage(1005049); // That cannot be dispelled.
            }
            else if (CheckHarmfulSequence(defender))
            {
                SpellHelper.Turn(attacker, defender);

                var dispelChance =
                    (50.0 + 100 * (attacker.Skills.Magery.Value - bc.DispelDifficulty) / (bc.DispelFocus * 2)) /
                    100;

                if (dispelChance > Utility.RandomDouble())
                {
                    Effects.SendLocationParticles(
                        EffectItem.Create(defender.Location, defender.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                    Effects.PlaySound(defender, 0x201);

                    defender.Delete();
                }
                else
                {
                    defender.FixedEffect(0x3779, 10, 20);
                    attacker.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                }
            }
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public class InternalTarget : Target
        {
            private readonly DispelSpell m_Owner;

            public InternalTarget(DispelSpell owner) : base(12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile m) 
                    m_Owner.Target(from, m);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}