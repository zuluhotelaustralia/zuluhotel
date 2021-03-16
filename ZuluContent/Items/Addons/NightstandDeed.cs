namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB34, 0xB35 )]
    public class NightstandComponent : AddonComponent
    {
        [Constructible]
        public NightstandComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public NightstandComponent(Serial serial) : base(serial)
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
    
    public class NightstandAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new NightstandDeed();


        [Constructible]
        public NightstandAddon()
        {
            AddComponent(new NightstandComponent(0xB34), 0, 0, 0);
        }

        [Constructible]
        public NightstandAddon(Serial serial) : base(serial)
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

    public class NightstandDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new NightstandAddon();

        public override int LabelNumber => 1044306; // small table


        public NightstandDeed()
        {
        }

        public NightstandDeed(Serial serial) : base(serial)
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