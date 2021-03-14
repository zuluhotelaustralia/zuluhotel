namespace Server.Items
{
    public class AltarAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new AltarDeed();


        [Constructible]
        public AltarAddon()
        {
            AddComponent(new AddonComponent(0x120E), -1, -1, 0);
            AddComponent(new AddonComponent(0x120F), 0, -1, 0);
            AddComponent(new AddonComponent(0x1210), 1, -1, 0);
            AddComponent(new AddonComponent(0x1215), -1, 0, 0);
            AddComponent(new AddonComponent(0x1216), 0, 0, 0);
            AddComponent(new AddonComponent(0x1211), 1, 0, 0);
            AddComponent(new AddonComponent(0x1214), -1, 1, 0);
            AddComponent(new AddonComponent(0x1213), 0, 1, 0);
            AddComponent(new AddonComponent(0x1212), 1, 1, 0);
        }

        [Constructible]
        public AltarAddon(Serial serial) : base(serial)
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

    public class AltarDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new AltarAddon();

        public override int LabelNumber => 1044329; // Altar


        public AltarDeed()
        {
        }

        public AltarDeed(Serial serial) : base(serial)
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