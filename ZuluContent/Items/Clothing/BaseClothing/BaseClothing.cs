using System;
using System.Collections.Generic;
using Scripts.Zulu.Utilities;
using Server.Engines.Craft;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public abstract class BaseClothing : BaseEquippableItem, IDyable, IScissorable, ICraftable, IWearableDurability,
        IArmorRating, IRepairable
    {
        public virtual bool CanFortify => true;

        private int m_MaxHitPoints;
        private int m_HitPoints;
        protected CraftResource m_Resource;
        private int m_StrReq = -1;
        private int m_ArmorBase = -1;

        public virtual int ArmorBase => 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorBonusType ArmorBonusType
        {
            get => Enchantments.Get((ArmorBonus e) => e.Value);
            set
            {
                if (value > ArmorBonusType.Adamantium)
                    return;

                Enchantments.Set((ArmorBonus e) => e.Value = value);
                Invalidate();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BaseArmorRating
        {
            get { return m_ArmorBase == -1 ? ArmorBase : m_ArmorBase; }
            set
            {
                m_ArmorBase = value;
                Invalidate();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BonusArmorRating
        {
            get => (int) ArmorBonusType;
            set => ArmorBonusType = (ArmorBonusType) value;
        }

        public double BaseArmorRatingScaled => (BaseArmorRating * ArmorScalar) + BonusArmorRating;

        public double ArmorRating
        {
            get
            {
                var ar = BaseArmorRating;

                ar = (int) (ar * Quality);

                return ar;
            }
        }

        public double ArmorRatingScaled => (ArmorRating * ArmorScalar) + BonusArmorRating;

        public virtual double ArmorScalar => 0.5;

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxHitPoints
        {
            get { return m_MaxHitPoints; }
            set { m_MaxHitPoints = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitPoints
        {
            get { return m_HitPoints; }
            set
            {
                if (value != m_HitPoints && MaxHitPoints > 0)
                {
                    m_HitPoints = value;

                    if (m_HitPoints < 0)
                        Delete();
                    else if (m_HitPoints > MaxHitPoints)
                        m_HitPoints = MaxHitPoints;
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrRequirement
        {
            get { return m_StrReq == -1 ? DefaultStrReq : m_StrReq; }
            set { m_StrReq = value; }
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => Enchantments.Get((ItemMark e) => (MarkQuality) e.Value);
            set => Enchantments.Set((ItemMark e) => e.Value = (int) value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double Quality
        {
            get => Enchantments.Get((ItemQuality e) => e.Value);
            set => Enchantments.Set((ItemQuality e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }

        public virtual CraftResource DefaultResource
        {
            get { return CraftResource.None; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set
            {
                m_Resource = value;
                Hue = CraftResources.GetHue(m_Resource);
            }
        }

        public virtual int DefaultStrBonus => 0;

        public virtual int DefaultDexBonus => 0;

        public virtual int DefaultIntBonus => 0;

        public virtual Race RequiredRace => null;

        public override bool CanEquip(Mobile from)
        {
            if (RequiredRace != null && from.Race != RequiredRace)
            {
                if (RequiredRace == Race.Elf)
                    from.SendLocalizedMessage(1072203); // Only Elves may use this.
                else
                    from.SendMessage("Only {0} may use this.", RequiredRace.PluralName);

                return false;
            }
            else if (!AllowMaleWearer && !from.Female)
            {
                if (AllowFemaleWearer)
                    from.SendLocalizedMessage(1010388); // Only females can wear this.
                else
                    from.SendMessage("You may not wear this.");

                return false;
            }
            else if (!AllowFemaleWearer && from.Female)
            {
                if (AllowMaleWearer)
                    from.SendLocalizedMessage(1063343); // Only males can wear this.
                else
                    from.SendMessage("You may not wear this.");

                return false;
            }
            else
            {
                int strBonus = ComputeStatBonus(StatType.Str);
                int strReq = ComputeStatReq(StatType.Str);

                if (from.Str < strReq || @from.Str + strBonus < 1)
                {
                    from.SendLocalizedMessage(500213); // You are not strong enough to equip that.
                    return false;
                }
            }


            return base.CanEquip(from);
        }

        public virtual int DefaultStrReq
        {
            get { return 0; }
        }

        public virtual int InitMinHits
        {
            get { return 0; }
        }

        public virtual int InitMaxHits
        {
            get { return 0; }
        }

        public virtual bool AllowMaleWearer
        {
            get { return true; }
        }

        public virtual bool AllowFemaleWearer
        {
            get { return true; }
        }

        public virtual bool CanBeBlessed
        {
            get { return true; }
        }

        public int ComputeStatReq(StatType type)
        {
            int v = StrRequirement;

            return v;
        }

        public int ComputeStatBonus(StatType type)
        {
            return type switch
            {
                StatType.Str => StrBonus,
                StatType.Dex => DexBonus,
                StatType.Int => IntBonus,
                _ => 0
            };
        }

        public static void ValidateMobile(Mobile m)
        {
            for (int i = m.Items.Count - 1; i >= 0; --i)
            {
                if (i >= m.Items.Count)
                    continue;

                Item item = m.Items[i];

                if (item is BaseClothing clothing)
                {
                    if (clothing.RequiredRace != null && m.Race != clothing.RequiredRace)
                    {
                        if (clothing.RequiredRace == Race.Elf)
                            m.SendLocalizedMessage(1072203); // Only Elves may use this.
                        else
                            m.SendMessage("Only {0} may use this.", clothing.RequiredRace.PluralName);

                        m.AddToBackpack(clothing);
                    }
                    else if (!clothing.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.GameMaster)
                    {
                        if (clothing.AllowFemaleWearer)
                            m.SendLocalizedMessage(1010388); // Only females can wear this.
                        else
                            m.SendMessage("You may not wear this.");

                        m.AddToBackpack(clothing);
                    }
                    else if (!clothing.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.GameMaster)
                    {
                        if (clothing.AllowMaleWearer)
                            m.SendLocalizedMessage(1063343); // Only males can wear this.
                        else
                            m.SendMessage("You may not wear this.");

                        m.AddToBackpack(clothing);
                    }
                }
            }
        }

        protected void Invalidate()
        {
            if (Parent is Mobile mp)
                mp.Delta(MobileDelta.Armor); // Tell them armor rating has changed
        }

        public override void OnAdded(IEntity parent)
        {
            if (parent is Mobile m)
            {
                m.CheckStatTimers();
            }

            base.OnAdded(parent);
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is Mobile m)
            {
                m.CheckStatTimers();
            }

            base.OnRemoved(parent);
        }

        public virtual double OnHit(BaseWeapon weapon, double damageTaken)
        {
            return damageTaken;
        }

        public BaseClothing(int itemID, Layer layer, int hue) : base(itemID)
        {
            StrBonus = DefaultStrBonus;
            DexBonus = DefaultDexBonus;
            IntBonus = DefaultIntBonus;

            Layer = layer;
            Hue = hue;

            m_Resource = DefaultResource;
            Mark = MarkQuality.Regular;

            m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);
        }

        public BaseClothing(Serial serial) : base(serial)
        {
        }

        public override bool CheckPropertyConflict(Mobile m)
        {
            if (base.CheckPropertyConflict(m))
                return true;

            if (Layer == Layer.Pants)
                return m.FindItemOnLayer(Layer.InnerLegs) != null;

            if (Layer == Layer.Shirt)
                return m.FindItemOnLayer(Layer.InnerTorso) != null;

            return false;
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        public virtual void AddEquipInfoAttributes(Mobile from, List<EquipInfoAttribute> attrs)
        {
            if (DisplayLootType)
            {
                if (LootType == LootType.Blessed)
                    attrs.Add(new EquipInfoAttribute(1038021)); // blessed
                else if (LootType == LootType.Cursed)
                    attrs.Add(new EquipInfoAttribute(1049643)); // cursed
            }

            if (Mark == MarkQuality.Exceptional)
                attrs.Add(new EquipInfoAttribute(1018305 - (int) Mark));
        }

        #region Serialization

        private static void SetSaveFlag(ref SaveFlag flags, SaveFlag toSet, bool setIf)
        {
            if (setIf)
                flags |= toSet;
        }

        private static bool GetSaveFlag(SaveFlag flags, SaveFlag toGet)
        {
            return (flags & toGet) != 0;
        }

        [Flags]
        private enum SaveFlag
        {
            None = 0x00000000,
            Resource = 0x00000001,
            Attributes = 0x00000002,
            ClothingAttributes = 0x00000004,
            SkillBonuses = 0x00000008,
            Resistances = 0x00000010,
            MaxHitPoints = 0x00000020,
            HitPoints = 0x00000040,
            PlayerConstructed = 0x00000080,
            Crafter = 0x00000100,
            Mark = 0x00000200,
            StrReq = 0x00000400,
            NewMagicalProperties = 0x00080000,
            ICraftable = 0x00100000,
            ArmorBase = 0x00110000,
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 8); // version

            SaveFlag flags = SaveFlag.None;

            SetSaveFlag(ref flags, SaveFlag.ICraftable, true);
            SetSaveFlag(ref flags, SaveFlag.NewMagicalProperties, true);
            SetSaveFlag(ref flags, SaveFlag.ArmorBase, true);

            if (!GetSaveFlag(flags, SaveFlag.ICraftable))
            {
                SetSaveFlag(ref flags, SaveFlag.PlayerConstructed, PlayerConstructed);
                SetSaveFlag(ref flags, SaveFlag.Crafter, Crafter != null);
            }

            if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
            {
                SetSaveFlag(ref flags, SaveFlag.Mark, Mark != MarkQuality.Regular);
            }

            SetSaveFlag(ref flags, SaveFlag.Resource, m_Resource != DefaultResource);
            SetSaveFlag(ref flags, SaveFlag.MaxHitPoints, m_MaxHitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.HitPoints, m_HitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.StrReq, m_StrReq != -1);

            writer.WriteEncodedInt((int) flags);

            if (GetSaveFlag(flags, SaveFlag.ArmorBase))
                writer.WriteEncodedInt(m_ArmorBase);

            if (GetSaveFlag(flags, SaveFlag.ICraftable))
                ICraftable.Serialize(writer, this);

            if (GetSaveFlag(flags, SaveFlag.Resource))
                writer.WriteEncodedInt((int) m_Resource);

            if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                writer.WriteEncodedInt((int) m_MaxHitPoints);

            if (GetSaveFlag(flags, SaveFlag.HitPoints))
                writer.WriteEncodedInt((int) m_HitPoints);

            if (GetSaveFlag(flags, SaveFlag.Crafter))
                writer.Write((Mobile) Crafter);

            if (GetSaveFlag(flags, SaveFlag.Mark))
                writer.WriteEncodedInt((int) Mark);

            if (GetSaveFlag(flags, SaveFlag.StrReq))
                writer.WriteEncodedInt((int) m_StrReq);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            SaveFlag flags = (SaveFlag) reader.ReadEncodedInt();

            switch (version)
            {
                case 8:
                    if (GetSaveFlag(flags, SaveFlag.ArmorBase))
                        m_ArmorBase = reader.ReadEncodedInt();
                    goto case 7;
                case 7:
                    if (GetSaveFlag(flags, SaveFlag.ICraftable))
                        ICraftable.Deserialize(reader, this);
                    goto case 6;
                case 6:
                case 5:
                {
                    if (GetSaveFlag(flags, SaveFlag.Resource))
                        m_Resource = (CraftResource) reader.ReadEncodedInt();
                    else
                        m_Resource = DefaultResource;

                    if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                        m_MaxHitPoints = reader.ReadEncodedInt();

                    if (GetSaveFlag(flags, SaveFlag.HitPoints))
                        m_HitPoints = reader.ReadEncodedInt();

                    if (GetSaveFlag(flags, SaveFlag.Crafter))
                        Crafter = reader.ReadEntity<Mobile>();

                    if (GetSaveFlag(flags, SaveFlag.Mark))
                        Mark = (MarkQuality) reader.ReadEncodedInt();
                    else if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                        Mark = MarkQuality.Regular;

                    if (GetSaveFlag(flags, SaveFlag.StrReq))
                        m_StrReq = reader.ReadEncodedInt();
                    else
                        m_StrReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.PlayerConstructed))
                        PlayerConstructed = true;

                    break;
                }
                case 4:
                {
                    m_Resource = (CraftResource) reader.ReadInt();

                    goto case 3;
                }
                case 3:
                case 2:
                {
                    PlayerConstructed = reader.ReadBool();
                    goto case 1;
                }
                case 1:
                {
                    Crafter = reader.ReadEntity<Mobile>();
                    Mark = (MarkQuality) reader.ReadInt();
                    break;
                }
                case 0:
                {
                    Crafter = null;
                    Mark = MarkQuality.Regular;
                    break;
                }
            }

            if (version < 2)
                PlayerConstructed = true; // we don't know, so, assume it's crafted

            if (version < 4)
                m_Resource = DefaultResource;

            if (m_MaxHitPoints == 0 && m_HitPoints == 0)
                m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

            Mobile parent = Parent as Mobile;

            parent?.CheckStatTimers();
        }

        #endregion

        public virtual bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;
            else if (RootParent is Mobile && from != RootParent)
                return false;

            Hue = sender.DyedHue;

            return true;
        }

        public virtual bool Scissor(Mobile from, Scissors scissors)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendFailureMessage(502437); // Items you wish to cut must be in your backpack.
                return false;
            }

            CraftSystem system = DefTailoring.CraftSystem;

            CraftItem item = system.CraftItems.SearchFor(GetType());

            if (item != null && item.Resources.Count == 1 && item.Resources[0].Amount >= 2)
            {
                try
                {
                    Type resourceType = null;

                    CraftResourceInfo info = CraftResources.GetInfo(m_Resource);

                    if (info != null && info.ResourceTypes.Length > 0)
                        resourceType = info.ResourceTypes[0];

                    if (resourceType == null)
                        resourceType = item.Resources[0].ItemType;

                    Item res = (Item) Activator.CreateInstance(resourceType);

                    ScissorHelper(from, res, PlayerConstructed ? item.Resources[0].Amount / 2 : 1);

                    res.LootType = LootType.Regular;

                    return true;
                }
                catch
                {
                }
            }

            from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
            return false;
        }

        #region ICraftable Members

        public virtual int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (MarkQuality) mark;

            if (makersMark)
                Crafter = from;

            if (DefaultResource != CraftResource.None)
            {
                Type resourceType = typeRes;

                if (resourceType == null)
                    resourceType = craftItem.Resources[0].ItemType;

                Resource = CraftResources.GetFromType(resourceType);
            }
            else
            {
                Hue = resHue;
            }

            PlayerConstructed = true;

            var resEnchantments = CraftResources.GetEnchantments(Resource);
            if (resEnchantments != null)
            {
                foreach (var (key, value) in resEnchantments)
                {
                    Enchantments.SetFromResourceType(key, value);
                }
            }

            Quality = quality;

            MaxHitPoints = (int) (MaxHitPoints * quality);
            HitPoints = MaxHitPoints;

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return mark;
        }

        #endregion
    }
}