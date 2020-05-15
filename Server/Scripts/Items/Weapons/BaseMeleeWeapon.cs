using System;
using Server;
using Server.Mobiles;
using Server.Spells.First;

namespace Server.Items
{
    public abstract class BaseMeleeWeapon : BaseWeapon
    {
        public BaseMeleeWeapon(int itemID) : base(itemID)
        {
        }

        public BaseMeleeWeapon(Serial serial) : base(serial)
        {
        }

        public override int AbsorbDamage(Mobile attacker, Mobile defender, int damage)
        {
            damage = base.AbsorbDamage(attacker, defender, damage);

            int absorb = defender.MeleeDamageAbsorb;

            if (absorb > 0)
            {
                if (absorb > damage)
                {
                    int react = damage / 5;

                    if (defender is PlayerMobile)
                    {
                        if (((PlayerMobile)defender).Spec.SpecName == SpecName.Mage)
                        {
                            double damagebonus = (double)react * ((PlayerMobile)defender).Spec.Bonus;
                            react = (int)damagebonus;
                            //Console.WriteLine("defender is spec mage, so damagebonus is {0}", damagebonus);
                        }
                    }

                    //Console.WriteLine("Damage is {0}, and therefore react is {1}", damage, react);

                    if (react > damage)
                    {
                        react = damage;
                    }

                    defender.MeleeDamageAbsorb -= damage;
                    damage = 0;

                    //if they're using a melee weapon or a ranged weapon but are within melee range
                    if (!(this is BaseRanged) || (attacker.InRange(defender, 1)))
                    {

                        attacker.Damage(react, defender);
                    }

                    attacker.PlaySound(0x1F1);
                    attacker.FixedEffect(0x374A, 10, 16);
                }
                else
                {
                    //absorb <= damage
                    defender.MeleeDamageAbsorb = 0;
                    defender.SendLocalizedMessage(1005556); // Your reactive armor spell has been nullified.
                    DefensiveSpell.Nullify(defender, typeof(ReactiveArmorSpell));
                }
            }

            return damage;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
