using System;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class MagicReflection : Enchantment<MagicReflectionInfo>, IDistinctEnchantment
    {
        [IgnoreMember] private int m_Value = 0;
        [IgnoreMember] public override string AffixName => Info.GetName(Value, Cursed);

        [IgnoreMember]
        public override EnchantmentInfo Info => Charges != int.MaxValue
            ? MagicReflectionInfo.ChargedMagicReflection
            : MagicReflectionInfo.PermMagicReflection;

        [Key(1)]
        public int Value
        {
            get => Cursed > CurseType.None ? 0 : m_Value;
            set => m_Value = value;
        }
        
        [Key(2)] public virtual int Charges { get; set; } = int.MaxValue;

        public override void OnCheckMagicReflection(Mobile parent, Spell spell, ref bool reflected)
        {
            if (
                spell is ITargetableAsyncSpell<Mobile> && 
                spell.Caster != parent && 
                Value >= spell.Circle &&
                !reflected
            )
            {
                if (Charges == int.MaxValue)
                {
                    reflected = true;
                }
                else if (Charges >= spell.Circle)
                {
                    if ((Charges -= spell.Circle) == 0)
                        NotifyMobile(parent, "One of your magic reflection items has run out of charges!");
                    
                    reflected = true;
                }
            }
        }

        public int CompareTo(object obj) => obj switch
        {
            MagicReflection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }

    public class MagicReflectionInfo : EnchantmentInfo
    {
        public static readonly MagicReflectionInfo PermMagicReflection = new()
        {
            Description = "Permanent Magic Reflection",
            Hue = 802,
            CursedHue = 802,
            Names = new [,] {
                {string.Empty, string.Empty},
                {"Raw Moonstone", "Chipped Moonstone"},
                {"Cut Moonstone", "Cracked Moonstone"},
                {"Refined Moonstone", "Flawed Moonstone"},
                {"Prepared Moonstone", "Inferior Moonstone"},
                {"Enchanted Moonstone", "Chaotic Moonstone"},
                {"Flawless Moonstone", "Corrupted Moonstone"},
            }
        };
        
        public static readonly MagicReflectionInfo ChargedMagicReflection = new()
        {
            Description = "Charged Magic Reflection",
            Hue = 802,
            CursedHue = 802,
            Names = new [,] {
                {"Spell Warding", "Magical Retribution"},
            }
        };
        
        public override string Description { get; protected set; }
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; }
        public override int CursedHue { get; protected set; }
        public override string[,] Names { get; protected set; }
    }
}