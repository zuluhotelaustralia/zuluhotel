namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB8F, 0xB7C )]
    public class YewWoodTableComponent : AddonComponent
    {
        [Constructible]
        public YewWoodTableComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public YewWoodTableComponent(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    
    public class YewWoodTableAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new YewWoodTableDeed();


        [Constructible]
        public YewWoodTableAddon()
        {
            AddComponent(new YewWoodTableComponent(0xB8F), 0, 0, 0);
        }

        [Constructible]
        public YewWoodTableAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class YewWoodTableDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new YewWoodTableAddon();

        public override int LabelNumber => 1044308; // Yew-wood table


        public YewWoodTableDeed()
        {
        }

        public YewWoodTableDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}