namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB5B, 0xB5A, 0xB5C, 0xB5D )]
    public class BambooChairComponent : AddonComponent
    {
        [Constructible]
        public BambooChairComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public BambooChairComponent(Serial serial) : base(serial)
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
    
    public class BambooChairAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new BambooChairDeed();


        [Constructible]
        public BambooChairAddon()
        {
            AddComponent(new BambooChairComponent(0xB5B), 0, 0, 0);
        }

        [Constructible]
        public BambooChairAddon(Serial serial) : base(serial)
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

    public class BambooChairDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new BambooChairAddon();

        public override int LabelNumber => 1044300; // straw chair


        public BambooChairDeed()
        {
        }

        public BambooChairDeed(Serial serial) : base(serial)
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