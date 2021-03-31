using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Engines.Magic.IElementalResistible;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PermMagicImmunity : Enchantment<PermMagicImmunityInfo>, IDistinctEnchantment
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);

        [Key(1)] 
        public SpellCircle Value { get; set; } = 0;

        public override void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {

            if (Cursed > CurseType.None)
            {
                NotifyMobile(defender, "Your items prevent all absorbtion!");
                return;
            }

            if (Value >= spell.Circle)
            {
                damage = 0;
                NotifyMobile(defender, $"{attacker.Name}'s spell is absorbed by your magical protection!");
                NotifyMobile(defender, attacker, $"The spell dissipates upon contact with {defender.Name}'s magical barrier!");
            }
        }
        
        public int CompareTo(object obj) => obj switch
        {
            PermMagicImmunity other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class PermMagicImmunityInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Permanent Magic Immunity";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 802;
        public override int CursedHue { get; protected set; } = 802;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Raw Blackrock", "Chipped Blackrock"},
            {"Refined Blackrock", "Cracked Blackrock"},
            {"Processed Blackrock", "Flawed Blackrock"},
            {"Smelted Blackrock", "Inferior Blackrock"},
            {"Forged Blackrock", "Chaotic Blackrock"},
            {"Tempered Blackrock", "Corrupted Blackrock"},
        };
    }
}