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
                if (owner.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
                    parrySkillVal *= owner.GetClassModifier(SkillName.Swords);
                
                owner.AwardSkillPoints(SkillName.Parry, 20);

                if (Utility.RandomMinMax(1, 100) <= (int) (parrySkillVal / 2))
                {
                    owner.FixedEffect(0x37B9, 10, 16);
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