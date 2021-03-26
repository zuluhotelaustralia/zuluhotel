using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class ArmorBuff : Enchantment<ArmorBuffInfo>, IBuff, IArmorMod
    {
        private string m_Description;
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)] public int ArmorMod { get; init; }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.Protection;
        [IgnoreMember] public string Title { get; init; } = "Protection";

        [IgnoreMember]
        public string Description
        {
            get => m_Description ??= ArmorMod.ToString("+0;-#") + " Armor";
            init => m_Description = value;
        }
        
        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;

        public void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
            parent.Delta(MobileDelta.Armor);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            if (parent is IEnchanted enchanted)
                enchanted.Enchantments.Remove(this);
            parent.Delta(MobileDelta.Armor);
        }

        #endregion

    }

    #region EnchantmentInfo

    public class ArmorBuffInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Protection";
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