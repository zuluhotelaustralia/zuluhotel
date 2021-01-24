using System;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Commands;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;
using Server.Utilities;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using static Server.Utility;
using static Server.Engines.Magic.IElementalResistible;
using System.Collections.Generic;

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

        //TODO: Temp attributes

        public ElementalType ProtectionType;
        public ElementalProtectionLevel ProtectionLevel;

        public ChargeType ChargeProtectionType;
        public int Charges;

        public string Hitscript;
        public CreatureType SlayerType;

        public int BonusStr;
        public int BonusInt;
        public int BonusDex;
        public SkillName SkillBonusName;
        public int SkillBonusValue;
        public SpellEntry SpellHitEntry { get; set; } = SpellEntry.None;
        public double SpellHitChance { get; set; }
        public CreatureType CreatureProtection { get; set; }
        public EffectHitType EffectHitType { get; set; }
        public double EffectHitTypeChance { get; set; }

        public LootItem(Type t)
        {
            Type = t;
        }
        
        public bool Is<T>() => Type.IsSubclassOf(typeof(T));


        public Item Create()
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
                magicItem.Identified = false;

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
                        if (Charges > 0)
                        {
                            jewelry.Enchantments.SetChargedResist(
                                ChargeProtectionType,
                                Charges
                            );
                        }
                        else
                        {
                            jewelry.Enchantments.SetResist(
                                ProtectionType,
                                GetResistForProtectionLevel(ProtectionLevel)
                            );
                        }
                        break;
                }

                foreach (var (key, value) in magicItem.Enchantments.Values)
                {
                    value.Cursed = Cursed;
                    value.CurseLevel = CurseLevelType.Unrevealed;
                }
            }

            return item;
        }
    }


    public static class LootGenerator
    {
        public static void Initialize()
        {
            CommandSystem.Register("MakeLoot", AccessLevel.Developer, MakeLoot_OnCommand);
        }

        private static void MakeLoot_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Target a container.");
            e.Mobile.Target = new InternalTarget();
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Container container)
                    MakeLoot(container, LootTable.Table1);
            }
        }

        public static void MakeLoot(Container container, LootTable table)
        {
            var items = table.Roll()
                .Select(lootItem =>
                {
                    // Attempt to make the item magical, will do nothing if its not eligible.
                    MakeItemMagical(lootItem);
                    return lootItem.Create();
                })
                // Group the items by id and whether they're stackable
                .GroupBy(item => new { item.Stackable, item.ItemID })
                // Flattens a collection containing many collections of the same type,
                // i.e. List<List<Item>> becomes List<Item>
                .SelectMany(group =>
                {
                    // Merge the stackable items into the first instance and return that as an array to be flattened
                    if (group.Key.Stackable)
                    {
                        var item = group.First();
                        item.Amount = group.Sum(x => x.Amount);
                        return new[] { item };
                    }

                    // If this group of items is not stackable just return the entire group
                    return group.ToArray();
                });

            foreach (var item in items)
                container.AddItem(item);
        }

        public static bool MakeItemMagical(LootItem item)
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
                case not null when item.Is<BaseWeapon>():
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
                            ApplyWeaponHitScript(item);
                            break;
                    }
                    break;
                }

                case not null when item.Is<BaseShield>():
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
                        ApplyMiscSkillMod(item);
                    else
                        ApplyMiscArmorMod(item);
                    AddRandomColor(item);
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

                case not null when item.Is<BaseTool>() || item.Type == typeof(Pickaxe):
                {
                    ApplyMiscSkillMod(item);
                    break;
                }

                case not null when item.Is<BaseArmor>():
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
                            ApplyOnHitScript(item);
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
            if (Utility.Random(1, 100) <= 5)
                item.Cursed = true;
        }


        private static void ApplyMiscSkillMod(LootItem item)
        {
            var chance = Utility.Random(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
            int value = 0;


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

            value = level switch
            {
                < 200 => 1,
                < 300 => 2,
                < 400 => 3,
                < 500 => 4,
                < 600 => 5,
                _ => 6
            };

            var skill = RandomSkill();

            if (ZuluClass.ClassSkills[ZuluClassType.Mage].Contains(skill) && item.ArmorMod > 0 && RandomBool())
                item.ArmorMod = 0;

            item.SkillBonusName = skill;
            item.SkillBonusValue = value;
            item.Hue = 1109;
        }

        private static void ApplyOnHitScript(LootItem item)
        {
            var creature = Enum.GetValues(typeof(CreatureType))
                .OfType<CreatureType>()
                .ToList()
                .RandomElement();
            
            item.CreatureProtection = creature;
        }


        private static void ApplyWeaponHitScript(LootItem item)
        {
            var scriptType = Utility.Random(1, 100) + item.ItemLevel * 2;

            if (scriptType <= 50)
                ApplySpellHitscript(item);
            else if (scriptType <= 85)
                ApplySlayerHitscript(item);
            else if (scriptType <= 112)
                ApplyEffectHitscript(item);
            else if (scriptType <= 116)
            {
                if (RandomBool())
                    ApplyGreaterHitscript(item);
                else
                {
                    // TODO: GM Item
                    // DestroyItem(item);
                    // CreateFromRandomString(who, "GMWeapon");
                }
            }
            else
                ApplyEffectHitscript(item);
        }

        private static void ApplyGreaterHitscript(LootItem item)
        {
            item.EffectHitType = (EffectHitType)RandomMinMax(1, 6);
            item.EffectHitTypeChance = 1.0;
        }

        private static void ApplyEffectHitscript(LootItem item)
        {
            item.EffectHitType = (EffectHitType)RandomMinMax(7, 9);
            item.EffectHitTypeChance = 1.0;
        }

        private static void ApplySlayerHitscript(LootItem item)
        {
            var slayers = Enum.GetValues(typeof(CreatureType)).OfType<CreatureType>().ToList();

            var slayer = slayers[Utility.Random(slayers.Count)];

            item.SlayerType = slayer;
        }

        private static void ApplySpellHitscript(LootItem item)
        {

            var roll = (Utility.Random(100) + 1) * (item.ItemLevel - 3);

            SpellCircle circle = roll switch
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
                .Where(k => SpellRegistry.GetInfo(k)?.Circle == circle)
                .ToList()
                .RandomElement();
            

            var effectChance = Utility.Random(1, 10) * item.ItemLevel / (double)100;
            // var effectChancemod = hitscriptcfg[n].ChanceOfEffectMod;

            var effectChanceMod = 0.0;
            switch (circle)
            {
                case SpellCircle.First:
                case SpellCircle.Second:
                    effectChanceMod = 0.15;
                    break;
                case SpellCircle.Third:
                    effectChanceMod = 0.05;
                    break;
                case SpellCircle.Fourth:
                case SpellCircle.Fifth:
                    effectChanceMod = -0.05;
                    break;
                case SpellCircle.Sixth:
                    effectChanceMod = -0.10;
                    break;
                case SpellCircle.Seventh:
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
            var level = Utility.Random(1, 100) * item.ItemLevel;

            int amount = level switch
            {
                < 200 => 5,
                < 300 => 10,
                < 400 => 15,
                < 500 => 20,
                < 600 => 25,
                _ => 30
            };

            switch (Utility.Random(1, 3))
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

            if (Utility.Random(1, 100) <= 2 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyEnchant(LootItem item)
        {
            var level = Utility.Random(1, 100) * item.ItemLevel;

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


            if (Utility.Random(1, 100) <= 5 * item.ItemLevel)
            {
                level = Utility.Random(1, 100);

                if (level < 75)
                    ApplyMiscSkillMod(item);
                else
                    ApplyMiscArmorMod(item);
            }
        }

        private static void ApplyProtection(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
            int charges = 0;
            ChargeType chargeProtection = 0;

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

            switch (Utility.Random(1, 3))
            {
                case 1:
                    chargeProtection = ChargeType.PoisonImmunity;
                    break;
                case 2:
                    chargeProtection = ChargeType.MagicImmunity;
                    break;
                case 3:
                    chargeProtection = ChargeType.SpellReflect;
                    break;
            }

            item.Charges = charges;
            item.ChargeProtectionType = chargeProtection;
        }

        private static void ApplyImmunity(LootItem item)
        {
            var level = Utility.Random(1, 100) * (item.ItemLevel - 3);
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

            switch (Utility.Random(1, 3))
            {
                case 1:
                    element = ElementalType.PermPoisonImmunity;
                    break;
                case 2:
                    element = ElementalType.PermMagicImmunity;
                    break;
                default:
                    element = ElementalType.PermSpellReflect;
                    break;
            }

            item.ProtectionLevel = value;
            item.ProtectionType = element;
        }

        private static void ApplyElementalImmunity(LootItem item)
        {
            var level = Utility.Random(1, 100) * item.ItemLevel;
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

            switch (Utility.Random(1, 8))
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
                item.Hue = Utility.Random(1, 1184);
            } while (item.Hue > 999 && item.Hue < 1152);
        }

        private static void ApplyDamageMod(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
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

            if (Utility.Random(1, 100) <= 10 * item.ItemLevel)
            {
                if (Utility.Random(1, 100) <= 75)
                    ApplyDurabilityMod(item);
                else
                    ApplyArmorSkillMod(item);
            }

            item.DamageLevel = value;
        }

        private static void ApplyMiscArmorMod(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;

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

            if (Utility.Random(100) + 1 <= 8 * item.ItemLevel)
                ApplyMiscSkillMod(item);
        }


        private static void ApplyWeaponSkillMod(LootItem item)
        {
            var chance = Utility.Random(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = Utility.Random(1, 50) * item.ItemLevel * 2;

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
                case 3:
                case 4:
                case 5:
                    if (level < 350)
                        level = 350;
                    break;
            }

            var value = level switch
            {
                < 100 => WeaponAccuracyLevel.Regular,
                < 200 => WeaponAccuracyLevel.Accurate,
                < 350 => WeaponAccuracyLevel.Surpassingly,
                < 450 => WeaponAccuracyLevel.Eminently,
                < 550 => WeaponAccuracyLevel.Exceedingly,
                _ => WeaponAccuracyLevel.Supremely
            };

            // TODO: convert into weapon BaseWeapon.DefSkill on random bool
            item.AccuracyLevel = value;

            if (Utility.Random(1, 100) < 5 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyArmorSkillMod(LootItem item)
        {
            var chance = Utility.Random(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = Utility.Random(1, 50) * item.ItemLevel * 2;

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

            if (Utility.Random(1, 100) <= 5 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyArmorMod(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
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

            if (Utility.Random(1, 100) <= 10 * item.ItemLevel)
            {
                if (Utility.Random(1, 100) <= 75)
                    ApplyDurabilityMod(item);
                else
                    ApplyArmorSkillMod(item);
            }

            item.ArmorProtectionLevel = value;
        }

        private static void ApplyDurabilityMod(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
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
    }
}