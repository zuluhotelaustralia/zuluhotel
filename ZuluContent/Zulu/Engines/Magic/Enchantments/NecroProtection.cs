using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class NecroProtection : Enchantment<NecroProtectionInfo>, IDistinctEnchantment
    {
        [IgnoreMember]
        private int m_Value = 0;

        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(
            IElementalResistible.GetProtectionLevelForResist(Value), Cursed);

        [Key(1)]
        public int Value
        {
            get => Cursed > CurseType.None ? -m_Value : m_Value;
            set => m_Value = value;
        }

        public override void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
            if (damageType == ElementalType.Necro) 
                damage -= (int) (damage * ((double) Value / 100));
        }
        
        public int CompareTo(object obj) => obj switch
        {
            NecroProtection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class NecroProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Necro Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Mystic Barrier", "Evil Influence"},
            {"Divine Shielding", "Liche's Laughter"},
            {"Heavenly Sanctuary", "Daemonic Temptation"},
            {"Angelic Protection", "Dark Channeling"},
            {"Arch-Angel's Guidance", "Shadow's Touch"},
            {"Seraphim's Warding", "Guardian's Blessing"},
        };

        public override int Hue { get; protected set; } = 1170;
        public override int CursedHue { get; protected set; } = 1170;
    }
}