using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class Protection : Enchantment<ProtectionInfo>, IBuff
    {
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public int Value { get; set; } = 0;

        #region IBuff
        [IgnoreMember] public BuffIcon Icon { get; } = BuffIcon.Protection;
        [IgnoreMember] public int TitleCliloc { get; } = 1075814; // Protection
        [IgnoreMember] public int SecondaryCliloc { get; } = IBuff.BlankCliloc;
        [IgnoreMember] public TextDefinition Args =>  Value.ToString("+0;-#") + " armor";
        [IgnoreMember] public bool RetainThroughDeath { get; } = false;
        [IgnoreMember] public bool Dispellable { get; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        
        public void OnBuffAdded(Mobile parent)
        {
            parent.VirtualArmorMod += Value;
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            parent.VirtualArmorMod -= Value;

            if (parent.VirtualArmorMod < 0)
                parent.VirtualArmorMod = 0;
            
            if (parent is IEnchanted enchanted) 
                enchanted.Enchantments.Remove(this);
        }
        
        #endregion
    }

    #region EnchantmentInfo
    public class ProtectionInfo : EnchantmentInfo
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