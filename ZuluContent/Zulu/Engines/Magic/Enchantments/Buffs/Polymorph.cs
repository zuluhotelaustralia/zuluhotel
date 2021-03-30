using System;
using MessagePack;
using Server;
using Server.Items;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    [MessagePackObject]
    public class Polymorph : Enchantment<PolymorphInfo>, IBuff, IArmorMod
    {
        [IgnoreMember] public override string AffixName => string.Empty;
        [IgnoreMember] private StatMod[] m_Mods;
        [Key(1)] public (int StrMod, int DexMod, int IntMod) StatMods { get; init; }
        [Key(2)] public (int body, int bodyHue) BodyMods { get; init; }
        [Key(3)] public int ArmorMod { get; init; }
        
        #region IBuff

        [IgnoreMember] public BuffIcon Icon { get; init; } = BuffIcon.Polymorph;
        [IgnoreMember] public string Title { get; init; } = "Polymorph";
        [IgnoreMember] public virtual string Description { get; init; }

        [IgnoreMember] public string[] Details { get; init; }
        [IgnoreMember] public bool ExpireOnDeath { get; init; } = true;
        [IgnoreMember] public bool Dispellable { get; init; } = true;
        [IgnoreMember] public virtual TimeSpan Duration { get; init; } = TimeSpan.Zero;
        [IgnoreMember] public virtual DateTime Start { get; init; } = DateTime.UtcNow;

        public virtual void OnBuffAdded(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Set(this);
            
            parent.BodyMod = BodyMods.body;
            parent.HueMod = BodyMods.bodyHue;

            m_Mods = new[]
            {
                new StatMod(StatType.Str, $"{GetType().Name}:{StatType.Str}", StatMods.StrMod, TimeSpan.Zero),
                new StatMod(StatType.Dex, $"{GetType().Name}:{StatType.Dex}", StatMods.DexMod, TimeSpan.Zero),
                new StatMod(StatType.Int, $"{GetType().Name}:{StatType.Int}", StatMods.IntMod, TimeSpan.Zero),
            };

            foreach (var mod in m_Mods) 
                parent.AddStatMod(mod);

            parent.Delta(MobileDelta.Armor);
            
            BaseArmor.ValidateMobile(parent);
            BaseClothing.ValidateMobile(parent);
        }

        public virtual void OnBuffRemoved(Mobile parent)
        {
            (parent as IEnchanted)?.Enchantments.Remove(this);
            
            parent.BodyMod = 0;
            parent.HueMod = -1;

            foreach (var mod in m_Mods) 
                parent.RemoveStatMod(mod.Name);

            parent.Delta(MobileDelta.Armor);
            
            BaseArmor.ValidateMobile(parent);
            BaseClothing.ValidateMobile(parent);
        }

        #endregion

    }

    #region EnchantmentInfo

    public class PolymorphInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Polymorph";
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