using System;
using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    
    [MessagePackObject]
    public class ArmorBonus : Enchantment<ArmorBonusInfo>
    {
        [IgnoreMember]
        private ArmorBonusType m_Value = ArmorBonusType.None;

        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed, CurseLevel);

        [Key(1)]
        public ArmorBonusType Value
        {
            get => Cursed ? (ArmorBonusType)(ArmorBonusType.None - m_Value) : m_Value;
            set => m_Value = value;
        }

        [CallPriority(1)]
        public override bool GetShouldDye() => Value > ArmorBonusType.None;
        
        public override int CompareTo(object obj) => obj switch
        {
            ArmorBonus other => ReferenceEquals(this, other) ? 0 : m_Value.CompareTo(other.m_Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class ArmorBonusInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Armor Bonus";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        
        public override int Hue { get; protected set; } = 1109;
        public override int CursedHue { get; protected set; } = 0;
        
        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty}, // None
            {"Iron", "Glass"},
            {"Steel", "Rusty"},
            {"Meteoric Steel", "Aluminum"},
            {"Obsidian", "Pitted"},
            {"Onyx", "Dirty"},
            {"Adamantium", "Tarnished"}
        };
    }
}