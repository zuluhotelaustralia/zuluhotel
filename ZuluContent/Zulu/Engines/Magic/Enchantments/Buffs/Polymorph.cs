using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.First;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class Polymorph : BaseStatBonus<PolymorphInfo>, IBuff
    {
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(2)]
        public (int body, int bodyHue) BodyMods
        {
            get;
            init;
        }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.Polymorph;
        [IgnoreMember] public string Title { get; init; } = "Polymorph";
        [IgnoreMember] public string Description { get; init; }
        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; } = TimeSpan.Zero;
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;
        
        protected override string StatModName => $"{GetType().Name}:{StatType.ToString()}:{Icon}";

        public void OnBuffAdded(Mobile parent)
        {
            parent.BodyMod = BodyMods.body;
            parent.HueMod = BodyMods.bodyHue;
            parent.VirtualArmorMod += Value / 3;
            
            BaseArmor.ValidateMobile(parent);
            BaseClothing.ValidateMobile(parent);
            (parent as IEnchanted)?.Enchantments.Set(this);
            OnAdded(parent);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            if (parent is IEnchanted enchanted)
            {
                parent.BodyMod = 0;
                parent.HueMod = -1;
                
                parent.VirtualArmorMod -= Value / 3;
                if (parent.VirtualArmorMod < 0)
                    parent.VirtualArmorMod = 0;
                
                BaseArmor.ValidateMobile(parent);
                BaseClothing.ValidateMobile(parent);
                
                enchanted.Enchantments.Remove(this);
                OnRemoved(parent, parent);
            }
        }

        #endregion

        public Polymorph() : base(StatType.All)
        {
        }
    }

    #region EnchantmentInfo

    public class PolymorphInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Reactive Armor";
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