namespace Server.Items
{
    public class MediumStoneTableEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new MediumStoneTableEastDeed();


        [Constructible]
        public MediumStoneTableEastAddon() : this(0)
        {
        }


        [Constructible]
        public MediumStoneTableEastAddon(int hue)
        {
            AddComponent(new AddonComponent(0x1202), 0, 0, 0);
            AddComponent(new AddonComponent(0x1201), 0, 1, 0);
            Hue = hue;
        }

        [Constructible]
        public MediumStoneTableEastAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class MediumStoneTableEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new MediumStoneTableEastAddon(Hue);

        public override int LabelNumber => 1044508; // stone table (east)


        public MediumStoneTableEastDeed()
        {
        }

        public MediumStoneTableEastDeed(Serial serial) : base(serial)
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