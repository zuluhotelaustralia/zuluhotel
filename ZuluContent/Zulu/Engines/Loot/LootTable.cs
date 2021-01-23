using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Server.Items;
using static Server.Scripts.Engines.Loot.LootGroup;

namespace Server.Scripts.Engines.Loot
{
    public partial class LootTable : LootGroup, IEnumerable
    {
        public int ItemLevel { get; }
        public double ItemChance { get; }
        public List<LootEntryGroup> Groups { get; } = new List<LootEntryGroup>();

        public LootTable(int itemLevel, double itemChance)
        {
            ItemLevel = itemLevel;
            ItemChance = itemChance;
        }

        private void Guard(LootGroup group)
        {
            if (group is LootTable)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(group),
                    "Cannot append a LootTable to another LootTable due to circular referencing"
                );
            }
        }

        public void Add(LootEntryGroup group)
        {
            Guard(group.Value);
            Groups.Add(group);
        }

        public void Add(LootGroup group, int minQuantity, int maxQuantity, double chance)
        {
            Guard(group);
            Groups.Add(new LootEntryGroup(group, minQuantity, maxQuantity, chance));
        }

        public new IEnumerator GetEnumerator()
        {
            foreach (var entry in Groups)
                yield return entry;

            foreach (var entry in Items)
                yield return entry;
        }
        
        public List<LootItem> Roll()
        {
            var lootItems = new List<LootItem>();
            foreach (var entry in Groups)
            {
                if (Utility.RandomDouble() < entry.Chance)
                {
                    int quantity = Utility.Random(entry.MinQuantity, entry.MaxQuantity);
                    lootItems.AddRange(entry.Value.Roll(quantity));
                }
            }

            foreach (var entry in Items)
            {
                if (Utility.RandomDouble() < entry.Chance)
                {
                    int quantity = Utility.Random(entry.MinQuantity, entry.MaxQuantity);
                    var item = new LootItem(entry.Value) {Quantity = quantity};
                    lootItems.Add(item);
                }
            }

            foreach (var li in lootItems)
            {
                li.ItemLevel = ItemLevel;
                li.ItemChance = ItemChance;
            }

            return lootItems;
        }
    }

    /**
     * Loot table definitions
     * (Split into a partial for separation from the class members)
     * 
     */

    [SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
    public partial class LootTable
    {
        public static readonly LootTable Table1 = new LootTable(itemLevel: 1, itemChance: 1.0)
        {
            {typeof(CheeseSlice), 0.5},
            {Weapons, 1, 10, 0.5},
            {Armor, 1, 10, 0.5},
            {Jewelry, 1, 10, 0.5},
            {Clothing, 1, 10, 0.5},
        };

        // level6map, golden dragon, behemoth, balron, arachnidqueen
        public static readonly LootTable Table2 = new LootTable(itemLevel: 5, itemChance: 1.0)
        {
            //{typeof(Gold), 1000, 2000, 1.0},
            //{Reagents, 5, 6, 1.0},
            //{PaganReagents, 5, 6, 1.0},
            //{Gems, 1, 5, 1.0},
            //{Weapons, 1, 3, 0.5},
            //{Armor, 2, 3, 0.5},
            {Jewelry, 100, 150, 1.0},
            //{Clothing, 50, 50, 1.0},
        };
    }
}