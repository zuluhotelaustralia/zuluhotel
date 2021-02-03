using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server.Items;

namespace Server.Scripts.Engines.Loot
{
    public class LootGroup : IEnumerable
    {
        public List<LootEntryItem> Items { get; } = new();

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
        
        private void Add(LootGroup subGroup)
        {
            foreach (var i in subGroup.Items) 
                Add(i);
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
}