using System;
using System.Diagnostics.CodeAnalysis;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    public class StatBuff : BaseStatBonus<StatBuffInfo>, IBuff
    {
        private BuffIcon? m_Icon;
        private string m_Title;
        private string m_Description;

        [IgnoreMember]
        public BuffIcon Icon
        {
            get => m_Icon ??= StatType switch
            {
                StatType.Str => Value > 0 ? BuffIcon.Strength : BuffIcon.Weaken,
                StatType.Dex => Value > 0 ? BuffIcon.Agility : BuffIcon.Clumsy,
                StatType.Int => Value > 0 ? BuffIcon.Cunning : BuffIcon.FeebleMind,
                StatType.All => Value > 0 ? BuffIcon.Bless : BuffIcon.Curse,
                _ => throw new ArgumentOutOfRangeException(nameof(StatType))
            };
            init => m_Icon = value;
        }

        [IgnoreMember]
        public string Title
        {
            get => m_Title ??= StatType switch
            {
                StatType.Str => Value > 0 ? "Strength" : "Weaken",
                StatType.Dex => Value > 0 ? "Agility" : "Clumsy",
                StatType.Int => Value > 0 ? "Cunning" : "Feeble Mind",
                StatType.All => Value > 0 ? "Bless" : "Curse",
                _ => throw new ArgumentOutOfRangeException(nameof(StatType))
            };
            init => m_Title = value;
        }

        [IgnoreMember]
        public string Description
        {
            get => m_Description ??= Value.ToString("+0;-#") + ' ' + StatType switch
            {
                StatType.Str => "Strength",
                StatType.Dex => "Dexterity",
                StatType.Int => "Intelligence",
                StatType.All => "Str/Int/Dex",
                _ => throw new ArgumentOutOfRangeException(nameof(StatType))
            };
            init => m_Description = value;
        }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; } = TimeSpan.Zero;
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        [IgnoreMember] public override string AffixName => string.Empty;
        
        protected override string StatModName => $"{GetType().Name}:{StatType.ToString()}:{Icon}";
        
        public StatBuff(StatType stat) : base(stat)
        {
        }

        public void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
            OnAdded(parent);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            OnRemoved(parent, parent);
            (parent as IEnchanted)?.Enchantments.Remove(this);
        }
    }

    public class StatBuffInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Stat Buff";
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