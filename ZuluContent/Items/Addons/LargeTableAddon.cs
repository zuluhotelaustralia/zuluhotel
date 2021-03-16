namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB90, 0xB7D )]
    public class LargeTableComponent : AddonComponent
    {
        [Constructible]
        public LargeTableComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public LargeTableComponent(Serial serial) : base(serial)
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
    
    public class LargeTableAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new LargeTableDeed();


        [Constructible]
        public LargeTableAddon()
        {
            AddComponent(new LargeTableComponent(0xB90), 0, 0, 0);
        }

        [Constructible]
        public LargeTableAddon(Serial serial) : base(serial)
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

    public class LargeTableDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new LargeTableAddon();

        public override int LabelNumber => 1044307; // large table


        public LargeTableDeed()
        {
        }

        public LargeTableDeed(Serial serial) : base(serial)
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