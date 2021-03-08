using System;
using Server.Network;
using Server.Engines.Craft;
using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public interface IUsesRemaining
    {
        int UsesRemaining { get; set; }
        bool ShowUsesRemaining { get; set; }
    }

    public abstract class BaseHarvestTool : Item, IUsesRemaining, ICraftable, IResource
    {
        private CraftResource m_Resource;
        private EnchantmentDictionary m_Enchantments;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set
            {
                if (m_Resource != value)
                {
                    m_Resource = value;

                    if (CraftItem.RetainsColor(GetType()))
                    {
                        Hue = CraftResources.GetHue(m_Resource);
                    }
                }
            }
        }

        public EnchantmentDictionary Enchantments
        {
            get => m_Enchantments ??= new EnchantmentDictionary();
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => Enchantments.Get((ItemMark e) => (MarkQuality) e.Value);
            set
            {
                UnscaleUses();
                Enchantments.Set((ItemMark e) => e.Value = (int) value);
                ScaleUses();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining { get; set; }

        public void ScaleUses()
        {
            UsesRemaining = UsesRemaining * GetUsesScalar() / 100;
        }

        public void UnscaleUses()
        {
            UsesRemaining = UsesRemaining * 100 / GetUsesScalar();
        }

        public int GetUsesScalar()
        {
            if (Mark == MarkQuality.Exceptional)
                return 200;

            return 100;
        }

        public bool ShowUsesRemaining
        {
            get { return true; }
            set { }
        }

        public abstract HarvestSystem HarvestSystem { get; }

        public BaseHarvestTool(int itemID) : this(50, itemID)
        {
        }

        public BaseHarvestTool(int usesRemaining, int itemID) : base(itemID)
        {
            UsesRemaining = usesRemaining;
            Mark = MarkQuality.Regular;
        }

        public virtual void DisplayDurabilityTo(Mobile m)
        {
            LabelToAffix(m, 1017323, AffixType.Append, ": " + UsesRemaining.ToString()); // Durability
        }

        public override void OnSingleClick(Mobile from)
        {
            DisplayDurabilityTo(from);

            HandleSingleClick(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
                HarvestSystem.BeginHarvesting(from, this);
            else
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
        }

        public BaseHarvestTool(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            ICraftable.Serialize(writer, this);

            Enchantments.Serialize(writer);

            writer.WriteEncodedInt((int) m_Resource);

            writer.Write((int) UsesRemaining);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                {
                    ICraftable.Deserialize(reader, this);

                    m_Enchantments = EnchantmentDictionary.Deserialize(reader);

                    m_Resource = (CraftResource) reader.ReadEncodedInt();

                    goto case 0;
                }
                case 0:
                {
                    UsesRemaining = reader.ReadInt();
                    break;
                }
            }
        }

        #region ICraftable Members

        public int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes, BaseTool tool,
            CraftItem craftItem, int resHue)
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

            return mark;
        }

        #endregion
    }
}