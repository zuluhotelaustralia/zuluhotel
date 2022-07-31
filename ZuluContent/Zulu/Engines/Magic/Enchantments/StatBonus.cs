using System;
using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class IntBonus : BaseStatBonus<IntBonusInfo>
    {
        [IgnoreMember]
        public override string AffixName => Value > 0 ? EnchantmentInfo.GetName(Value / 5, Cursed) : string.Empty;
        public IntBonus() : base(StatType.Int) { }
    }
    
    public class IntBonusInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Intelligence";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty},
            {"Apprentice's", "Fool's"},
            {"Adept's", "Simpleton's"},
            {"Wizard's", "Infantile"},
            {"Archmage's", "Senile"},
            {"Magister's", "Demented"},
            {"Oracle's", "Madman's"},
        };
    }
    
    [MessagePackObject]
    public class DexBonus : BaseStatBonus<DexBonusInfo>
    {
        [IgnoreMember]
        public override string AffixName => Value > 0 ? EnchantmentInfo.GetName(Value / 5, Cursed) : string.Empty;
        public DexBonus() : base(StatType.Dex) { }
    }

    public class DexBonusInfo : EnchantmentInfo
    {
        public override string Description { get; protected set; } = "Dexterity";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty},
            {"Cutpurse's", "Heavy"},
            {"Thief's", "Leaden"},
            {"Cat Burglar's", "Encumbering"},
            {"Tumbler's", "Binding"},
            {"Acrobat's", "Fumbling"},
            {"Escape Artist's", "Blundering"},
        };
    }
    
    [MessagePackObject]
    public class StrBonus : BaseStatBonus<StrBonusInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value / 5, Cursed);
        public StrBonus() : base(StatType.Str) { }
    }
    
    public class StrBonusInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Strength";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Prefix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } =
        {
            {string.Empty, string.Empty},
            {"Warrior's", "Weakling's"},
            {"Veteran's", "Enfeebling"},
            {"Champion's", "Powerless"},
            {"Hero's", "Frail"},
            {"Warlord's", "Diseased"},
            {"King's", "Leper's"},
        };
    }

    public abstract class BaseStatBonus<T> : Enchantment<T> where T : EnchantmentInfo, new()
    {
        [Key(2)]protected StatType StatType;
        protected StatMod Mod;
        private int m_Value;
        protected Mobile Mobile;
        protected IEntity Entity;

        [Key(1)]
        public virtual int Value
        {
            get => Cursed > CurseType.None ? -m_Value : m_Value;
            set
            {
                if (value == m_Value || value == 0 && m_Value == 0)
                    return;
                
                AddStatMod(Mobile, Entity);
                m_Value = value;
            }
        }

        protected BaseStatBonus(StatType statType)
        {
            StatType = statType;
        }

        protected virtual string StatModName => $"{GetType().Name}:{StatType.ToString()}";

        protected void AddStatMod(Mobile mobile, IEntity entity)
        {
            if (mobile == null)
                return;
            
            Mod = new StatMod(
                StatType,
                $"{StatModName}:{entity.GetType().Name}",
                Value,
                TimeSpan.Zero
            );
            
            mobile.AddStatMod(Mod);
        }

        public override void OnAdded(IEntity entity, IEntity parent)
        {
            base.OnAdded(entity, parent);

            if (parent is Mobile mobile)
            {
                Mobile = mobile;
                Entity = entity;
                AddStatMod(mobile, entity);
            }
        }

        public override void OnRemoved(IEntity entity, IEntity parent)
        {
            if (parent is Mobile mobile)
            {
                Mobile = null;
                Entity = null;

                if (Mod != null)
                    mobile.RemoveStatMod(Mod.Name);
            }
        }
    }
}