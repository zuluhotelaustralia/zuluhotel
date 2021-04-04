// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
using System;
using System.Linq;

namespace Server.Spells
{
    public sealed record SpellCircle : IComparable
    {
        public static readonly SpellCircle System = new() { Id = 0,  Name = "System" };
        public static readonly SpellCircle Highest = ZhConfig.Spells.SpellCircles.Keys.Max();
        public static readonly SpellCircle Lowest = ZhConfig.Spells.SpellCircles.Keys.Min();
        public static readonly SpellCircle First = 1;
        public static readonly SpellCircle Second = 2;
        public static readonly SpellCircle Third = 3;
        public static readonly SpellCircle Fourth = 4;
        public static readonly SpellCircle Fifth = 5;
        public static readonly SpellCircle Sixth = 6;
        public static readonly SpellCircle Seventh = 7;
        public static readonly SpellCircle Eighth = 8;
        
        private readonly int m_EffectiveCircle;
        public int Id { get; init; }
        public string Name { get; init; }
        public int Mana { get; init; }
        public int Difficulty { get; init; }
        public int PointValue { get; init; }
        public int Delay { get; init; }

        public int EffectiveCircle
        {
            get => m_EffectiveCircle == 0 ? Id : m_EffectiveCircle;
            init => m_EffectiveCircle = value;
        }

        public static implicit operator SpellCircle(int id) =>
            ZhConfig.Spells.SpellCircles.TryGetValue(id, out var value) ? value : null;
        
        public static implicit operator SpellCircle(string name) =>
            ZhConfig.Spells.SpellCircles.FirstOrDefault(kv => kv.Value.Name == name).Value;

        public static implicit operator int(SpellCircle c) => c.EffectiveCircle;

        public override string ToString() => Name ?? Id.ToString();

        public int CompareTo(object obj) => obj switch
        {
            SpellCircle other => ReferenceEquals(this, other) ? 0 : Id.CompareTo(other.Id),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
}