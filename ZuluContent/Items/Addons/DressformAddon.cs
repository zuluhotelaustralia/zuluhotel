namespace Server.Items
{
    [Furniture]
    [Flipable(0x0EC6, 0x0EC7)]
    public class DressformComponent : AddonComponent
    {
        [Constructible]
        public DressformComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public DressformComponent(Serial serial) : base(serial)
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

    public class DressformAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new DressformDeed();


        [Constructible]
        public DressformAddon()
        {
            AddComponent(new DressformComponent(0x0EC6), 0, 0, 0);
        }

        [Constructible]
        public DressformAddon(Serial serial) : base(serial)
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

    public class DressformDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new DressformAddon();

        public override int LabelNumber => 1044339; // dressform (front)


        public DressformDeed()
        {
        }

        public DressformDeed(Serial serial) : base(serial)
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