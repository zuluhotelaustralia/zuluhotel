namespace Server.Items
{
    public class StoneBenchEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new StoneBenchEastDeed();


        [Constructible]
        public StoneBenchEastAddon()
        {
            AddComponent(new AddonComponent(0x1207), 2, 0, 0);
            AddComponent(new AddonComponent(0x1209), 1, 0, 0);
            AddComponent(new AddonComponent(0x1208), 0, 0, 0);
        }

        [Constructible]
        public StoneBenchEastAddon(Serial serial) : base(serial)
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

    public class StoneBenchEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new StoneBenchEastAddon();

        public override int LabelNumber => 1021115; // stone bench


        public StoneBenchEastDeed()
        {
        }

        public StoneBenchEastDeed(Serial serial) : base(serial)
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