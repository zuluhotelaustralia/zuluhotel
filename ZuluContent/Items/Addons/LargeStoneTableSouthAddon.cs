namespace Server.Items
{
    public class LargeStoneTableSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new LargeStoneTableSouthDeed();


        [Constructible]
        public LargeStoneTableSouthAddon() : this(0)
        {
        }


        [Constructible]
        public LargeStoneTableSouthAddon(int hue)
        {
            AddComponent(new AddonComponent(0x1205), 0, 0, 0);
            AddComponent(new AddonComponent(0x1206), 1, 0, 0);
            AddComponent(new AddonComponent(0x1204), 2, 0, 0);
            Hue = hue;
        }

        [Constructible]
        public LargeStoneTableSouthAddon(Serial serial) : base(serial)
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

    public class LargeStoneTableSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new LargeStoneTableSouthAddon(Hue);

        public override int LabelNumber => 1044512; // large stone table (South)


        public LargeStoneTableSouthDeed()
        {
        }

        public LargeStoneTableSouthDeed(Serial serial) : base(serial)
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