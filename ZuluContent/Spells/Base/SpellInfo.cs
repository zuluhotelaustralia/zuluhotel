using System;
using System.Linq;
using System.Text.Json.Serialization;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells
{
    public record SpellInfo
    {
        public Type Type { get; set; }
        public string Name { get; init; }
        public string Mantra { init; get; }
        public SpellCircle Circle { get; init; }
        public ElementalType DamageType { get; init; } = ElementalType.None;
        public int RightHandEffect { get; init; }
        public int LeftHandEffect { get; init; }
        public int Action { get; init; }
        public bool ShowHandMovement { get; init; } = true;
        public bool RevealOnCast { get; init; } = true;
        public bool Resistable { get; init; } = true;
        public bool Reflectable { get; init; } = true;
        public bool ClearHandsOnCast { get; init; } = true;
        public bool BlocksMovement { get; init; } = false;
        public bool AllowTown { get; init; }
        public bool AllowDead { get; init; }
        public TargetOptions TargetOptions { get; init; }
        public ReagentCost[] ReagentCosts { get; init; } = Array.Empty<ReagentCost>();


        private int[] m_Amounts;
        [JsonIgnore]
        public int[] Amounts
        {
            get { return m_Amounts ??= ReagentCosts.Select(t => t.Amount).ToArray(); }
        }

        private Type[] m_Reagents;
        [JsonIgnore]
        public Type[] Reagents
        {
            get { return m_Reagents ??= ReagentCosts.Select(t => t.Reagent).ToArray(); }
        }
    }
}