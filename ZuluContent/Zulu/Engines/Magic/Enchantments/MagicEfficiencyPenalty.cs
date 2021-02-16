using System;
using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class MagicEfficiencyPenalty : Enchantment<MagicEfficiencyInfo>
    {
        [IgnoreMember] private double m_Value = 0;

        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public double Value
        {
            get => m_Value;
            set => m_Value = value;
        }
    }

    public class MagicEfficiencyInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Magic Efficiency Penalty";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = { };

        public override string GetName(int index, bool cursed = false, CurseLevelType curseLevel = CurseLevelType.None)
        {
            return string.Empty;
        }
    }
}