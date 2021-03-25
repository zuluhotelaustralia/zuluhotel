using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.First;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class NightSight : Enchantment<NightSightInfo>, IBuff
    {
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public int Value { get; init; }

        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.NightSight;
        [IgnoreMember] public string Title { get; init; } = "Night Sight";
        [IgnoreMember] public string Description { get; init; }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public TimeSpan Duration { get; init; } = TimeSpan.Zero;
        [IgnoreMember] public DateTime Start { get; init; } = DateTime.UtcNow;

        public void OnBuffAdded(Mobile parent)
        {
            parent.LightLevel = Value;
            (parent as IEnchanted)?.Enchantments.Set(this);
        }

        public void OnBuffRemoved(Mobile parent)
        {
            parent.LightLevel = 0;
            (parent as IEnchanted)?.Enchantments.Remove(this);
        }

        #endregion
    }

    #region EnchantmentInfo

    public class NightSightInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Night Sight";
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