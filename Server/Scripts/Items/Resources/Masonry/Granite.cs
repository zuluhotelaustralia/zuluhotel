using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public abstract class BaseGranite : Item
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set { m_Resource = value; InvalidateProperties(); }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                case 0:
                    {
                        m_Resource = (CraftResource)reader.ReadInt();
                        break;
                    }
            }

            if (version < 1)
                Stackable = Core.ML;
        }

        public override double DefaultWeight
        {
            get { return Core.ML ? 1.0 : 10.0; } // Pub 57
        }

        public BaseGranite(CraftResource resource) : base(0x1779)
        {
            Hue = CraftResources.GetHue(resource);
            Stackable = true; //was Core.ML but we disabled expansion for $reasons --sith

            m_Resource = resource;
        }

        public BaseGranite(Serial serial) : base(serial)
        {
        }

        public override int LabelNumber { get { return 1044607; } } // high quality granite

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!CraftResources.IsStandard(m_Resource))
            {
                int num = CraftResources.GetLocalizationNumber(m_Resource);

                if (num > 0)
                    list.Add(num);
                else
                    list.Add(CraftResources.GetName(m_Resource));
            }
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            int resourceCliloc = CraftResources.GetLocalizationNumber(m_Resource);

            if (Amount > 1)
            {
                list.Add(1160100, "{0}\t#{1}\t#{2}", Amount, resourceCliloc, 1160108); // 1160100 = ~1_amount~ ~2_resource~ ~3_name~, 1160104 = "granite"
            }
            else
            {
                list.Add(1160101, "#{0}\t#{1}", resourceCliloc, 1160108); // 1160101 = ~1_type~ ~2_name~
            }
        }
    }

    //TOGENERATE
    // no idea wtf this is for
    // it's the Iron Ore of Granites
    public class Granite : BaseGranite
    {
        [Constructable]
        public Granite() : base(CraftResource.Iron)
        {
        }

        public Granite(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    // sub granites moved to ./Generated/
}
