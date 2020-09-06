using System;
using System.Linq;
using Scripts.Engines.Magic;
using Scripts.Zulu.Engines.Classes;
using Server.Commands;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using static Server.Utility;

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
        public int AccuracyLevel = 0;
        public int DamageLevel = 0;
        public int ArmorMod = 0;
        public int Color = 0;

        //TODO: Temp attributes

        public ElementalType ProtectionType;
        public int ProtectionLevel;

        public string Hitscript;
        public SlayerName SlayerName;

        public int BonusStr;
        public int BonusInt;
        public int BonusDex;
        public SkillName SkillBonusName;
        public int SkillBonusValue;


        public LootItem(Type t)
        {
            Type = t;
        }

        public bool Is<T>() => Type.IsSubclassOf(typeof(T));


        public bool CreateIn(Container c)
        {
            Item item = null;
            try
            {
                item = (Item) Activator.CreateInstance(Type, null);
            }
            catch (Exception e)
            {
                return false;
            }

            if (item == null)
                return false;

            if (item.Stackable)
                item.Amount = Quantity;

            item.Hue = Color;


            if (item is IMagicEquipItem magicItem)
            {
                magicItem.DexBonus = BonusDex;
                magicItem.StrBonus = BonusStr;
                magicItem.IntBonus = BonusInt;

                if (SkillBonusValue > 0)
                    magicItem.MagicProps.AddMod(new MagicSkillMod(SkillBonusName, SkillBonusValue));
            }

            switch (item)
            {
                case BaseWeapon weapon:
                    weapon.DurabilityLevel = (WeaponDurabilityLevel) DurabilityLevel;
                    weapon.AccuracyLevel = (WeaponAccuracyLevel) AccuracyLevel;
                    weapon.DamageLevel = (WeaponDamageLevel) DamageLevel;
                    weapon.Slayer = SlayerName;
                    

                    break;
                case BaseArmor armor:

                    armor.Durability = (ArmorDurabilityLevel) DurabilityLevel;
                    break;
                case BaseClothing clothing:
                    
                    // clothing.VirtualArmorMod = ArmorMod;
                    // clothing.Prot = new Prot(ProtectionType, ProtectionLevel);

                    break;
                case BaseJewel jewelry:

                    // jewelry.VirtualArmorMod = ArmorMod;
                    // jewelry.Prot = new Prot(ProtectionType, ProtectionLevel);
                    break;
            }

            c.AddItem(item);

            return true;
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
            var lootItems = table.Roll();

            foreach (var item in lootItems)
            {
                if (MakeItemMagical(item))
                {
                    item.CreateIn(container);
                }
            }
        }


        public static bool MakeItemMagical(LootItem item)
        {
            bool isMagic = false;

            while (item.ItemChance > 0)
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
                item.EnchantLevel = (Math.Clamp(RandomDouble(),0, 0.75) + 0.26) * (item.ItemLevel / 2 + 1);

            if (item.ItemLevel == 5)
                item.EnchantLevel += 0.51;


            switch (item.Type)
            {
                case {} when item.Is<BaseWeapon>():
                    if (item.EnchantLevel < 0.75)
                        ApplyDurabilityMod(item);
                    else if (item.EnchantLevel < 1.5)
                        ApplyWeaponSkillMod(item);
                    else if (item.EnchantLevel < 3.5)
                        ApplyDamageMod(item);
                    else
                        ApplyWeaponHitScript(item);
                    break;

                case {} when item.Is<BaseShield>():
                    if (item.EnchantLevel < 1.0)
                        ApplyDurabilityMod(item);
                    else if (item.EnchantLevel < 2.0)
                        ApplyArmorSkillMod(item);
                    else
                        ApplyArmorMod(item);
                    break;

                case {} when item.Is<BaseClothing>():
                    if (item.EnchantLevel < 2.5)
                        ApplyMiscSkillMod(item);
                    else
                        ApplyMiscArmorMod(item);
                    AddRandomColor(item);
                    break;

                case {} when item.Is<BaseJewel>():
                    if (item.EnchantLevel < 2.5)
                        ApplyMiscSkillMod(item);
                    else if (item.EnchantLevel < 3.0)
                        ApplyEnchant(item);
                    else
                        ApplyMiscArmorMod(item);
                    break;

                case {} when item.Is<BaseTool>() || item.Type == typeof(Pickaxe):
                    ApplyMiscSkillMod(item);
                    break;

                case {} when item.Is<BaseArmor>():
                    if (item.EnchantLevel < 0.75)
                        ApplyDurabilityMod(item);
                    else if (item.EnchantLevel < 1.5)
                        ApplyArmorSkillMod(item);
                    else if (item.EnchantLevel < 3.5)
                        ApplyArmorMod(item);
                    else
                        ApplyOnHitScript(item);
                    break;
            }

            return isMagic;
        }


        private static void ApplyMiscSkillMod(LootItem item)
        {
            var chance = Utility.Random(1, 1000);
            if (chance <= 5)
            {
                ApplyStatMod(item);
                return;
            }

            var level = Utility.Random(1, 50) + item.ItemLevel * 2;
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

            if (level < 200)
                value = 1;
            else if (level < 300)
                value = 2;
            else if (level < 400)
                value = 3;
            else if (level < 500)
                value = 4;
            else if (level < 600)
                value = 5;
            else
                value = 6;

            var skill = RandomSkill();

            if (Spec.SpecSkills[SpecName.Mage].Contains(skill) && item.ArmorMod > 0 && RandomBool())
                item.ArmorMod = 0;

            item.SkillBonusName = skill;
            item.SkillBonusValue = value;
            item.Color = 1109;
        }

        private static void ApplyOnHitScript(LootItem item)
        {
            item.Hitscript = "ArmorOnHitScript";
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
            item.Hitscript = "GreaterHitscript";
        }

        private static void ApplyEffectHitscript(LootItem item)
        {
            item.Hitscript = "EffectHitScript";
        }

        private static void ApplySlayerHitscript(LootItem item)
        {
            var slayers = Enum.GetValues(typeof(SlayerName)).OfType<SlayerName>().ToList();

            var slayer = slayers[Utility.Random(slayers.Count)];

            item.SlayerName = slayer;
        }

        private static void ApplySpellHitscript(LootItem item)
        {
            var multiplier = 10;
            var spellId = Utility.Random(1, 64);

            var asCircle = (Utility.Random(100) + 1) * (item.ItemLevel - 3);

            if (asCircle < 50)
                asCircle = 1;
            else if (asCircle < 100)
                asCircle = 2;
            else if (asCircle < 150)
                asCircle = 3;
            else if (asCircle < 200)
                asCircle = 4;
            else if (asCircle < 250)
                asCircle = 5;
            else if (asCircle < 300)
                asCircle = 6;
            else
                asCircle = 7;

            /*
	            var ascirclemod := hitscriptcfg[n].AsCircleMod;
	            if (ascirclemod)
		            ascircle := ascircle + ascirclemod;
	            endif

	            if (ascircle <= 0)
		            ascircle := 1;
	            endif
             */

            var effectChance = Utility.Random(1, 10) * item.ItemLevel;
            // var effectChancemod = hitscriptcfg[n].ChanceOfEffectMod;

            var effectChanceMod = 0;
            if (effectChanceMod > 0)
                effectChance = +effectChanceMod;


            if (effectChance <= 0)
                effectChance = 4;

            item.Hitscript = $"HitWithSpell,{spellId},{asCircle},{effectChance}";
        }


        private static void ApplyStatMod(LootItem item)
        {
            var level = Utility.Random(1, 100) * item.ItemLevel;
            int amount;

            if (level < 200)
                amount = 5;
            else if (level < 300)
                amount = 10;
            else if (level < 400)
                amount = 15;
            else if (level < 500)
                amount = 20;
            else if (level < 600)
                amount = 25;
            else
                amount = 30;

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
                //ApplyProtection();
            }
            else if (level < 500)
                ApplyElementalImmunity(item);
            else
            {
                // ApplyMagicImmunity();
            }


            if (Utility.Random(1, 100) <= (5 * item.ItemLevel))
            {
                level = Utility.Random(1, 100);

                if (level < 75)
                    ApplyMiscSkillMod(item);
                else
                    ApplyMiscArmorMod(item);
            }
        }


        private static void ApplyElementalImmunity(LootItem item)
        {
            var level = Utility.Random(1, 100) * item.ItemLevel;
            int value = 0;

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

            if (level < 150)
                value = 1;
            else if (level < 300)
                value = 2;
            else if (level < 450)
                value = 3;
            else if (level < 550)
                value = 4;
            else if (level < 600)
                value = 5;
            else
                value = 6;

            var element = Utility.Random(1, 7) switch
            {
                1 => ElementalType.Fire,
                2 => ElementalType.Water,
                3 => ElementalType.Air,
                4 => ElementalType.Earth,
                5 => ElementalType.Necro,
                6 => ElementalType.Poison,
                _ => ElementalType.None,
            };


            item.ProtectionLevel = value;
            item.ProtectionType = element;
        }

        private static void AddRandomColor(LootItem item)
        {
            do
            {
                item.Color = Utility.Random(1, 1184);
            } while (item.Color > 999 && item.Color < 1152);
        }

        private static void ApplyDamageMod(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
            int value = 0;

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

            if (level < 150)
                value = 1;
            else if (level < 300)
                value = 2;
            else if (level < 400)
                value = 3;
            else if (level < 500)
                value = 4;
            else if (level < 600)
                value = 5;
            else
                value = 6;

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
            int value;
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

            if (level < 200)
                value = 1;
            else if (level < 350)
                value = 2;
            else if (level < 450)
                value = 3;
            else if (level < 550)
                value = 4;
            else if (level < 600)
                value = 5;
            else
                value = 6;

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

            int value;
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

            if (level < 100)
                value = 1;
            else if (level < 200)
                value = 2;
            else if (level < 350)
                value = 3;
            else if (level < 450)
                value = 4;
            else if (level < 550)
                value = 5;
            else
                value = 6;

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

            int value;
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

            if (level < 200)
                value = 1;
            else if (level < 300)
                value = 2;
            else if (level < 400)
                value = 3;
            else if (level < 500)
                value = 4;
            else if (level < 600)
                value = 5;
            else
                value = 6;

            item.SkillBonusName = RandomBool() ? SkillName.MagicResist : SkillName.Hiding;
            item.SkillBonusValue = value;

            if (Utility.Random(1, 100) < 5 * item.ItemLevel)
                ApplyDurabilityMod(item);
        }

        private static void ApplyArmorMod(LootItem item)
        {
            var level = Utility.Random(1, 50) * item.ItemLevel * 2;
            int value = 0;

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

            if (level < 150)
                value = 8;
            else if (level < 300)
                value = 16;
            else if (level < 400)
                value = 24;
            else if (level < 500)
                value = 32;
            else if (level < 600)
                value = 40;
            else
                value = 48;

            if (Utility.Random(1, 100) <= 10 * item.ItemLevel)
            {
                if (Utility.Random(1, 100) <= 75)
                    ApplyDurabilityMod(item);
                else
                    ApplyArmorSkillMod(item);
            }

            item.ArmorMod = value;
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