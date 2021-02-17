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

        public int ParalysisResist { get; set; }

        public int HealingBonus { get; set; }

        public int MagicImmunity { get; set; }

        public int SpellReflection { get; set; }


        private static readonly Dictionary<ElementalProtectionLevel, int> ProtectionToResist =
            new Dictionary<ElementalProtectionLevel, int>
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

    public enum ElementalType
    {
        Water = 0,
        Air,
        Physical,
        Fire,
        Cold,
        Poison,
        Holy,
        Earth,
        Necro,
        Paralysis,
        HealingBonus,
        PermPoisonImmunity,
        PermMagicImmunity,
        PermSpellReflect,
        None
    }
}