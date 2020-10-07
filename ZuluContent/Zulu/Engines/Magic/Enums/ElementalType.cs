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

        public static int GetResistForProtectionLevel(ElementalProtectionLevel level) => 
            ProtectionToResist[level];

        public static ElementalProtectionLevel GetProtectionLevelForResist(int resist) =>
            ProtectionToResist.FirstOrDefault(kv => kv.Value <= resist).Key;
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
        None
    }
}