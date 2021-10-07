using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.ContextMenus;
using Server.Engines.Magic;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items.SingleClick;

namespace ZuluContent.Zulu.Items
{
    public abstract class BaseEquippableItem : Item, IMagicItem, IElementalResistible
    {
        private EnchantmentDictionary m_Enchantments;

        public EnchantmentDictionary Enchantments
        {
            get => m_Enchantments ??= new EnchantmentDictionary();
            protected set => m_Enchantments = value;
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
        
        [Hue]
        [CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get => Identified ? base.Hue : 0x0;
            set => base.Hue = value;
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public CurseType Cursed
        {
            get => Enchantments.Values.Count > 0
                    ? Enchantments.Values.First().Value.Cursed
                    : CurseType.None;
            set
            {
                Movable = value switch
                {
                    < CurseType.RevealedCantUnEquip => true,
                    CurseType.RevealedCantUnEquip => false,
                };

                foreach (var (_, val) in Enchantments.Values)
                    val.Cursed = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WaterResist
        {
            get => Enchantments.Get((WaterProtection e) => e.Value);
            set => Enchantments.Set((WaterProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int AirResist
        {
            get => Enchantments.Get((AirProtection e) => e.Value);
            set => Enchantments.Set((AirProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int PhysicalResist
        {
            get => Enchantments.Get((PhysicalProtection e) => e.Value);
            set => Enchantments.Set((PhysicalProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FireResist
        {
            get => Enchantments.Get((FireProtection e) => e.Value);
            set => Enchantments.Set((FireProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public PoisonLevel PoisonImmunity
        {
            get => Enchantments.Get((PoisonProtection e) => e.Value);
            set => Enchantments.Set((PoisonProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int EarthResist
        {
            get => Enchantments.Get((EarthProtection e) => e.Value);
            set => Enchantments.Set((EarthProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int NecroResist
        {
            get => Enchantments.Get((NecroProtection e) => e.Value);
            set => Enchantments.Set((NecroProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ParalysisProtection
        {
            get => Enchantments.Get((ParalysisProtection e) => e.Value);
            set => Enchantments.Set((ParalysisProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HealingBonus
        {
            get => Enchantments.Get((HealingBonus e) => e.Value);
            set => Enchantments.Set((HealingBonus e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SpellCircle MagicImmunity
        {
            get => Enchantments.Get((MagicImmunity e) => e.Value);
            set => Enchantments.Set((MagicImmunity e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SpellCircle MagicReflection
        {
            get => Enchantments.Get((MagicReflection e) => e.Value);
            set => Enchantments.Set((MagicReflection e) => e.Value = value);
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
        public double MagicEfficiencyPenalty
        {
            get => Enchantments.Get((MagicEfficiencyPenalty e) => e.Value);
            set => Enchantments.Set((MagicEfficiencyPenalty e) => e.Value = value);
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
        
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from.Alive && Identified == false)
            {
                list.Add(new IdentifyEntry(from, this));
            }
        }

        public BaseEquippableItem(Serial serial) : base(serial)
        {
        }

        protected BaseEquippableItem(int itemId) : base(itemId)
        {
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);
            
            if (parent is Mobile mobile && Cursed == CurseType.Unrevealed)
            {
                Cursed = CurseType.RevealedCantUnEquip;
                mobile.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                mobile.PlaySound(0x1E1);
                mobile.SendAsciiMessage(33,
                    $"That item is cursed, and reveals itself to be a {SingleClickHandler.GetMagicItemName(this)}");    
            }
            
            Enchantments.FireHook(e => e.OnAdded(this, parent));
        }

        public override void OnRemoved(IEntity parent)
        {
            Enchantments.FireHook(e => e.OnRemoved(this, parent));
            base.OnRemoved(parent);
        }

        public override bool OnDragLift(Mobile from)
        {
            bool canLift = true;
            Enchantments.FireHook(e => e.OnBeforeRemoved(this, from, ref canLift));

            if (Cursed == CurseType.RevealedCantUnEquip && Parent is Mobile parent && parent == from)
                canLift = Movable = false;

            return canLift;
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
            if (Parent is Mobile mobile)
                m_Enchantments.FireHook(e => e.OnAdded(this, mobile));
        }
    }
}