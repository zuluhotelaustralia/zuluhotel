namespace Server.Items
{
    public class SmallForgeAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new SmallForgeDeed();


        [Constructible]
        public SmallForgeAddon()
        {
            AddComponent(new ForgeComponent(0xFB1), 0, 0, 0);
        }

        [Constructible]
        public SmallForgeAddon(Serial serial) : base(serial)
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

    public class SmallForgeDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new SmallForgeAddon();
        public override int LabelNumber => 1044330; // small forge


        public SmallForgeDeed()
        {
        }

        public SmallForgeDeed(Serial serial) : base(serial)
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