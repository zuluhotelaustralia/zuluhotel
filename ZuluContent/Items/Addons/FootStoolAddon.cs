namespace Server.Items
{
    [Furniture]
    public class FootStoolAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new FootStoolDeed();


        [Constructible]
        public FootStoolAddon()
        {
            AddComponent(new AddonComponent(0xB5E), 0, 0, 0);
        }

        [Constructible]
        public FootStoolAddon(Serial serial) : base(serial)
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

    public class FootStoolDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new FootStoolAddon();

        public override int LabelNumber => 1022910; // foot stool


        public FootStoolDeed()
        {
        }

        public FootStoolDeed(Serial serial) : base(serial)
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