// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Server.Spells
{
    public sealed record SpellCircle : IComparable
    {
        private static readonly List<SpellCircle> AllCircles = new();

        public static IReadOnlyList<SpellCircle> Circles => AllCircles;

        public static readonly SpellCircle System = new() { Id = 0,  Name = "System" };
        public static readonly SpellCircle Highest = AllCircles.Max(c => c.Id);
        public static readonly SpellCircle Lowest = AllCircles.Min(c => c.Id);
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

        public SpellCircle()
        {
            AllCircles.Add(this);
        }

        public static implicit operator SpellCircle(int id) =>
            AllCircles.FirstOrDefault(circle => circle.Id == id);
        
        public static implicit operator SpellCircle(string name) =>
            AllCircles.FirstOrDefault(circle => circle.Name.InsensitiveEquals(name));

        public static implicit operator int(SpellCircle c) => c?.EffectiveCircle ?? 0;

        public override string ToString() => Name ?? Id.ToString();

        public int CompareTo(object obj) => obj switch
        {
            SpellCircle other => ReferenceEquals(this, other) ? 0 : Id.CompareTo(other.Id),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
}