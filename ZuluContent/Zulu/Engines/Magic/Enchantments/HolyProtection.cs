using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class HolyProtection : Enchantment<HolyProtectionInfo>
    {
        [IgnoreMember] private int m_Value = 0;

        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(
            IElementalResistible.GetProtectionLevelForResist(Value), Cursed, CurseLevel);

        [Key(1)]
        public int Value
        {
            get => Cursed ? -m_Value : m_Value;
            set => m_Value = value;
        }

        public override void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
            if (damageType == ElementalType.Holy)
                damage -= (int) (damage * ((double) Value / 100));
        }
        
        public override int CompareTo(object obj) => obj switch
        {
            HolyProtection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }

    public class HolyProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Holy Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty},
            {"Dark Barriers", "Holy Influence"},
            {"Infernal Shielding", "Cherub's Laughter"},
            {"Hellish Sanctuary", "Holy Channeling"},
            {"Daemonic Protection", "Searing Light"},
            {"Arch-Fiend's Guidance", "Heaven's Curse"},
            {"Devil's Warding", "Angelic Fury"},
        };

        public override int Hue { get; protected set; } = 1172;
        public override int CursedHue { get; protected set; } = 1172;
    }
}