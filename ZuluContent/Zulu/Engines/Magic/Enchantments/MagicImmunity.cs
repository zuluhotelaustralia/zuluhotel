using System;
using MessagePack;
using Server;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class MagicImmunity : Enchantment<MagicImmunityInfo>, IDistinctEnchantment
    {
        [IgnoreMember]
        public override string AffixName => Info.GetName(Value, Cursed);
        [IgnoreMember]
        public override EnchantmentInfo Info => Charges != int.MaxValue
            ? MagicImmunityInfo.ChargedMagicImmunity
            : MagicImmunityInfo.PermMagicImmunity;

        [Key(1)] public SpellCircle Value { get; set; } = 0;
        [Key(2)] public int Charges { get; set; } = int.MaxValue;

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
                var nullify = false;
                if (Charges == int.MaxValue)
                {
                    nullify = true;
                }
                else if (Charges >= (int)spell.Circle)
                {
                    if ((Charges -= (int)spell.Circle) == 0)
                        NotifyMobile(defender, "One of your magic immunity items has run out of charges!");

                    nullify = true;
                }

                if (nullify)
                {
                    damage = 0;
                    NotifyMobile(defender, $"{attacker.Name}'s spell is absorbed by your magical protection!");
                    NotifyMobile(defender, attacker, $"The spell dissipates upon contact with {defender.Name}'s magical barrier!");
                }
            }
        }
        
        public int CompareTo(object obj) => obj switch
        {
            MagicImmunity other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }
    
    public class MagicImmunityInfo : EnchantmentInfo
    {
        public static readonly MagicImmunityInfo PermMagicImmunity = new()
        {
            Description = "Permanent Magic Immunity",
            Hue = 802,
            CursedHue = 802,
            Names = new [,] {
                {string.Empty, string.Empty},
                {"Raw Blackrock", "Chipped Blackrock"},
                {"Refined Blackrock", "Cracked Blackrock"},
                {"Processed Blackrock", "Flawed Blackrock"},
                {"Smelted Blackrock", "Inferior Blackrock"},
                {"Forged Blackrock", "Chaotic Blackrock"},
                {"Tempered Blackrock", "Corrupted Blackrock"},
            }
        };
        
        public static readonly MagicImmunityInfo ChargedMagicImmunity = new()
        {
            Description = "Permanent Magic Immunity",
            Hue = 802,
            CursedHue = 802,
            Names = new [,] {
                {string.Empty, string.Empty},
                {"Raw Blackrock", "Chipped Blackrock"},
                {"Refined Blackrock", "Cracked Blackrock"},
                {"Processed Blackrock", "Flawed Blackrock"},
                {"Smelted Blackrock", "Inferior Blackrock"},
                {"Forged Blackrock", "Chaotic Blackrock"},
                {"Tempered Blackrock", "Corrupted Blackrock"},
            }
        };

        public override string Description { get; protected set; }
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; }
        public override int CursedHue { get; protected set; }
        public override string[,] Names { get; protected set; }
    }
}