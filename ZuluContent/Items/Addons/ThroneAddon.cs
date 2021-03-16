namespace Server.Items
{
    [Furniture]
    [Flipable(0xB32, 0xB33)]
    public class ThroneComponent : AddonComponent
    {
        [Constructible]
        public ThroneComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public ThroneComponent(Serial serial) : base(serial)
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

    public class ThroneAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new ThroneDeed();


        [Constructible]
        public ThroneAddon()
        {
            AddComponent(new ThroneComponent(0xB32), 0, 0, 0);
        }

        [Constructible]
        public ThroneAddon(Serial serial) : base(serial)
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

    public class ThroneDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new ThroneAddon();

        public override int LabelNumber => 1022866; // throne


        public ThroneDeed()
        {
        }

        public ThroneDeed(Serial serial) : base(serial)
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