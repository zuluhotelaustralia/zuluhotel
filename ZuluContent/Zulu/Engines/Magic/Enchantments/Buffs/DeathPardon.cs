using System;
using MessagePack;
using Server;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class DeathPardon : Enchantment<DeathPardon.DeathPardonInfo>, IBuff
    {
        [IgnoreMember] public override string AffixName => string.Empty;

        [Key(1)]
        public bool Value
        {
            get;
            set;
        }
        
        public override void OnDeath(Mobile victim, ref bool willDie)
        {
            if (Value)
            {
                willDie = false;
            }
        }

        #region IBuff
        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.GiftOfLife;
        [IgnoreMember] public string Title { get; init; } = "Death Pardon";
        [IgnoreMember] public string Description { get; init; } = "Death pardons your next transgression into the nether realm";
        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = false;
        [IgnoreMember] public TimeSpan Duration { get; init; }
        [IgnoreMember] public DateTime Start { get; init; }

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

        #region EnchantmentInfo

        public class DeathPardonInfo : EnchantmentInfo
        {
            public override string Description { get; protected set; } = "Death Pardon";
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
}