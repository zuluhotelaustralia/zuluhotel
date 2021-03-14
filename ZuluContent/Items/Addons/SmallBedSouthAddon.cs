namespace Server.Items
{
    public class SmallBedSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new SmallBedSouthDeed();


        [Constructible]
        public SmallBedSouthAddon()
        {
            AddComponent(new AddonComponent(0xA63), 0, 0, 0);
            AddComponent(new AddonComponent(0xA5C), 0, 1, 0);
        }

        [Constructible]
        public SmallBedSouthAddon(Serial serial) : base(serial)
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

    public class SmallBedSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new SmallBedSouthAddon();
        public override int LabelNumber => 1044321; // small bed (south)


        public SmallBedSouthDeed()
        {
        }

        public SmallBedSouthDeed(Serial serial) : base(serial)
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