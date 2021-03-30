using System;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static Server.Engines.Magic.IElementalResistible;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class PoisonImmunity : Enchantment<PoisonImmunityInfo>, IBuff
    {
        private string m_Description;
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)] public int Value { get; set; }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.PoisonImmunity;
        [IgnoreMember] public string Title { get; init; } = "Poison Immunity";

        [IgnoreMember]
        public string Description
        {
            get => m_Description ??= "Provides protection from poison";
            init => m_Description = value;
        }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = false;
        [IgnoreMember] public TimeSpan Duration { get; init; } = TimeSpan.Zero;
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        
        // We should use this first before checking item enchantments
        [CallPriority(1)]
        public override void OnCheckPoisonImmunity(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
            var poisonProtectionLevel = GetProtectionLevelForResist(Value);

            if ((int)poisonProtectionLevel >= poison.Level)
            {
                immune = true;
                NotifyMobile(defender, "Your poison immunity protected you from the poison!");
            }
        }

        public void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            if (parent is IEnchanted enchanted)
                enchanted.Enchantments.Remove(this);
        }

        #endregion
    }

    #region EnchantmentInfo

    public class PoisonImmunityInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Poison Immunity";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;
        public override string[,] Names { get; protected set; } = { };

        public override string GetName(int index, CurseType curse = CurseType.None)
        {
            return string.Empty;
        }
    }

    #endregion
}