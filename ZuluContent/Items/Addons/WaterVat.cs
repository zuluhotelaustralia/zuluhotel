namespace Server.Items
{
    public class WaterVatAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WaterVatDeed();

        public WaterVatAddon()
        {
            AddComponent(new AddonComponent(0x1558), 0, 0, 0);
            AddComponent(new AddonComponent(0x14DE), -1, 1, 0);
            AddComponent(new AddonComponent(0x1552), 0, 1, 0);
            AddComponent(new AddonComponent(0x14DF), 1, -1, 0);
            AddComponent(new AddonComponent(0x1554), 1, 0, 0);
            AddComponent(new AddonComponent(0x1559), 1, 1, 0);
            AddComponent(new AddonComponent(0x1550), 1, 3, 0);
            AddComponent(new AddonComponent(0x1555), 3, 1, 0);
            AddComponent(new AddonComponent(0x14D7), 2, 2, 0);

            // Blockers
            AddComponent(new AddonComponent(0x21A4), 2, -1, 0);
            AddComponent(new AddonComponent(0x21A4), 3, 0, 0);
            AddComponent(new AddonComponent(0x21A4), -1, 2, 0);
            AddComponent(new AddonComponent(0x21A4), 0, 3, 0);
        }

        public WaterVatAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class WaterVatDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WaterVatAddon();
        public override int LabelNumber => 1025460; // vat


        public WaterVatDeed()
        {
        }

        public WaterVatDeed(Serial serial) : base(serial)
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