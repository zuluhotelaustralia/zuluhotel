namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB2E, 0xB2F, 0xB31, 0xB30 )]
    public class WoodenThroneComponent : AddonComponent
    {
        [Constructible]
        public WoodenThroneComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public WoodenThroneComponent(Serial serial) : base(serial)
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
    
    public class WoodenThroneAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenThroneDeed();


        [Constructible]
        public WoodenThroneAddon()
        {
            AddComponent(new WoodenThroneComponent(0xB2E), 0, 0, 0);
        }

        [Constructible]
        public WoodenThroneAddon(Serial serial) : base(serial)
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

    public class WoodenThroneDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenThroneAddon();

        public override int LabelNumber => 1044304; // wooden throne


        public WoodenThroneDeed()
        {
        }

        public WoodenThroneDeed(Serial serial) : base(serial)
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