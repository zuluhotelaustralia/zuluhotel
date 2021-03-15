namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB57, 0xB56, 0xB59, 0xB58 )]
    public class WoodenChairComponent : AddonComponent
    {
        [Constructible]
        public WoodenChairComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public WoodenChairComponent(Serial serial) : base(serial)
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
    
    public class WoodenChairAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenChairDeed();


        [Constructible]
        public WoodenChairAddon()
        {
            AddComponent(new WoodenChairComponent(0xB57), 0, 0, 0);
        }

        [Constructible]
        public WoodenChairAddon(Serial serial) : base(serial)
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

    public class WoodenChairDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenChairAddon();

        public override int LabelNumber => 1022862; // wooden chair


        public WoodenChairDeed()
        {
        }

        public WoodenChairDeed(Serial serial) : base(serial)
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