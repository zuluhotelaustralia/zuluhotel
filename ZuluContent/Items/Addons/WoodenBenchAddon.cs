namespace Server.Items
{
    [Furniture]
    [Flipable(0xB2D, 0xB2C)]
    public class WoodenBenchComponent : AddonComponent
    {
        [Constructible]
        public WoodenBenchComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public WoodenBenchComponent(Serial serial) : base(serial)
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

    public class WoodenBenchAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBenchDeed();


        [Constructible]
        public WoodenBenchAddon()
        {
            AddComponent(new WoodenBenchComponent(0xB2D), 0, 0, 0);
        }

        [Constructible]
        public WoodenBenchAddon(Serial serial) : base(serial)
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

    public class WoodenBenchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBenchAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBenchDeed()
        {
        }

        public WoodenBenchDeed(Serial serial) : base(serial)
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

    public class WoodenBench2EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBench2EastDeed();


        [Constructible]
        public WoodenBench2EastAddon()
        {
            AddComponent(new AddonComponent(0x0B65), 2, 0, 0);
            AddComponent(new AddonComponent(0x0B67), 1, 0, 0);
            AddComponent(new AddonComponent(0x0B66), 0, 0, 0);
        }

        [Constructible]
        public WoodenBench2EastAddon(Serial serial) : base(serial)
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

    public class WoodenBench2EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBench2EastAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBench2EastDeed()
        {
        }

        public WoodenBench2EastDeed(Serial serial) : base(serial)
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

    public class WoodenBench2SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBench2SouthDeed();


        [Constructible]
        public WoodenBench2SouthAddon()
        {
            AddComponent(new AddonComponent(0x0B5F), 0, 2, 0);
            AddComponent(new AddonComponent(0x0B61), 0, 1, 0);
            AddComponent(new AddonComponent(0x0B60), 0, 0, 0);
        }

        [Constructible]
        public WoodenBench2SouthAddon(Serial serial) : base(serial)
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

    public class WoodenBench2SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBench2SouthAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBench2SouthDeed()
        {
        }

        public WoodenBench2SouthDeed(Serial serial) : base(serial)
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

    public class WoodenBench3EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBench3EastDeed();


        [Constructible]
        public WoodenBench3EastAddon()
        {
            AddComponent(new AddonComponent(0x0B68), 2, 0, 0);
            AddComponent(new AddonComponent(0x0B6A), 1, 0, 0);
            AddComponent(new AddonComponent(0x0B69), 0, 0, 0);
        }

        [Constructible]
        public WoodenBench3EastAddon(Serial serial) : base(serial)
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

    public class WoodenBench3EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBench3EastAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBench3EastDeed()
        {
        }

        public WoodenBench3EastDeed(Serial serial) : base(serial)
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

    public class WoodenBench3SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBench3SouthDeed();


        [Constructible]
        public WoodenBench3SouthAddon()
        {
            AddComponent(new AddonComponent(0x0B62), 0, 2, 0);
            AddComponent(new AddonComponent(0x0B64), 0, 1, 0);
            AddComponent(new AddonComponent(0x0B63), 0, 0, 0);
        }

        [Constructible]
        public WoodenBench3SouthAddon(Serial serial) : base(serial)
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

    public class WoodenBench3SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBench3SouthAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBench3SouthDeed()
        {
        }

        public WoodenBench3SouthDeed(Serial serial) : base(serial)
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

    public class WoodenBench4EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBench4EastDeed();


        [Constructible]
        public WoodenBench4EastAddon()
        {
            AddComponent(new AddonComponent(0x0B92), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B91), 1, 0, 0);
        }

        [Constructible]
        public WoodenBench4EastAddon(Serial serial) : base(serial)
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

    public class WoodenBench4EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBench4EastAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBench4EastDeed()
        {
        }

        public WoodenBench4EastDeed(Serial serial) : base(serial)
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

    public class WoodenBench4SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new WoodenBench4SouthDeed();


        [Constructible]
        public WoodenBench4SouthAddon()
        {
            AddComponent(new AddonComponent(0x0B94), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B93), 0, 1, 0);
        }

        [Constructible]
        public WoodenBench4SouthAddon(Serial serial) : base(serial)
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

    public class WoodenBench4SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new WoodenBench4SouthAddon();

        public override int LabelNumber => 1022860; // wooden bench


        public WoodenBench4SouthDeed()
        {
        }

        public WoodenBench4SouthDeed(Serial serial) : base(serial)
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