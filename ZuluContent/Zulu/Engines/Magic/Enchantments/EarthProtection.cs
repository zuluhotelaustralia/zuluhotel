using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class EarthProtection : Enchantment<EarthProtectionInfo>
    {
        [IgnoreMember]
        private int m_Value = 0;

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
            if (damageType == ElementalType.Earth) 
                damage -= (int) (damage * ((double) Value / 100));
        }
        
        public override int CompareTo(object obj) => obj switch
        {
            EarthProtection other => ReferenceEquals(this, other) ? 0 : m_Value.CompareTo(other.m_Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class EarthProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Earth Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override string[,] Names { get; protected set; } = MakeProtNames("Earth", IEnchantmentInfo.DefaultElementalProtectionNames);
        public override int Hue { get; protected set; } = 343;
        public override int CursedHue { get; protected set; } = 343;
    }
}