using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Mobiles;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public class BaseShield : BaseArmor
    {
        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public BaseShield(int itemID) : base(itemID)
        {
        }

        public BaseShield(Serial serial) : base(serial)
        {
        }

        public override double ArmorRating =>
            Parent is Mobile owner
                ? 0.5 * 0.01 * base.ArmorRating * owner.Skills.Parry.Value
                : base.ArmorRating;

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); //version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override double OnHit(BaseWeapon weapon, double damage)
        {
            if (!(Parent is Mobile owner))
                return damage;
            
            var armorRating = ArmorRating;

            if (armorRating > 0)
            {
                var parrySkillVal = owner.Skills[SkillName.Parry].Value;
                parrySkillVal *= owner.GetClassModifier(SkillName.Swords);
                
                owner.AwardSkillPoints(SkillName.Parry, 20);

                if (Utility.RandomMinMax(1, 100) <= (int) (parrySkillVal / 2))
                {
                    owner.Animate(owner.Mounted ? 28 : 19, 7, 1, true, false, 0);
                    owner.SendSuccessMessage("You successfully parry the attack.");

                    double absorbAmount;

                    if (weapon.Parent is Mobile weaponOwner && weapon.GetUsedSkill(weaponOwner) == SkillName.Archery)
                        absorbAmount = armorRating;
                    else
                        absorbAmount = armorRating / 2;

                    damage -= absorbAmount;
                    damage = Math.Max(damage, 0);
                }
            }

            base.OnHit(weapon, damage);
            
            return damage;
        }
    }
}