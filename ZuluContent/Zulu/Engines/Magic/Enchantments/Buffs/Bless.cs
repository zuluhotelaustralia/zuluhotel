using System;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    public class Bless : BaseStatBonus<BlessInfo>, IBuff
    {
        [IgnoreMember] public BuffIcon Icon => Value > 0 ? BuffIcon.Bless : BuffIcon.Curse;
        [IgnoreMember] public int TitleCliloc => Value > 0 ? 1075845 : 1075837; // 1075847 : 1075835
        [IgnoreMember] public int SecondaryCliloc { get; } = IBuff.BlankCliloc;
        [IgnoreMember] public TextDefinition Args => Value.ToString("+0;-#") + " Str/Dex/Int";
        [IgnoreMember] public bool RetainThroughDeath { get; } = false;
        [IgnoreMember] public bool Dispellable { get; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        [IgnoreMember] public override string AffixName => string.Empty;
        public Bless() : base(StatType.All) { }
        
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
    
    public class BlessInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Bless";
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