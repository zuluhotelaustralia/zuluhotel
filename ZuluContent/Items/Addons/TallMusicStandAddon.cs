namespace Server.Items
{
    [Furniture]
    [Flipable(0x0EB5, 0x0EB6, 0x0EB7, 0x0EB8)]
    public class TallMusicStandComponent : AddonComponent
    {
        [Constructible]
        public TallMusicStandComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public TallMusicStandComponent(Serial serial) : base(serial)
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

    public class TallMusicStandAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new TallMusicStandDeed();


        [Constructible]
        public TallMusicStandAddon()
        {
            AddComponent(new TallMusicStandComponent(0x0EB5), 0, 0, 0);
        }

        [Constructible]
        public TallMusicStandAddon(Serial serial) : base(serial)
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

    public class TallMusicStandDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new TallMusicStandAddon();

        public override int LabelNumber => 1044315; // tall music stand (left)


        public TallMusicStandDeed()
        {
        }

        public TallMusicStandDeed(Serial serial) : base(serial)
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