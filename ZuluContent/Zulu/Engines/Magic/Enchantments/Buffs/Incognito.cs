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
    public class Incognito : Enchantment<IncognitoInfo>, IBuff
    {
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public (int body, int bodyHue, int hair, int hairHue, int facial, int facialHue, string name) Values
        {
            get;
            init;
        }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.Incognito;
        [IgnoreMember] public string Title { get; init; } = "Incognito";
        [IgnoreMember] public string Description { get; init; }
        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; } = TimeSpan.Zero;
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;

        public void OnBuffAdded(Mobile parent)
        {
            parent.BodyMod = Values.body;
            parent.HueMod = Values.bodyHue;
            (parent as PlayerMobile)?.SetHairMods(Values.hair, Values.hairHue, Values.facial, Values.facialHue);
            parent.NameMod = Values.name;
            
            BaseArmor.ValidateMobile(parent);
            BaseClothing.ValidateMobile(parent);
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            if (parent is IEnchanted enchanted)
            {
                parent.BodyMod = 0;
                parent.HueMod = -1;
                (parent as PlayerMobile)?.RemoveHairMods();
                parent.NameMod = null;
                
                BaseArmor.ValidateMobile(parent);
                BaseClothing.ValidateMobile(parent);
                
                enchanted.Enchantments.Remove(this);
                parent.SendLocalizedMessage(1112074); // You have removed your disguise.
            }
        }

        #endregion
    }

    #region EnchantmentInfo

    public class IncognitoInfo : EnchantmentInfo
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