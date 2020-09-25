using System;
using Server.Engines.Craft;
using Server.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    public enum GemType
    {
        None,
        StarSapphire,
        Emerald,
        Sapphire,
        Ruby,
        Citrine,
        Amethyst,
        Tourmaline,
        Amber,
        Diamond
    }

    public abstract class BaseJewel : Item, ICraftable, IArmorRating, IMagicEquipItem, IElementalResistible
    {
        private int m_HitPoints;

        private CraftResource m_Resource;

        #region Magical Properties

        private MagicalProperties m_MagicProps;

        public MagicalProperties MagicProps
        {
            get => m_MagicProps ??= new MagicalProperties(this);
        }

        #endregion
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalWaterResist
        {
            get => MagicProps.GetResist(ElementalType.Water);
            set => MagicProps.SetResist(ElementalType.Water, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalAirResist
        {
            get => MagicProps.GetResist(ElementalType.Air);
            set => MagicProps.SetResist(ElementalType.Air, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalPhysicalResist
        {
            get => MagicProps.GetResist(ElementalType.Physical);
            set => MagicProps.SetResist(ElementalType.Physical, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalFireResist
        {
            get => MagicProps.GetResist(ElementalType.Fire);
            set => MagicProps.SetResist(ElementalType.Fire, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalColdResist
        {
            get => MagicProps.GetResist(ElementalType.Cold);
            set => MagicProps.SetResist(ElementalType.Cold, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalPoisonResist
        {
            get => MagicProps.GetResist(ElementalType.Poison);
            set => MagicProps.SetResist(ElementalType.Poison, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalEnergyResist
        {
            get => MagicProps.GetResist(ElementalType.Energy);
            set => MagicProps.SetResist(ElementalType.Energy, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalEarthResist
        {
            get => MagicProps.GetResist(ElementalType.Earth);
            set => MagicProps.SetResist(ElementalType.Earth, value);
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalNecroResist
        {
            get => MagicProps.GetResist(ElementalType.Necro);
            set => MagicProps.SetResist(ElementalType.Necro, value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArmorBonus ArmorBonus
        {
            get => MagicProps.GetAttr(MagicProp.ArmorBonus, ArmorBonus.None);
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
        public int MaxHitPoints { get; set; }

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
        public CraftResource Resource
        {
            get { return m_Resource; }
            set
            {
                m_Resource = value;
                Hue = CraftResources.GetHue(m_Resource);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public GemType GemType { get; set; }

        public virtual int BaseGemTypeNumber => 0;

        public virtual int InitMinHits => 0;

        public virtual int InitMaxHits => 0;

        public override int LabelNumber
        {
            get
            {
                if (GemType == GemType.None)
                    return base.LabelNumber;

                return BaseGemTypeNumber + (int) GemType - 1;
            }
        }

        public BaseJewel(int itemID, Layer layer) : base(itemID)
        {
            m_Resource = CraftResource.Iron;
            GemType = GemType.None;

            Layer = layer;

            m_HitPoints = MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);
        }

        public BaseJewel(Serial serial) : base(serial)
        {
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

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            
            writer.Write((int) 4); // version

            MagicProps.Serialize(writer);

            writer.WriteEncodedInt((int) MaxHitPoints);
            writer.WriteEncodedInt((int) m_HitPoints);

            writer.WriteEncodedInt((int) m_Resource);
            writer.WriteEncodedInt((int) GemType);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            
            int version = reader.ReadInt();

            switch (version)
            {
                case 4:
                    m_MagicProps = MagicalProperties.Deserialize(reader, this);
                    goto case 3;
                case 3:
                {
                    MaxHitPoints = reader.ReadEncodedInt();
                    m_HitPoints = reader.ReadEncodedInt();

                    goto case 2;
                }
                case 2:
                {
                    m_Resource = (CraftResource) reader.ReadEncodedInt();
                    GemType = (GemType) reader.ReadEncodedInt();

                    goto case 1;
                }
                case 1:
                {
                    if (Parent is Mobile mobile)
                        mobile.CheckStatTimers();

                    break;
                }
                case 0:
                {
                    break;
                }
            }

            if (version < 2)
            {
                m_Resource = CraftResource.Iron;
                GemType = GemType.None;
            }
        }

        #region ICraftable Members

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Type resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources.GetAt(0).ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            if (1 < craftItem.Resources.Count)
            {
                resourceType = craftItem.Resources.GetAt(1).ItemType;

                if (resourceType == typeof(StarSapphire))
                    GemType = GemType.StarSapphire;
                else if (resourceType == typeof(Emerald))
                    GemType = GemType.Emerald;
                else if (resourceType == typeof(Sapphire))
                    GemType = GemType.Sapphire;
                else if (resourceType == typeof(Ruby))
                    GemType = GemType.Ruby;
                else if (resourceType == typeof(Citrine))
                    GemType = GemType.Citrine;
                else if (resourceType == typeof(Amethyst))
                    GemType = GemType.Amethyst;
                else if (resourceType == typeof(Tourmaline))
                    GemType = GemType.Tourmaline;
                else if (resourceType == typeof(Amber))
                    GemType = GemType.Amber;
                else if (resourceType == typeof(Diamond))
                    GemType = GemType.Diamond;
            }

            return 1;
        }

        #endregion
    }
}