using System;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PermMagicReflection : Enchantment<PermSpellReflectInfo>, IDistinctEnchantment
    {
        [IgnoreMember] private SpellCircle m_Value = 0;

        [IgnoreMember] public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);

        [Key(1)]
        public SpellCircle Value
        {
            get => Cursed > CurseType.None ? 0 : m_Value;
            set => m_Value = value;
        }

        public override void OnCheckMagicReflection(Mobile parent, Spell spell, ref bool reflected)
        {
            if (
                spell is ITargetableAsyncSpell<Mobile> && 
                spell.Caster != parent && 
                Value >= spell.Circle &&
                !reflected
            )
            {
                reflected = true;
            }
        }

        public int CompareTo(object obj) => obj switch
        {
            PermMagicReflection other => ReferenceEquals(this, other) ? 0 : Value.CompareTo(other.Value),
            null => 1,
            _ => throw new ArgumentException($"Object must be of type {GetType().FullName}")
        };
    }

    public class PermSpellReflectInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Permanent Magic Reflection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty},
            {"Raw Moonstone", "Chipped Moonstone"},
            {"Cut Moonstone", "Cracked Moonstone"},
            {"Refined Moonstone", "Flawed Moonstone"},
            {"Prepared Moonstone", "Inferior Moonstone"},
            {"Enchanted Moonstone", "Chaotic Moonstone"},
            {"Flawless Moonstone", "Corrupted Moonstone"},
        };
    }
}