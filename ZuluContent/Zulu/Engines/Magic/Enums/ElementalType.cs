using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Engines.Magic
{
    public interface IElementalResistible
    {
        public int ElementalWaterResist { get; set; }

        public int ElementalAirResist { get; set; }

        public int ElementalPhysicalResist { get; set; }

        public int ElementalFireResist { get; set; }

        public int ElementalPoisonResist { get; set; }


        public int ElementalEarthResist { get; set; }

        public int ElementalNecroResist { get; set; }

        private static readonly Dictionary<ElementalProtectionLevel, int> ProtectionToResist =
            new Dictionary<ElementalProtectionLevel, int>
            {
                [ElementalProtectionLevel.None] = 0,
                [ElementalProtectionLevel.Bane] = 15,
                [ElementalProtectionLevel.Warding] = 30,
                [ElementalProtectionLevel.Protection] = 45,
                [ElementalProtectionLevel.Immunity] = 65,
                [ElementalProtectionLevel.Attunement] = 80,
                [ElementalProtectionLevel.Absorbsion] = 100,
            };

        private static readonly ElementalType[] ElementalTypesWithCharges = 
            {
                ElementalType.PoisonImmunity,
                ElementalType.MagicImmunity,
                ElementalType.SpellReflect
            };

        public static bool HasCharges(ElementalType type) =>
            ElementalTypesWithCharges.Contains(type);

        public static int GetResistForProtectionLevel(ElementalProtectionLevel level) => 
            ProtectionToResist[level];

        public static ElementalProtectionLevel GetProtectionLevelForResist(int resist) =>
            ProtectionToResist.
                Where(kv => kv.Value <= resist).
                OrderByDescending(kv => kv.Value).
                FirstOrDefault().Key;
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

    public enum ElementalType
    {
        Water = 0,
        Air,
        Physical,
        Fire,
        Cold,
        Poison,
        Energy,
        Earth,
        Necro,
        Paralysis,
        HealingBonus,
        PoisonImmunity,
        MagicImmunity,
        SpellReflect,
        PermPoisonImmunity,
        PermMagicImmunity,
        PermSpellReflect,
        None
    }
}