using System;
using Server;

namespace Server.Scripts.Engines.Loot
{
    public abstract class LootEntry<T>
    {
        public int MinQuantity { get; }
        public int MaxQuantity { get; }
        public double Chance { get; }
        public T Value { get; }

        public LootEntry(T value, int minQuantity, int maxQuantity, double chance)
        {
            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;
            Chance = chance;
            Value = value;
        }
    }
    
    public class LootEntryItem : LootEntry<Type>
    {
        public LootEntryItem(Type value, int minQuantity, int maxQuantity, double chance) : base(value, minQuantity, maxQuantity, chance)
        {
            if(!value.IsSubclassOf(typeof(Item)))
                throw new ArgumentException(nameof(value), $"Received Type {value} must be a subclass of {typeof(Item)}");
        }

    }

    public class LootEntryGroup : LootEntry<LootGroup>
    {
        public LootEntryGroup(LootGroup value, int minQuantity, int maxQuantity, double chance): base(value, minQuantity, maxQuantity, chance)
        {
        }

    }
    
    

}