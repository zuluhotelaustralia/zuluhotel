namespace Server.Items
{
    public class StatueEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new StatueEastDeed();


        [Constructible]
        public StatueEastAddon()
        {
            AddComponent(new AddonComponent(0x129F), 1, 1, 0);
            AddComponent(new AddonComponent(0x12A0), 1, 0, 0);
            AddComponent(new AddonComponent(0x12A1), 0, 1, 0);
        }

        [Constructible]
        public StatueEastAddon(Serial serial) : base(serial)
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

    public class StatueEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new StatueEastAddon();

        public override int LabelNumber => 1030912; // statue


        public StatueEastDeed()
        {
        }

        public StatueEastDeed(Serial serial) : base(serial)
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

    public class StatueSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new StatueSouthDeed();


        [Constructible]
        public StatueSouthAddon()
        {
            AddComponent(new AddonComponent(0x139E), 1, 1, 0);
            AddComponent(new AddonComponent(0x139F), 0, 1, 0);
            AddComponent(new AddonComponent(0x13A0), 1, 0, 0);
        }

        [Constructible]
        public StatueSouthAddon(Serial serial) : base(serial)
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

    public class StatueSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new StatueSouthAddon();

        public override int LabelNumber => 1030912; // statue


        public StatueSouthDeed()
        {
        }

        public StatueSouthDeed(Serial serial) : base(serial)
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

    public class Statue2EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Statue2EastDeed();


        [Constructible]
        public Statue2EastAddon()
        {
            AddComponent(new AddonComponent(0x12A2), 1, 1, 0);
            AddComponent(new AddonComponent(0x12A3), 1, 0, 0);
            AddComponent(new AddonComponent(0x12A4), 0, 1, 0);
        }

        [Constructible]
        public Statue2EastAddon(Serial serial) : base(serial)
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

    public class Statue2EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Statue2EastAddon();

        public override int LabelNumber => 1030912; // statue

        public Statue2EastDeed()
        {
        }

        public Statue2EastDeed(Serial serial) : base(serial)
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

    public class Statue2SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Statue2SouthDeed();


        [Constructible]
        public Statue2SouthAddon()
        {
            AddComponent(new AddonComponent(0x13A1), 1, 1, 0);
            AddComponent(new AddonComponent(0x13A2), 0, 1, 0);
            AddComponent(new AddonComponent(0x13A3), 1, 0, 0);
        }

        [Constructible]
        public Statue2SouthAddon(Serial serial) : base(serial)
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

    public class Statue2SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Statue2SouthAddon();

        public override int LabelNumber => 1030912; // statue


        public Statue2SouthDeed()
        {
        }

        public Statue2SouthDeed(Serial serial) : base(serial)
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

    public class Statue3EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Statue3EastDeed();


        [Constructible]
        public Statue3EastAddon()
        {
            AddComponent(new AddonComponent(0x12D9), 0, 0, 0);
        }

        [Constructible]
        public Statue3EastAddon(Serial serial) : base(serial)
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

    public class Statue3EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Statue3EastAddon();

        public override int LabelNumber => 1030912; // statue


        public Statue3EastDeed()
        {
        }

        public Statue3EastDeed(Serial serial) : base(serial)
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

    public class Statue3SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Statue3SouthDeed();


        [Constructible]
        public Statue3SouthAddon()
        {
            AddComponent(new AddonComponent(0x12D8), 0, 0, 0);
        }

        [Constructible]
        public Statue3SouthAddon(Serial serial) : base(serial)
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

    public class Statue3SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Statue3SouthAddon();

        public override int LabelNumber => 1030912; // statue


        public Statue3SouthDeed()
        {
        }

        public Statue3SouthDeed(Serial serial) : base(serial)
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