namespace Server.Items
{
    [Furniture]
    [Flipable(0xB4A, 0xB49, 0xB4B, 0xB4C)]
    public class WritingTableComponent : AddonComponent
    {
        [Constructible]
        public WritingTableComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public WritingTableComponent(Serial serial) : base(serial)
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

    public class WritingTableAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WritingTableDeed();


        [Constructible]
        public WritingTableAddon()
        {
            AddComponent(new WritingTableComponent(0xB4A), 0, 0, 0);
        }

        [Constructible]
        public WritingTableAddon(Serial serial) : base(serial)
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

    public class WritingTableDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WritingTableAddon();

        public override int LabelNumber => 1022890; // writing table


        public WritingTableDeed()
        {
        }

        public WritingTableDeed(Serial serial) : base(serial)
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