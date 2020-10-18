using System;
using System.Linq;
using Server;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Items
{
    public abstract class BaseEquippableItem : Item, IMagicItem, IElementalResistible
    {
        private EnchantmentDictionary m_Enchantments;
        public EnchantmentDictionary Enchantments
        {
            get => m_Enchantments ??= new EnchantmentDictionary();
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get => Enchantments.Identified;
            set
            {
                if (!Enchantments.Identified && value) 
                    Enchantments.OnIdentified(this);
                
                Enchantments.Identified = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalWaterResist
        {
            get => Enchantments.Get((WaterProtection e) => e.Value);
            set => Enchantments.Set((WaterProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalAirResist
        {
            get => Enchantments.Get((AirProtection e) => e.Value);
            set => Enchantments.Set((AirProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalPhysicalResist 
        {
            get => Enchantments.Get((PhysicalProtection e) => e.Value);
            set => Enchantments.Set((PhysicalProtection e) => e.Value = value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalFireResist
        {
            get => Enchantments.Get((FireProtection e) => e.Value);
            set => Enchantments.Set((FireProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalPoisonResist
        {
            get => Enchantments.Get((PoisonProtection e) => e.Value);
            set => Enchantments.Set((PoisonProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalEarthResist
        {
            get => Enchantments.Get((EarthProtection e) => e.Value);
            set => Enchantments.Set((EarthProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalNecroResist
        {
            get => Enchantments.Get((NecroProtection e) => e.Value);
            set => Enchantments.Set((NecroProtection e) => e.Value = value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int StrBonus
        {
            get => Enchantments.Get((StrBonus e) => e.Value);
            set => Enchantments.Set((StrBonus e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DexBonus
        {
            get => Enchantments.Get((DexBonus e) => e.Value);
            set => Enchantments.Set((DexBonus e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IntBonus
        {
            get => Enchantments.Get((IntBonus e) => e.Value);
            set => Enchantments.Set((IntBonus e) => e.Value = value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName FirstSkillBonusName
        {
            get => Enchantments.Get((FirstSkillBonus e) => e.Skill);
            set => Enchantments.Set((FirstSkillBonus e) => e.Skill = value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public double FirstSkillBonusValue
        {
            get => Enchantments.Get((FirstSkillBonus e) => e.Value);
            set => Enchantments.Set((FirstSkillBonus e) => e.Value = value);
        }

        
        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName SecondSkillBonusName
        {
            get => Enchantments.Get((SecondSkillBonus e) => e.Skill);
            set => Enchantments.Set((SecondSkillBonus e) => e.Skill = value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public double SecondSkillBonusValue
        {
            get => Enchantments.Get((SecondSkillBonus e) => e.Value);
            set => Enchantments.Set((SecondSkillBonus e) => e.Value = value);
        }

        public BaseEquippableItem(Serial serial): base(serial)
        {
        }

        protected BaseEquippableItem(int itemId) : base(itemId)
        {
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);
            Enchantments.FireHook(e => e.OnAdded(this));
        }

        public override void OnRemoved(IEntity parent)
        {
            Enchantments.FireHook(e => e.OnRemoved(this));
            base.OnRemoved(parent);
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            Enchantments.Serialize(writer);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            m_Enchantments = EnchantmentDictionary.Deserialize(reader);
            if(Parent is Mobile m)
                m_Enchantments.FireHook(e => e.OnAdded(this));
        }
    }
}