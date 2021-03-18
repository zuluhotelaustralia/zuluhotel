using System;
using Server.Engines.Harvest;
using Server.Engines.Craft;
using Server.Network;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public class FishingPole : Item, ICraftable, IResource
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
        
        [Constructible]
        public FishingPole() : base(0x0DC0)
        {
            Layer = Layer.TwoHanded;
            Weight = 8.0;
        }
        
        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            Point3D loc = GetWorldLocation();

            if (!from.InLOS(loc) || !from.InRange(loc, 2))
                from.LocalOverheadMessage(MessageType.Regular, 0x3E9, 1019045); // I can't reach that
            else
                Fishing.System.BeginHarvesting(from, this);
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

        public override bool CheckConflictingLayer(Mobile m, Item item, Layer layer)
        {
            if (base.CheckConflictingLayer(m, item, layer))
                return true;

            if (layer == Layer.OneHanded)
            {
                m.SendLocalizedMessage(500214); // You already have something in both hands.
                return true;
            }

            return false;
        }

        [Constructible]
        public FishingPole(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version
            
            ICraftable.Serialize(writer, this);

            writer.WriteEncodedInt((int) m_Resource);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 2)
            {
                ICraftable.Deserialize(reader, this);

                m_Resource = (CraftResource) reader.ReadEncodedInt();
            }

            if (version < 1 && Layer == Layer.OneHanded)
                Layer = Layer.TwoHanded;
        }
    }
}