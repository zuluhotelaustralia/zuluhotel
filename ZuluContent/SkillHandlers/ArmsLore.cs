using System;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Skills;

namespace Server.SkillHandlers
{
    public class ArmsLore : BaseSkillHandler
    {
        public override SkillName Skill { get; } = SkillName.ArmsLore;

        private static readonly TargetOptions TargetOptions = new()
        {
            Range = 2,
            AllowNonLocal = true,
        };

        public override async Task<TimeSpan> OnUse(Mobile from)
        {
            var target = new ArmsLoreTarget(from, TargetOptions);
            from.Target = target;

            from.SendLocalizedMessage(500349); // What item do you wish to get information about?

            var (targeted, responseType) = await target;

            if (responseType != TargetResponseType.Success)
                return Delay;

            switch (targeted)
            {
                case BaseWeapon weapon when from.ShilCheckSkill(SkillName.ArmsLore):
                {
                    if (weapon.MaxHitPoints != 0)
                    {
                        var hp = (int) (weapon.HitPoints / (double) weapon.MaxHitPoints * 10);

                        hp = hp switch
                        {
                            < 0 => 0,
                            > 9 => 9,
                            _ => hp
                        };

                        from.SendLocalizedMessage(1038285 + hp);
                    }

                    var damage = (weapon.MaxDamage + weapon.MinDamage) / 2;
                    var hand = weapon.Layer == Layer.OneHanded ? 0 : 1;

                    if (damage < 3)
                        damage = 0;
                    else
                        damage = (int) Math.Ceiling(Math.Min(damage, 30) / 5.0);

                    var type = weapon.Type;

                    switch (type)
                    {
                        case WeaponType.Ranged:
                            from.SendLocalizedMessage(1038224 + damage * 9);
                            break;
                        case WeaponType.Piercing:
                            from.SendLocalizedMessage(1038218 + hand + damage * 9);
                            break;
                        case WeaponType.Slashing:
                            from.SendLocalizedMessage(1038220 + hand + damage * 9);
                            break;
                        case WeaponType.Bashing:
                            from.SendLocalizedMessage(1038222 + hand + damage * 9);
                            break;
                        default:
                            from.SendLocalizedMessage(1038216 + hand + damage * 9);
                            break;
                    }

                    if (weapon.Poison != null && weapon.PoisonCharges > 0)
                        from.SendLocalizedMessage(1038284); // It appears to have poison smeared on it.
                    break;
                }
                case BaseWeapon:
                    from.SendLocalizedMessage(500353); // You are not certain...
                    break;
                case BaseArmor armor when from.ShilCheckSkill(SkillName.ArmsLore):
                {
                    var arm = armor;

                    if (arm.MaxHitPoints != 0)
                    {
                        var hp = (int) (arm.HitPoints / (double) arm.MaxHitPoints * 10);

                        hp = hp switch
                        {
                            < 0 => 0,
                            > 9 => 9,
                            _ => hp
                        };

                        from.SendLocalizedMessage(1038285 + hp);
                    }

                    from.SendLocalizedMessage(1038295 + (int) Math.Ceiling(Math.Min(arm.ArmorRating, 35) / 5.0));

                    break;
                }
                case BaseArmor:
                    from.SendLocalizedMessage(500353); // You are not certain...
                    break;
                default:
                    from.SendLocalizedMessage(500352); // This is neither weapon nor armor.
                    break;
            }


            return TimeSpan.FromSeconds(1.0);
        }
        
        [PlayerVendorTarget] // Necessary to allow ArmsLore on items inside of player vendors
        private class ArmsLoreTarget : AsyncTarget<Item>
        {
            public ArmsLoreTarget(Mobile mobile, TargetOptions opts) : base(mobile, opts) { }
        }
    }
}