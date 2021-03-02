using System;
using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class HarvestBonus : Enchantment<HarvestBonusInfo>
    {
        [IgnoreMember] private int m_Value = 0;

        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public int Value
        {
            get => m_Value;
            set => m_Value = value;
        }

        public override void OnToolHarvestBonus(Mobile harvester, ref int amount)
        {
            amount *= Value;
        }

        public override void OnToolHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod)
        {
            bonus += 6 * Value;
            toMod -= 5 * Value;
        }
    }

    public class HarvestBonusInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Harvest Bonus";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = { };

        public override string GetName(int index, CurseType curse = CurseType.None)
        {
            return string.Empty;
        }
    }
}