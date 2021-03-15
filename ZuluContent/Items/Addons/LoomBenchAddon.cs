namespace Server.Items
{
    [Furniture]
    public class LoomBenchAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new LoomBenchDeed();


        [Constructible]
        public LoomBenchAddon()
        {
            AddComponent(new AddonComponent(0x104A), 0, 0, 0);
        }

        [Constructible]
        public LoomBenchAddon(Serial serial) : base(serial)
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

    public class LoomBenchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new LoomBenchAddon();

        public override int LabelNumber => 1024169; // loom bench


        public LoomBenchDeed()
        {
        }

        public LoomBenchDeed(Serial serial) : base(serial)
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