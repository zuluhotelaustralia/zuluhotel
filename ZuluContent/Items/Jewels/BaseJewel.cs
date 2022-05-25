using System;
using Server.Engines.Craft;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

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

    public abstract class BaseJewel : BaseEquippableItem, ICraftable, IArmorRating, IResource
    {
        private int m_HitPoints;

        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }
        
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
            get => (int) ArmorBonusType;
            set => ArmorBonusType = (ArmorBonusType) value;
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
        public MarkQuality Mark { get; set; }

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

            Mark = MarkQuality.Regular;

            m_HitPoints = MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);
        }

        public BaseJewel(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
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

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(5); // version

            ICraftable.Serialize(writer, this);

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
                case 5:
                    ICraftable.Deserialize(reader, this);
                    goto case 4;
                case 4:
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

        public int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (MarkQuality) mark;

            if (makersMark)
                Crafter = from;

            var resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources[0].ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            PlayerConstructed = true;

            var context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            if (1 < craftItem.Resources.Count)
            {
                resourceType = craftItem.Resources[1].ItemType;

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