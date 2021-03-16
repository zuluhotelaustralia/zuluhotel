using System;
using System.Collections.Generic;
using System.Linq;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells
{
    public record SpellInfo
    {
        public const int DefaultSpellRange = 12;
        
        public SpellCircle Circle { get; init; }
        public int Action { get; init; }
        public bool AllowTown { get; init; }
        public bool AllowDead { get; init; }
        public bool DelayedDamageStacking { get; init; }
        public bool DelayedDamage { get; init; }
        public bool RevealOnCast { get; init; } = true;
        public bool ClearHandsOnCast { get; init; } = true;
        public bool ShowHandMovement { get; init; } = true;
        public bool BlocksMovement { get; init; } = true;
        public bool Resistable { get; init; } = true;
        public bool Reflectable { get; init; } = true;
        public int[] Amounts => ReagentCosts.Values.ToArray();
        public string Mantra { get; init; }
        public string Name { get; init; }
        public Type[] Reagents => ReagentCosts.Keys.ToArray();
        public int LeftHandEffect { get; init; }
        public int RightHandEffect { get; init; }
        public Dictionary<Type, int> ReagentCosts { get; init; } = new();
        public ElementalType DamageType { get; init; } = ElementalType.None;
        public TargetOptions TargetOptions { get; init; }
    }
}