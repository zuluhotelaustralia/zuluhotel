using System;
using Server.Engines.Craft;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public abstract class BaseTinkerItem : Item, ICraftable, IResource
    {
        private CraftResource m_Resource;

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

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        public BaseTinkerItem(int itemID) : base(itemID)
        {
        }

        public BaseTinkerItem(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
            
            ICraftable.Serialize(writer, this);

            writer.WriteEncodedInt((int) m_Resource);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            ICraftable.Deserialize(reader, this);

            m_Resource = (CraftResource) reader.ReadEncodedInt();
        }
        
        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

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

            return mark;
        }
    }
}