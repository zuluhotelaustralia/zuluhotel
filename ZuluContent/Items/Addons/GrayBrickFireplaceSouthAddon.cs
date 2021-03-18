namespace Server.Items
{
    public class GrayBrickFireplaceSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new GrayBrickFireplaceSouthDeed();


        [Constructible]
        public GrayBrickFireplaceSouthAddon()
        {
            AddComponent(new AddonComponent(0x94B), -1, 0, 0);
            AddComponent(new AddonComponent(0x945), 0, 0, 0);
        }

        [Constructible]
        public GrayBrickFireplaceSouthAddon(Serial serial) : base(serial)
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

    public class GrayBrickFireplaceSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new GrayBrickFireplaceSouthAddon();
        public override int LabelNumber => 1061847; // grey brick fireplace (south)


        public GrayBrickFireplaceSouthDeed()
        {
        }

        public GrayBrickFireplaceSouthDeed(Serial serial) : base(serial)
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