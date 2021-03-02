using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PhysicalProtection : Enchantment<PhysicalProtectionInfo>, IDistinctEnchantment
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
            if (damageType == ElementalType.Physical) 
                damage -= (int) (damage * ((double) Value / 100));
        }

        public override void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
            var reduction = (int) (damage * ((double) Value / 100));

            if (attacker is PlayerMobile) 
                reduction /= 2;

            damage -= reduction;
        }
        
        public int CompareTo(object obj) => obj switch
        {
            PhysicalProtection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class PhysicalProtectionInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Elemental Physical Protection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Protection", "Pain"},
            {"Stoneskin", "Bleeding"},
            {"Unmovable Stone", "Rending"},
            {"Adamantine Shielding", "Tearing"},
            {"Mystical Cloaks", "Shredding"},
            {"Holy Auras", "Peril"},
        };
        public override int Hue { get; protected set; } = 1160;
        public override int CursedHue { get; protected set; } = 1160;
    }

}