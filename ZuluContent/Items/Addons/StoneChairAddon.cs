namespace Server.Items
{
    [Furniture]
    [Flipable(0x1218, 0x1219, 0x121A, 0x121B)]
    public class StoneChairComponent : AddonComponent
    {
        [Constructible]
        public StoneChairComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public StoneChairComponent(Serial serial) : base(serial)
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

    public class StoneChairAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new StoneChairDeed();


        [Constructible]
        public StoneChairAddon()
        {
            AddComponent(new StoneChairComponent(0x1218), 0, 0, 0);
        }

        [Constructible]
        public StoneChairAddon(Serial serial) : base(serial)
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

    public class StoneChairDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new StoneChairAddon();

        public override int LabelNumber => 1024632; // stone chair


        public StoneChairDeed()
        {
        }

        public StoneChairDeed(Serial serial) : base(serial)
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