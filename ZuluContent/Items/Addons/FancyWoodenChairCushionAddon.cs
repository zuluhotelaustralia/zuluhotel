namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB4F, 0xB4E, 0xB50, 0xB51 )]
    public class FancyWoodenChairCushionComponent : AddonComponent
    {
        [Constructible]
        public FancyWoodenChairCushionComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public FancyWoodenChairCushionComponent(Serial serial) : base(serial)
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
    
    public class FancyWoodenChairCushionAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new FancyWoodenChairCushionDeed();


        [Constructible]
        public FancyWoodenChairCushionAddon()
        {
            AddComponent(new FancyWoodenChairCushionComponent(0xB4F), 0, 0, 0);
        }

        [Constructible]
        public FancyWoodenChairCushionAddon(Serial serial) : base(serial)
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

    public class FancyWoodenChairCushionDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new FancyWoodenChairCushionAddon();

        public override int LabelNumber => 1022862; // wooden chair


        public FancyWoodenChairCushionDeed()
        {
        }

        public FancyWoodenChairCushionDeed(Serial serial) : base(serial)
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