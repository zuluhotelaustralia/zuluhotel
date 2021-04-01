using System;
using System.Collections.Generic;
using System.Linq;
using Server.Spells;

namespace Server.Engines.Magic
{
    public interface IElementalResistible
    {
        public int ElementalWaterResist { get; }

        public int ElementalAirResist { get; }

        public int ElementalPhysicalResist { get; }

        public int ElementalFireResist { get; }

        public int ElementalEarthResist { get; }

        public int ElementalNecroResist { get; }

        public int ParalysisProtection { get; }

        public int HealingBonus { get; }
        public PoisonLevel PoisonImmunity { get; }

        public SpellCircle MagicImmunity { get; }

        public SpellCircle MagicReflection { get; }


        private static readonly Dictionary<ElementalProtectionLevel, int> ProtectionToResist = new()
        {
            [ElementalProtectionLevel.None] = 0,
            [ElementalProtectionLevel.Bane] = 25,
            [ElementalProtectionLevel.Warding] = 50,
            [ElementalProtectionLevel.Protection] = 75,
            [ElementalProtectionLevel.Immunity] = 100,
            [ElementalProtectionLevel.Attunement] = 125,
            [ElementalProtectionLevel.Absorbsion] = 150,
        };

        public static int GetResistForProtectionLevel(ElementalProtectionLevel level) =>
            ProtectionToResist[level];

        public static ElementalProtectionLevel GetProtectionLevelForResist(int resist) =>
            ProtectionToResist.Where(kv => kv.Value <= resist).OrderByDescending(kv => kv.Value).FirstOrDefault().Key;
    }

    public enum ElementalProtectionLevel
    {
        None = 0,
        Bane,
        Warding,
        Protection,
        Immunity,
        Attunement,
        Absorbsion
    }

    public enum PoisonLevel
    {
        None = 0,
        Lesser,
        Medium,
        Greater,
        Deadly,
        Lethal
    }

    public enum ElementalType
    {
        None = 0,
        Water,
        Air,
        Physical,
        Fire,
        Poison,
        Earth,
        Necro,
        Paralysis,
        HealingBonus,
        MagicImmunity,
        MagicReflection,
    }
}