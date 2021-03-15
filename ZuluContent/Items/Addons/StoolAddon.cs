namespace Server.Items
{
    [Furniture]
    public class StoolAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new StoolDeed();


        [Constructible]
        public StoolAddon()
        {
            AddComponent(new AddonComponent(0xA2A), 0, 0, 0);
        }

        [Constructible]
        public StoolAddon(Serial serial) : base(serial)
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

    public class StoolDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new StoolAddon();

        public override int LabelNumber => 1022602; // stool


        public StoolDeed()
        {
        }

        public StoolDeed(Serial serial) : base(serial)
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