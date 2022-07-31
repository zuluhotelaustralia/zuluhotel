using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Utilities;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Utility;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Scripts.Engines.Loot
{
    public class LootItem
    {
        public readonly Type Type;
        public double ItemChance = 0.0;
        public int ItemLevel = 0;
        public double EnchantLevel = 0.0;
        public int Quantity = 1;

        public int DurabilityLevel = 0;
        public MagicalWeaponType MagicalWeaponType = MagicalWeaponType.None;
        public WeaponAccuracyLevel AccuracyLevel = 0;
        public WeaponDamageLevel DamageLevel = 0;
        public ArmorProtectionLevel ArmorProtectionLevel = 0;
        public ArmorBonusType ArmorMod = 0;
        public int Hue = 0;

        public bool Cursed = false;

        public bool Deleted = false;

        //TODO: Temp attributes

        public ElementalType ProtectionType;
        public ElementalProtectionLevel ProtectionLevel;
        public int ProtectionCharges;

        public string Hitscript;
        public CreatureType SlayerType;

        public int BonusStr;
        public int BonusInt;
        public int BonusDex;
        public SkillName SkillBonusName;
        public int SkillBonusValue;
        public int SkillBonusMultiplier;
        public SpellEntry SpellHitEntry { get; set; } = SpellEntry.None;
        public double SpellHitChance { get; set; }
        public CreatureType CreatureProtection { get; set; }
        public EffectHitType EffectHitType { get; set; }
        public double EffectHitTypeChance { get; set; }
        
        public static bool StaffRevealMagicItems { get; set; } = true;

        public LootItem(Type t)
        {
            Type = t;
        }

        public bool Is<T>() => Type.IsSubclassOf(typeof(T));


        public Item Create(Mobile? killedBy)
        {
            Item item = null;
            try
            {
                item = Type.CreateInstance<Item>();
            }
            catch (Exception e)
            {
                return null;
            }

            if (item == null)
                return null;

            if (item.Stackable)
                item.Amount = Quantity;

            if (item is IMagicItem magicItem)
            {
                if (SkillBonusValue > 0)
                {
                    magicItem.Enchantments.Set((FirstSkillBonus e) =>
                    {
                        e.Skill = SkillBonusName;
                        return e.Value = SkillBonusValue;
                    });
                }

                switch (magicItem)
                {
                    case BaseWeapon weapon:
                        weapon.DurabilityLevel = (WeaponDurabilityLevel) DurabilityLevel;
                        weapon.AccuracyLevel = AccuracyLevel;
                        weapon.DamageLevel = DamageLevel;
                        weapon.Slayer = SlayerType;

                        if (SpellHitChance > 0)
                        {
                            weapon.SpellHitEntry = SpellHitEntry;
                            weapon.SpellHitChance = SpellHitChance;
                        }

                        if (EffectHitTypeChance > 0)
                        {
                            weapon.EffectHitType = EffectHitType;
                            weapon.EffectHitTypeChance = EffectHitTypeChance;
                        }

                        weapon.MagicalWeaponType = MagicalWeaponType;
                        break;
                    case BaseArmor armor:
                        armor.DexBonus = BonusDex;
                        armor.StrBonus = BonusStr;
                        armor.IntBonus = BonusInt;

                        armor.CreatureProtection = CreatureProtection;
                        armor.ProtectionLevel = ArmorProtectionLevel;
                        armor.Durability = (ArmorDurabilityLevel) DurabilityLevel;
                        break;
                    case BaseClothing clothing:
                        item.Hue = Hue;
                        clothing.ArmorBonusType = ArmorMod;
                        break;
                    case BaseJewel jewelry:
                        jewelry.ArmorBonusType = ArmorMod;

                        if (ProtectionType > ElementalType.None)
                        {
                            if (ProtectionLevel > 0)
                                jewelry.TrySetResist(ProtectionType, ProtectionLevel);
                            else if (ProtectionCharges > 0)
                                jewelry.SetResistCharges(ProtectionType, ProtectionCharges);
                        }
                        break;
                }

                if (Cursed)
                {
                    foreach (var (_, value) in magicItem.Enchantments.Values)
                    {
                        value.Cursed = CurseType.Unrevealed;
                    }
                }

                if (StaffRevealMagicItems && killedBy?.AccessLevel >= AccessLevel.GameMaster)
                {
                    magicItem.Identified = true;
                    magicItem.Enchantments.OnIdentified(item);
                }
                else
                    magicItem.Identified = false;
            }

            return item;
        }
    }


    public static class LootGenerator
    {
        public static int MakeLoot(Mobile killedBy, Container container, LootTable table, int itemLevel, double itemChance)
        {
            var items = table.Roll(itemLevel, itemChance)
                .Select(lootItem =>
                {
                    // Attempt to make the item magical, will do nothing if its not eligible.
                    MakeItemMagical(lootItem, container);

                    if (lootItem.Deleted)
                        return null;
                    
                    return lootItem.Create(killedBy);
                })
                .Where(item => item != null)
                // Group the items by id and whether they're stackable
                .GroupBy(item => new {item.Stackable, item.ItemID})
                // Flattens a collection containing many collections of the same type,
                // i.e. List<List<Item>> becomes List<Item>
                .SelectMany(group =>
                {
                    // Merge the stackable items into the first instance and return that as an array to be flattened
                    if (group.Key.Stackable)
                    {
                        var item = group.First();
                        item.Amount = group.Sum(x => x.Amount);
                        return new[] {item};
                    }

                    // If this group of items is not stackable just return the entire group
                    return group.ToArray();
                });

            foreach (var item in items)
                container.AddItem(item);

            return items.Count();
        }

        public static bool MakeItemMagical(LootItem item, Container container)
        {
            bool isMagic = false;

            while (item.ItemLevel > 0)
            {
                if (RandomDouble() < item.ItemChance)
                {
                    isMagic = true;
                    break;
                }

                item.ItemChance += 0.1;
                item.ItemLevel -= 1;
            }

            if (isMagic)
                item.EnchantLevel = (Math.Clamp(RandomDouble(), 0, 0.75) + 0.26) * (item.ItemLevel / 2 + 1);

            if (item.ItemLevel == 5)
                item.EnchantLevel += 0.51;


            switch (item.Type)
            {
                case not null when !item.Is<IGMItem>() &&
                                   (item.Type == typeof(SmithHammer) || item.Type == typeof(Pickaxe)):
                {
                    ApplyMiscSkillMod(item, item.Type == typeof(SmithHammer) ? SkillName.Blacksmith : SkillName.Mining,
                        3);
                    break;
                }
                
                case not null when !item.Is<IGMItem>() && item.Is<BaseWeapon>() && item.Type != typeof(BaseWand):
                {
                    if (RandomDouble() < 0.2)
                        if (RandomBool())
                            item.MagicalWeaponType = MagicalWeaponType.Mystical;
                        else if (RandomBool())
                            item.MagicalWeaponType = MagicalWeaponType.Stygian;
                        else if (RandomBool())
                            item.MagicalWeaponType = MagicalWeaponType.Swift;

                    switch (item.EnchantLevel)
                    {
                        case < 0.75:
                            ApplyDurabilityMod(item);
                            break;
                        case < 1.5:
                            ApplyWeaponSkillMod(item);
                            break;
                        case < 3.5:
                            ApplyDamageMod(item);
                            break;
                        default:
                            ApplyWeaponHitScript(item, container);
                            break;
                    }

                    break;
                }

                case not null when !item.Is<IGMItem>() && item.Is<BaseShield>():
                {
                    switch (item.EnchantLevel)
                    {
                        case < 1.0:
                            ApplyDurabilityMod(item);
                            break;
                        case < 2.0:
                            ApplyArmorSkillMod(item);
                            break;
                        default:
                            ApplyArmorMod(item);
                            break;
                    }

                    break;
                }

                case not null when item.Is<BaseClothing>():
                {
                    if (item.EnchantLevel < 2.5)
                    {
                        ApplyMiscSkillMod(item);
                        AddRandomColor(item);
                    }
                    else
                        ApplyMiscArmorMod(item);
                    break;
                }

                case not null when item.Is<BaseJewel>():
                {
                    switch (item.EnchantLevel)
                    {
                        case < 2.5:
                            ApplyMiscSkillMod(item);
                            break;
                        case < 3.0:
                            ApplyEnchant(item);
                            break;
                        default:
                            ApplyMiscArmorMod(item);
                            break;
                    }

                    break;
                }

                case not null when item.Is<BaseArmor>() && !item.Is<IGMItem>():
                {
                    switch (item.EnchantLevel)
                    {
                        case < 0.75:
                            ApplyDurabilityMod(item);
                            break;
                        case < 1.5:
                            ApplyArmorSkillMod(item);
                            break;
                        case < 3.5:
                            ApplyArmorMod(item);
                            break;
                        default:
                            ApplyOnHitScript(item, container);
                            break;
                    }

                    break;
                }
            }

            ApplyCursed(item);

            return isMagic;
        }

        private static void ApplyCursed(LootItem item)
        {
            if (RandomMinMax(1, 100) <= 5)
                item.Cursed = true;
        }


        private static void ApplyMiscSkillMod(LootItem item, SkillName? skillBonus = null, int skillBonusMultiplier = 0)
        {
            var chance = RandomMinMax(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;
            var value = 0;


            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 200)
                        level = 200;
                    break;
                case 2:
                    if (level < 300)
                        level = 300;
                    break;
            }

            value = level switch
            {
                < 200 => 1,
                < 300 => 2,
                < 400 => 3,
                < 500 => 4,
                < 600 => 5,
                _ => 6
            };

            if (skillBonus != null)
            {
                item.SkillBonusName = (SkillName)skillBonus;
                item.SkillBonusValue = skillBonusMultiplier > 0 ? value * skillBonusMultiplier : value;
            }
            else
            {
                var skill = RandomSkill();
            
                if (ZuluClass.ClassSkills[ZuluClassType.Mage].Contains(skill) && item.ArmorMod > 0 && RandomBool())
                    item.ArmorMod = 0;

                item.SkillBonusName = skill;
                item.SkillBonusValue = value;
            }
        }

        private static void ApplyOnHitScript(LootItem item, Container container)
        {
            var scriptType = RandomMinMax(1, 95) + item.ItemLevel * 2;
            
            if (scriptType <= 80)
                ApplyResistantHitscript(item);
            else if (scriptType is > 100 and <= 105)
            {
                item.Deleted = true;
                var gmArmor = CreateRandomGMArmor();
                container.AddItem(gmArmor);
            }
        }


        private static void ApplyWeaponHitScript(LootItem item, Container container)
        {
            var scriptType = RandomMinMax(1, 95) + item.ItemLevel * 2;

            if (scriptType <= 40)
                ApplySpellHitscript(item);
            else if (scriptType <= 80)
                ApplySlayerHitscript(item);
            else if (scriptType <= 100)
                ApplyEffectHitscript(item);
            else if (scriptType <= 105)
            {
                item.Deleted = true;
                var gmWeapon = CreateRandomGMWeapon();
                container.AddItem(gmWeapon);
            }
            else
                ApplyGreaterHitscript(item);
        }

        private static void ApplyGreaterHitscript(LootItem item)
        {
            item.EffectHitType = (EffectHitType) RandomMinMax(6, 8);
            item.EffectHitTypeChance = 1.0;
        }

        private static void ApplyEffectHitscript(LootItem item)
        {
            item.EffectHitType = (EffectHitType) RandomMinMax(1, 5);
            item.EffectHitTypeChance = 1.0;
        }

        private static void ApplySlayerHitscript(LootItem item)
        {
            var creature = Enum.GetValues(typeof(CreatureType))
                .OfType<CreatureType>()
                .ToList()
                .RandomElement();

            item.SlayerType = creature;
        }

        private static void ApplyResistantHitscript(LootItem item)
        {
            var creature = Enum.GetValues(typeof(CreatureType))
                .OfType<CreatureType>()
                .ToList()
                .RandomElement();

            item.CreatureProtection = creature;
        }

        private static void ApplySpellHitscript(LootItem item)
        {
            var roll = RandomMinMax(1, 100) * (item.ItemLevel - 3);

            var circle = roll switch
            {
                < 50 => SpellCircle.First,
                < 100 => SpellCircle.Second,
                < 150 => SpellCircle.Third,
                < 200 => SpellCircle.Fourth,
                < 250 => SpellCircle.Fifth,
                < 300 => SpellCircle.Sixth,
                _ => SpellCircle.Seventh
            };

            var spellEntry = SpellHit.Spells.Keys
                .Skip(1)
                .Where(k => SpellRegistry.GetInfo(k)?.Circle == circle)
                .ToList()
                .RandomElement();


            var effectChance = RandomMinMax(1, 10) * item.ItemLevel / (double) 100;
            // var effectChancemod = hitscriptcfg[n].ChanceOfEffectMod;

            var effectChanceMod = 0.0;
            switch (circle)
            {
                case 1:
                case 2:
                    effectChanceMod = 0.15;
                    break;
                case 3:
                    effectChanceMod = 0.05;
                    break;
                case 4:
                case 5:
                    effectChanceMod = -0.05;
                    break;
                case 6:
                    effectChanceMod = -0.10;
                    break;
                case 7:
                    effectChanceMod = -0.15;
                    break;
            }

            if (effectChance <= 0)
                effectChance = 0.04;

            item.SpellHitEntry = spellEntry;
            item.SpellHitChance = effectChance + effectChanceMod;
        }


        private static void ApplyStatMod(LootItem item)
        {
            var level = RandomMinMax(1, 100) * item.ItemLevel;

            int amount = level switch
            {
                < 200 => 5,
                < 300 => 10,
                < 400 => 15,
                < 500 => 20,
                < 600 => 25,
                _ => 30
            };

            switch (RandomMinMax(1, 3))
            {
                case 1:
                    item.BonusStr = amount;
                    break;
                case 2:
                    item.BonusInt = amount;
                    break;
                case 3:
                    item.BonusDex = amount;
                    break;
            }

            if (RandomMinMax(1, 100) <= 2 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyEnchant(LootItem item)
        {
            var level = RandomMinMax(1, 100) * item.ItemLevel;

            if (level < 200)
            {
                ApplyProtection(item);
            }
            else if (level < 500)
                ApplyElementalImmunity(item);
            else
            {
                ApplyImmunity(item);
            }


            if (RandomMinMax(1, 100) <= 5 * item.ItemLevel)
            {
                level = RandomMinMax(1, 100);

                if (level < 75)
                    ApplyMiscSkillMod(item);
                else
                    ApplyMiscArmorMod(item);
            }
        }

        private static void ApplyProtection(LootItem item)
        {
            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;
            int charges = 0;
            ElementalType chargeProtection = 0;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 75)
                        level = 75;
                    break;
                case 2:
                    if (level < 150)
                        level = 150;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 300)
                        level = 300;
                    break;
            }

            charges = level switch
            {
                < 75 => 5,
                < 150 => 10,
                < 300 => 15,
                < 400 => 20,
                < 550 => 25,
                _ => 30
            };

            chargeProtection = RandomMinMax(1, 3) switch
            {
                1 => ElementalType.Poison,
                2 => ElementalType.MagicImmunity,
                3 => ElementalType.MagicReflection,
                _ => chargeProtection
            };

            item.ProtectionCharges = charges;
            item.ProtectionType = chargeProtection;
        }

        private static void ApplyImmunity(LootItem item)
        {
            var level = RandomMinMax(1, 100) * (item.ItemLevel - 3);
            ElementalProtectionLevel value = 0;
            ElementalType element = 0;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 170)
                        level = 170;
                    break;
                case 2:
                    if (level < 200)
                        level = 200;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 230)
                        level = 230;
                    break;
            }

            value = level switch
            {
                < 170 => ElementalProtectionLevel.Bane,
                < 200 => ElementalProtectionLevel.Warding,
                < 230 => ElementalProtectionLevel.Protection,
                < 270 => ElementalProtectionLevel.Immunity,
                < 300 => ElementalProtectionLevel.Attunement,
                _ => ElementalProtectionLevel.Absorbsion
            };

            switch (RandomMinMax(1, 3))
            {
                case 1:
                    element = ElementalType.Poison;
                    break;
                case 2:
                    element = ElementalType.MagicImmunity;
                    break;
                default:
                    element = ElementalType.MagicReflection;
                    break;
            }

            item.ProtectionLevel = value;
            item.ProtectionType = element;
        }

        private static void ApplyElementalImmunity(LootItem item)
        {
            var level = RandomMinMax(1, 100) * item.ItemLevel;
            ElementalProtectionLevel value = 0;
            ElementalType element = 0;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 150)
                        level = 150;
                    break;
                case 2:
                    if (level < 300)
                        level = 300;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 450)
                        level = 450;
                    break;
            }

            value = level switch
            {
                < 150 => ElementalProtectionLevel.Bane,
                < 300 => ElementalProtectionLevel.Warding,
                < 450 => ElementalProtectionLevel.Protection,
                < 550 => ElementalProtectionLevel.Immunity,
                < 600 => ElementalProtectionLevel.Attunement,
                _ => ElementalProtectionLevel.Absorbsion
            };

            switch (RandomMinMax(1, 8))
            {
                case 1:
                    element = ElementalType.Fire;
                    break;
                case 2:
                    element = ElementalType.Water;
                    break;
                case 3:
                    element = ElementalType.Air;
                    break;
                case 4:
                    element = ElementalType.Earth;
                    break;
                case 5:
                    element = ElementalType.Paralysis;
                    value = ElementalProtectionLevel.Bane;
                    break;
                case 6:
                    element = ElementalType.Necro;
                    break;
                case 7:
                    element = ElementalType.HealingBonus;
                    break;
                default:
                    element = ElementalType.Physical;
                    break;
            }

            item.ProtectionLevel = value;
            item.ProtectionType = element;
        }

        private static void AddRandomColor(LootItem item)
        {
            do
            {
                item.Hue = RandomMinMax(1, 1184);
            } while (item.Hue > 999 && item.Hue < 1152);
        }

        private static void ApplyDamageMod(LootItem item)
        {
            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;
            WeaponDamageLevel value = 0;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 150)
                        level = 150;
                    break;
                case 2:
                    if (level < 300)
                        level = 300;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 400)
                        level = 400;
                    break;
            }

            value = level switch
            {
                < 150 => WeaponDamageLevel.Ruin,
                < 300 => WeaponDamageLevel.Might,
                < 400 => WeaponDamageLevel.Force,
                < 500 => WeaponDamageLevel.Power,
                < 600 => WeaponDamageLevel.Vanquishing,
                _ => WeaponDamageLevel.Devastation
            };

            if (RandomMinMax(1, 100) <= 10 * item.ItemLevel)
            {
                if (RandomMinMax(1, 100) <= 75)
                    ApplyDurabilityMod(item);
                else
                    ApplyArmorSkillMod(item);
            }

            item.DamageLevel = value;
        }

        private static void ApplyMiscArmorMod(LootItem item)
        {
            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 200)
                        level = 200;
                    break;
                case 2:
                    if (level < 350)
                        level = 350;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 450)
                        level = 450;
                    break;
            }

            var value = level switch
            {
                < 200 => ArmorBonusType.Iron,
                < 350 => ArmorBonusType.Steel,
                < 450 => ArmorBonusType.MeteoricSteel,
                < 550 => ArmorBonusType.Obsidian,
                < 600 => ArmorBonusType.Onyx,
                _ => ArmorBonusType.Adamantium
            };

            item.ArmorMod = value;

            if (RandomMinMax(1, 100) <= 8 * item.ItemLevel)
                ApplyMiscSkillMod(item);
        }


        private static void ApplyWeaponSkillMod(LootItem item)
        {
            var chance = RandomMinMax(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 100)
                        level = 100;
                    break;
                case 2:
                    if (level < 200)
                        level = 200;
                    break;
            }

            var value = level switch
            {
                < 100 => WeaponAccuracyLevel.Accurate,
                < 200 => WeaponAccuracyLevel.Precisely,
                < 350 => WeaponAccuracyLevel.Surpassingly,
                < 450 => WeaponAccuracyLevel.Eminently,
                < 550 => WeaponAccuracyLevel.Exceedingly,
                _ => WeaponAccuracyLevel.Supremely
            };

            item.AccuracyLevel = value;

            if (RandomMinMax(1, 100) < 5 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyArmorSkillMod(LootItem item)
        {
            var chance = RandomMinMax(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 200)
                        level = 200;
                    break;
                case 2:
                    if (level < 300)
                        level = 300;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 400)
                        level = 400;
                    break;
            }

            var value = level switch
            {
                < 200 => 1,
                < 300 => 2,
                < 400 => 3,
                < 500 => 4,
                < 600 => 5,
                _ => 6
            };

            item.SkillBonusName = RandomBool() ? SkillName.MagicResist : SkillName.Hiding;
            item.SkillBonusValue = value;

            if (RandomMinMax(1, 100) <= 5 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyArmorMod(LootItem item)
        {
            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;
            var value = ArmorProtectionLevel.Regular;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 150)
                        level = 150;
                    break;
                case 2:
                    if (level < 300)
                        level = 300;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 400)
                        level = 400;
                    break;
            }

            value = level switch
            {
                < 150 => ArmorProtectionLevel.Defense,
                < 300 => ArmorProtectionLevel.Guarding,
                < 400 => ArmorProtectionLevel.Hardening,
                < 500 => ArmorProtectionLevel.Fortification,
                < 600 => ArmorProtectionLevel.Invulnerability,
                _ => ArmorProtectionLevel.Invincibility
            };

            if (RandomMinMax(1, 100) <= 10 * item.ItemLevel)
            {
                if (RandomMinMax(1, 100) <= 75)
                    ApplyDurabilityMod(item);
                else
                    ApplyArmorSkillMod(item);
            }

            item.ArmorProtectionLevel = value;
        }

        private static void ApplyDurabilityMod(LootItem item)
        {
            var level = RandomMinMax(1, 50) * item.ItemLevel * 2;
            int value = 0;

            switch (item.ItemLevel / 3)
            {
                case 1:
                    if (level < 75)
                        level = 75;
                    break;
                case 2:
                    if (level < 150)
                        level = 150;
                    break;
                case 3:
                case 4:
                case 5:
                    if (level < 300)
                        level = 300;
                    break;
            }

            if (level < 75)
                value = 1;
            else if (level < 150)
                value = 1;
            else if (level < 300)
                value = 2;
            else if (level < 400)
                value = 3;
            else if (level < 550)
                value = 4;
            else
                value = 5;


            item.DurabilityLevel = value;
        }

        private static Item CreateRandomGMWeapon()
        {
            var gmWeaponType = RandomList(typeof(LanceOfLothian), typeof(ScalpelOfTrevize), typeof(AnubisMaceOfDeath),
                typeof(BardicheOfLynx), typeof(DekeronsThunder), typeof(BalthazaarsChillingScimitar),
                typeof(AxeOfAnias), typeof(TjaldursStaffOfHaste), typeof(KatanaOfKieri), typeof(WarMaceOfErik),
                typeof(HalberdfOfAlcatraz), typeof(SpearOfRenah), typeof(WeaponOfZulu), typeof(OmerosPickaxe),
                typeof(XarafaxsAxe));
            return gmWeaponType.CreateInstance<Item>();
        }

        private static Item CreateRandomGMArmor()
        {
            var gmArmorType = RandomList(typeof(FemaleDrakonChest), typeof(DrakonHelm), typeof(DrakonArms),
                typeof(DrakonLegs), typeof(DrakonGloves), typeof(DrakonGorget),
                typeof(DrakonChest), typeof(FemaleRyousChest), typeof(RyousHelm), typeof(RyousArms),
                typeof(RyousLegs), typeof(RyousGloves), typeof(RyousGorget), typeof(RyousChest),
                typeof(FemaleDarknessChest), typeof(DarknessHelm), typeof(DarknessArms),
                typeof(DarknessLegs), typeof(DarknessGloves), typeof(DarknessGorget),
                typeof(DarknessChest), typeof(FemaleElvenChest), typeof(ElvenHelm),
                typeof(ElvenArms), typeof(ElvenLegs), typeof(ElvenGloves),
                typeof(ElvenGorget), typeof(ElvenChest), typeof(DrakonShield),
                typeof(RyousShield), typeof(DarknessShield), typeof(ElvenShield),
                typeof(ZephyrCoif), typeof(ZephyrLegs), typeof(ZephyrChest), typeof(InfernalChest),
                typeof(InfernalArms),
                typeof(InfernalLegs), typeof(InfernalGloves), typeof(SylvianGorget), typeof(SylvianGloves),
                typeof(SylvianArms),
                typeof(SylvianLegs), typeof(SylvianChest), typeof(FemaleSylvianChest), typeof(SageRobe),
                typeof(MagisterRobe),
                typeof(MagisterMundiRobe), typeof(CelestialRobe), typeof(PracticusRobe), typeof(ZelatorRobe));
            return gmArmorType.CreateInstance<Item>();
        }
    }
}