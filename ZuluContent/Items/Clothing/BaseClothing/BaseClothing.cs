using System;
using System.Collections.Generic;
using Scripts.Engines.Magic;
using Server.Engines.Craft;
using Server.Network;
using ZuluContent.Zulu;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public abstract class BaseClothing : Item, IDyable, IScissorable, ICraftable, IWearableDurability, IArmorRating, IMagicEquipItem
    {
        public virtual bool CanFortify
        {
            get { return true; }
        }

        private int m_MaxHitPoints;
        private int m_HitPoints;
        private Mobile m_Crafter;
        protected CraftResource m_Resource;
        private int m_StrReq = -1;
        
        #region Magical Properties
        private MagicalProperties m_MagicProps;

        public MagicalProperties MagicProps
        {
            get => m_MagicProps ??= new MagicalProperties(this);
        }

        #endregion

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorBonus ArmorBonus
        {
            get => MagicProps.GetAttr<ArmorBonus>();
            set
            {
                if (value > ArmorBonus.Adamantium)
                    return;
                
                MagicProps.SetAttr(value);
                Hue = value.GetHue();
                Invalidate();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BaseArmorRating
        {
            get => (int) ArmorBonus;
            set => ArmorBonus = (ArmorBonus)value;
        }

        public double BaseArmorRatingScaled => BaseArmorRating;
        public double ArmorRating => BaseArmorRating;
        public double ArmorRatingScaled => BaseArmorRating;

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
        public Mobile Crafter
        {
            get { return m_Crafter; }
            set { m_Crafter = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrRequirement
        {
            get { return m_StrReq == -1 ? DefaultStrReq : m_StrReq; }
            set { m_StrReq = value; }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get => MagicProps.GetAttr<bool>(MagicProp.Identified);
            set => MagicProps.SetAttr(MagicProp.Identified, value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public ClothingQuality Quality
        {
            get => MagicProps.GetAttr<ClothingQuality>();
            set => MagicProps.SetAttr(value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed
        {
            get => MagicProps.GetAttr<bool>(MagicProp.PlayerConstructed);
            set => MagicProps.SetAttr(MagicProp.PlayerConstructed, value);
        }

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
        
        public virtual int DefaultStrBonus
        {
            get => 0;
        }

        public virtual int DefaultDexBonus
        {
            get => 0;
        }

        public virtual int DefaultIntBonus
        {
            get => 0;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrBonus
        {
            get => MagicProps.TryGetMod(StatType.Str, out MagicStatMod mod) ? mod.Offset : 0;
            set
            {
                if (value == 0 && !MagicProps.HasMod(StatType.Str))
                    return;

                MagicProps.AddMod(new MagicStatMod(StatType.Str, value, Parent));
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DexBonus
        {
            get => MagicProps.TryGetMod(StatType.Dex, out MagicStatMod mod) ? mod.Offset : 0;
            set
            {
                if (value == 0 && !MagicProps.HasMod(StatType.Dex))
                    return;
                
                MagicProps.AddMod(new MagicStatMod(StatType.Dex, value, Parent));
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IntBonus
        {
            get => MagicProps.TryGetMod(StatType.Int, out MagicStatMod mod) ? mod.Offset : 0;
            set
            {
                if (value == 0 && !MagicProps.HasMod(StatType.Int))
                    return;

                MagicProps.AddMod(new MagicStatMod(StatType.Int, value, Parent));
            }
        }
        
        public virtual Race RequiredRace
        {
            get { return null; }
        }

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
                MagicProps.OnMobileEquip();
                m.CheckStatTimers();
            }

            base.OnAdded(parent);
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is Mobile m)
            {
                MagicProps.OnMobileRemoved();
                m.CheckStatTimers();
            }

            base.OnRemoved(parent);
        }

        public virtual int OnHit(BaseWeapon weapon, int damageTaken)
        {
            int Absorbed = Utility.RandomMinMax(1, 4);

            damageTaken -= Absorbed;

            if (damageTaken < 0)
                damageTaken = 0;

            if (25 > Utility.Random(100)) // 25% chance to lower durability
            {
                int wear;

                if (weapon.Type == WeaponType.Bashing)
                    wear = Absorbed / 2;
                else
                    wear = Utility.Random(2);

                if (wear > 0 && m_MaxHitPoints > 0)
                {
                    if (m_HitPoints >= wear)
                    {
                        HitPoints -= wear;
                        wear = 0;
                    }
                    else
                    {
                        wear -= HitPoints;
                        HitPoints = 0;
                    }

                    if (wear > 0)
                    {
                        if (m_MaxHitPoints > wear)
                        {
                            MaxHitPoints -= wear;

                            if (Parent is Mobile)
                                ((Mobile) Parent).LocalOverheadMessage(MessageType.Regular, 0x3B2,
                                    1061121); // Your equipment is severely damaged.
                        }
                        else
                        {
                            Delete();
                        }
                    }
                }
            }

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
            Quality = ClothingQuality.Regular;

            m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);
        }

        public BaseClothing(Serial serial) : base(serial)
        {
        }

        public override bool AllowEquippedCast(Mobile from)
        {
            if (base.AllowEquippedCast(from))
                return true;

            return false;
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
            List<EquipInfoAttribute> attrs = new List<EquipInfoAttribute>();

            AddEquipInfoAttributes(from, attrs);

            int number;

            if (Name == null)
            {
                number = LabelNumber;
            }
            else
            {
                LabelTo(from, Name);
                number = 1041000;
            }

            if (attrs.Count == 0 && Crafter == null && Name != null)
                return;

            EquipmentInfo eqInfo = new EquipmentInfo(number, m_Crafter, false, attrs.ToArray());

            from.Send(new DisplayEquipmentInfo(this, eqInfo));
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

            if (Quality == ClothingQuality.Exceptional)
                attrs.Add(new EquipInfoAttribute(1018305 - (int) Quality));
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
            Quality = 0x00000200,
            StrReq = 0x00000400,
            NewMagicalProperties = 0x00080000,
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 6); // version

            SaveFlag flags = SaveFlag.None;

            SetSaveFlag(ref flags, SaveFlag.NewMagicalProperties, true);

            if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
            {
                SetSaveFlag(ref flags, SaveFlag.PlayerConstructed, PlayerConstructed != false);
                SetSaveFlag(ref flags, SaveFlag.Quality, Quality != ClothingQuality.Regular);
            }

            SetSaveFlag(ref flags, SaveFlag.Resource, m_Resource != DefaultResource);
            SetSaveFlag(ref flags, SaveFlag.MaxHitPoints, m_MaxHitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.HitPoints, m_HitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.Crafter, m_Crafter != null);
            SetSaveFlag(ref flags, SaveFlag.StrReq, m_StrReq != -1);

            writer.WriteEncodedInt((int) flags);
            
            if (GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                m_MagicProps.Serialize(writer);

            if (GetSaveFlag(flags, SaveFlag.Resource))
                writer.WriteEncodedInt((int) m_Resource);

            if (GetSaveFlag(flags, SaveFlag.MaxHitPoints))
                writer.WriteEncodedInt((int) m_MaxHitPoints);

            if (GetSaveFlag(flags, SaveFlag.HitPoints))
                writer.WriteEncodedInt((int) m_HitPoints);

            if (GetSaveFlag(flags, SaveFlag.Crafter))
                writer.Write((Mobile) m_Crafter);

            if (GetSaveFlag(flags, SaveFlag.Quality))
                writer.WriteEncodedInt((int) Quality);

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
                case 6:
                    if (GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                        m_MagicProps = MagicalProperties.Deserialize(reader, this);
                    goto case 5;
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
                        m_Crafter = reader.ReadMobile();

                    if (GetSaveFlag(flags, SaveFlag.Quality))
                        Quality = (ClothingQuality) reader.ReadEncodedInt();
                    else if(!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
                        Quality = ClothingQuality.Regular;

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
                    m_Crafter = reader.ReadMobile();
                    Quality = (ClothingQuality) reader.ReadInt();
                    break;
                }
                case 0:
                {
                    m_Crafter = null;
                    Quality = ClothingQuality.Regular;
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
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack.
                return false;
            }

            CraftSystem system = DefTailoring.CraftSystem;

            CraftItem item = system.CraftItems.SearchFor(GetType());

            if (item != null && item.Resources.Count == 1 && item.Resources.GetAt(0).Amount >= 2)
            {
                try
                {
                    Type resourceType = null;

                    CraftResourceInfo info = CraftResources.GetInfo(m_Resource);

                    if (info != null && info.ResourceTypes.Length > 0)
                        resourceType = info.ResourceTypes[0];

                    if (resourceType == null)
                        resourceType = item.Resources.GetAt(0).ItemType;

                    Item res = (Item) Activator.CreateInstance(resourceType);

                    ScissorHelper(from, res, PlayerConstructed ? item.Resources.GetAt(0).Amount / 2 : 1);

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

        public virtual int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Quality = (ClothingQuality) quality;

            if (makersMark)
                Crafter = from;

            if (DefaultResource != CraftResource.None)
            {
                Type resourceType = typeRes;

                if (resourceType == null)
                    resourceType = craftItem.Resources.GetAt(0).ItemType;

                Resource = CraftResources.GetFromType(resourceType);
            }
            else
            {
                Hue = resHue;
            }

            PlayerConstructed = true;

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return quality;
        }

        #endregion
    }
}