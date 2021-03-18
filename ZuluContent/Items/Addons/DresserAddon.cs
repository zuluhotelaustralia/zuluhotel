namespace Server.Items
{
    public class DresserAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new DresserDeed();


        [Constructible]
        public DresserAddon()
        {
            AddComponent(new AddonComponent(0x0A3D), 0, 0, 0);
            AddComponent(new AddonComponent(0x0A3C), 1, 0, 0);
        }

        [Constructible]
        public DresserAddon(Serial serial) : base(serial)
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

    public class DresserDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new DresserAddon();

        public override int LabelNumber => 1022620; // dresser


        public DresserDeed()
        {
        }

        public DresserDeed(Serial serial) : base(serial)
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