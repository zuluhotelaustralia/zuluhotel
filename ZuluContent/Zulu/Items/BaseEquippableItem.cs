using System;
using System.Linq;
using Server;
using Server.Engines.Magic;
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
            set => Enchantments.Identified = value;
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
            get => Enchantments.Get((PhysicalProtection e) => e.Value);
            set => Enchantments.Set((PhysicalProtection e) => e.Value = value);
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
        

        public BaseEquippableItem(Serial serial): base(serial)
        {
        }

        protected BaseEquippableItem(int itemId) : base(itemId)
        {
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);
            if (parent is Mobile m) 
                Enchantments.OnEquip(this, m);
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is Mobile m) 
                Enchantments.OnRemoved(this, m);
            
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
                m_Enchantments.OnEquip(this, m);
        }
    }
}