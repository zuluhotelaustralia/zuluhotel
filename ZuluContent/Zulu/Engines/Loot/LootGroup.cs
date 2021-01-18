using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server.Items;

namespace Server.Scripts.Engines.Loot
{
    public partial class LootGroup : IEnumerable
    {
        public List<LootEntryItem> Items { get; } = new List<LootEntryItem>();

        public void Add(LootEntryItem item) => Items.Add(item);

        public void Add(Type itemType, double chance = 1)
            => Items.Add(new LootEntryItem(itemType, 1, 1, chance));

        public void Add(Type itemType, int quantity, double chance)
            => Items.Add(new LootEntryItem(itemType, quantity, quantity, chance));

        public void Add(Type itemType, int minQuantity, int maxQuantity, double chance)
            => Items.Add(new LootEntryItem(itemType, minQuantity, maxQuantity, chance));

        public void Add(IEnumerable<Type> types)
        {
            foreach (var t in types) 
                Add(t);
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public List<LootItem> Roll(int quantity)
        {
            var items = new List<LootItem>();

            if (Items.Count == 0)
                return items;

            for (var i = 0; i < quantity; i++)
            {
                var item = Items[Utility.Random(Items.Count)];
                var itemQuantity = Utility.Random(item.MinQuantity, item.MaxQuantity);
                if (Utility.RandomDouble() < item.Chance)
                    items.Add(new LootItem(item.Value) {Quantity = itemQuantity});
            }

            return items;
        }
    }


    /**
     * Loot Group definitions
     * (Split into a partial for separation from the class members)
     * 
     */
    public partial class LootGroup
    {
        public static readonly LootGroup Ores = new LootGroup
        {
            {typeof(IronOre), 3, 9, 0.91},
            {typeof(SpikeOre), 3, 6, 0.88},
            {typeof(FruityOre), 3, 6, 0.85},
            {typeof(BlackDwarfOre), 3, 6, 0.82},
            {typeof(BronzeOre), 3, 6, 0.79},
            {typeof(DarkPaganOre), 3, 6, 0.76},
            {typeof(SilverRockOre), 2, 6, 0.73},
            {typeof(PlatinumOre), 2, 6, 0.70},
            {typeof(DullCopperOre), 2, 6, 0.67},
            {typeof(MysticOre), 2, 6, 0.64},
            {typeof(CopperOre), 2, 6, 0.61},
            {typeof(SpectralOre), 1, 4, 0.58},
            {typeof(OnyxOre), 1, 4, 0.55},
            {typeof(OldBritainOre), 1, 4, 0.52},
            {typeof(RedElvenOre), 1, 4, 0.49},
            {typeof(UndeadOre), 1, 4, 0.46},
            {typeof(PyriteOre), 1, 3, 0.43},
            {typeof(VirginityOre), 1, 3, 0.40},
            {typeof(MalachiteOre), 1, 3, 0.37},
            {typeof(LavarockOre), 1, 3, 0.34},
            {typeof(AzuriteOre), 1, 3, 0.31},
            {typeof(IceRockOre), 1, 2, 0.28},
            {typeof(DripstoneOre), 1, 2, 0.25},
            {typeof(ExecutorOre), 1, 2, 0.22},
            {typeof(PeachblueOre), 1, 2, 0.19},
            {typeof(DestructionOre), 1, 2, 0.16},
            {typeof(AnraOre), 1, 0.13},
            {typeof(CrystalOre), 1, 0.10},
            {typeof(DoomOre), 1, 0.7},
            {typeof(GoddessOre), 1, 0.4},
            {typeof(NewZuluOre), 1, 0.1},
        };

        public static readonly LootGroup Gems = new LootGroup
        {
            Server.Loot.GemTypes
        };

        public static readonly LootGroup Reagents = new LootGroup
        {
            Server.Loot.RegTypes
        };

        public static readonly LootGroup PaganReagents = new LootGroup
        {
            Server.Loot.PaganRegTypes
        };

        public static readonly LootGroup Weapons = new LootGroup
        {
            Server.Loot.WeaponTypes,
            // Server.Loot.AosWeaponTypes
        };

        public static readonly LootGroup Armor = new LootGroup
        {
            Server.Loot.ArmorTypes,
            Server.Loot.ShieldTypes,
            Server.Loot.AosShieldTypes
        };

        public static readonly LootGroup Jewelry = new LootGroup
        {
            Server.Loot.JewelryTypes
        };
        
        public static readonly LootGroup Clothing = new LootGroup
        {
            Server.Loot.ClothingTypes
        };
    }
}