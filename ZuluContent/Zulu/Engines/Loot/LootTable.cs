using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Scripts.Engines.Loot
{
    public class LootTable : LootGroup, IEnumerable
    {
        public List<LootEntryGroup> Groups { get; } = new();

        public LootTable()
        {

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
        
        public List<LootItem> Roll(int itemLevel, double itemChance)
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
                li.ItemLevel = itemLevel;
                li.ItemChance = itemChance;
            }

            return lootItems;
        }
    }
}