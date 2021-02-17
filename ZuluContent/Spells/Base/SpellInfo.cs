using System;
using System.Collections.Generic;
using System.Linq;
using Server.Commands;
using Server.Engines.Magic;
using Server.Items;

namespace Server.Spells
{
    public record SpellInfo
    {
        public SpellCircle Circle { get; init; }
        public int Action { get; init; }
        public bool AllowTown { get; init; }
        public int[] Amounts => ReagentCosts.Values.ToArray();
        public string Mantra { get; init; }
        public string Name { get; init; }
        public Type[] Reagents => ReagentCosts.Keys.ToArray();
        public int LeftHandEffect { get; init; }
        public int RightHandEffect { get; init; }
        public Dictionary<Type, int> ReagentCosts { get; init; } = new();

        public ElementalType DamageType { get; init; } = ElementalType.None;
    }
}