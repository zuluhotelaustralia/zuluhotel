namespace Server.Items
{
    [Furniture]
    [Flipable(0xB53, 0xB52, 0xB54, 0xB55)]
    public class WoodenChairCushionComponent : AddonComponent
    {
        [Constructible]
        public WoodenChairCushionComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public WoodenChairCushionComponent(Serial serial) : base(serial)
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

    public class WoodenChairCushionAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenChairCushionDeed();


        [Constructible]
        public WoodenChairCushionAddon()
        {
            AddComponent(new WoodenChairCushionComponent(0xB53), 0, 0, 0);
        }

        [Constructible]
        public WoodenChairCushionAddon(Serial serial) : base(serial)
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

    public class WoodenChairCushionDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenChairCushionAddon();

        public override int LabelNumber => 1022862; // wooden chair


        public WoodenChairCushionDeed()
        {
        }

        public WoodenChairCushionDeed(Serial serial) : base(serial)
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