namespace Server.Items
{
    public class StoneBenchSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new StoneBenchSouthDeed();


        [Constructible]
        public StoneBenchSouthAddon()
        {
            AddComponent(new AddonComponent(0x120A), 0, 2, 0);
            AddComponent(new AddonComponent(0x120C), 0, 1, 0);
            AddComponent(new AddonComponent(0x120B), 0, 0, 0);
        }

        [Constructible]
        public StoneBenchSouthAddon(Serial serial) : base(serial)
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

    public class StoneBenchSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new StoneBenchSouthAddon();

        public override int LabelNumber => 1021115; // stone bench


        public StoneBenchSouthDeed()
        {
        }

        public StoneBenchSouthDeed(Serial serial) : base(serial)
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