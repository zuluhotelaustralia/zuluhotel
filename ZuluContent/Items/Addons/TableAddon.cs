namespace Server.Items
{
    public class TableEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new TableEastDeed();


        [Constructible]
        public TableEastAddon()
        {
            AddComponent(new AddonComponent(0x0B7E), 2, 0, 0);
            AddComponent(new AddonComponent(0x0B80), 1, 0, 0);
            AddComponent(new AddonComponent(0x0B7F), 0, 0, 0);
        }

        [Constructible]
        public TableEastAddon(Serial serial) : base(serial)
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

    public class TableEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new TableEastAddon();

        public override int LabelNumber => 1022876; // table


        public TableEastDeed()
        {
        }

        public TableEastDeed(Serial serial) : base(serial)
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
    
    public class TableSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new TableSouthDeed();


        [Constructible]
        public TableSouthAddon()
        {
            AddComponent(new AddonComponent(0xB6B), 0, 2, 0);
            AddComponent(new AddonComponent(0xB6D), 0, 1, 0);
            AddComponent(new AddonComponent(0xB6C), 0, 0, 0);
        }

        [Constructible]
        public TableSouthAddon(Serial serial) : base(serial)
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

    public class TableSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new TableSouthAddon();

        public override int LabelNumber => 1022876; // table


        public TableSouthDeed()
        {
        }

        public TableSouthDeed(Serial serial) : base(serial)
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
    
    public class Table2EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table2EastDeed();


        [Constructible]
        public Table2EastAddon()
        {
            AddComponent(new AddonComponent(0x0B82), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B81), 1, 0, 0);
        }

        [Constructible]
        public Table2EastAddon(Serial serial) : base(serial)
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

    public class Table2EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table2EastAddon();

        public override int LabelNumber => 1022876; // table


        public Table2EastDeed()
        {
        }

        public Table2EastDeed(Serial serial) : base(serial)
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
    
    public class Table2SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table2SouthDeed();


        [Constructible]
        public Table2SouthAddon()
        {
            AddComponent(new AddonComponent(0xB6F), 0, 0, 0);
            AddComponent(new AddonComponent(0xB6E), 0, 1, 0);
        }

        [Constructible]
        public Table2SouthAddon(Serial serial) : base(serial)
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

    public class Table2SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table2SouthAddon();

        public override int LabelNumber => 1022876; // table


        public Table2SouthDeed()
        {
        }

        public Table2SouthDeed(Serial serial) : base(serial)
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
    
    public class Table3EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table3EastDeed();


        [Constructible]
        public Table3EastAddon()
        {
            AddComponent(new AddonComponent(0x0B70), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B71), 2, 0, 0);
            AddComponent(new AddonComponent(0x0B72), 2, -1, 0);
            AddComponent(new AddonComponent(0x0B73), 1, -1, 0);
            AddComponent(new AddonComponent(0x0B73), 0, -1, 0);
            AddComponent(new AddonComponent(0x0B74), 1, 0, 0);
        }

        [Constructible]
        public Table3EastAddon(Serial serial) : base(serial)
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

    public class Table3EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table3EastAddon();

        public override int LabelNumber => 1022876; // table


        public Table3EastDeed()
        {
        }

        public Table3EastDeed(Serial serial) : base(serial)
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
    
    public class Table3SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table3SouthDeed();


        [Constructible]
        public Table3SouthAddon()
        {
            AddComponent(new AddonComponent(0x0B83), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B84), 0, 2, 0);
            AddComponent(new AddonComponent(0x0B85), -1, 2, 0);
            AddComponent(new AddonComponent(0x0B86), -1, 1, 0);
            AddComponent(new AddonComponent(0x0B86), -1, 0, 0);
            AddComponent(new AddonComponent(0x0B87), 0, 1, 0);
        }

        [Constructible]
        public Table3SouthAddon(Serial serial) : base(serial)
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

    public class Table3SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table3SouthAddon();

        public override int LabelNumber => 1022876; // table


        public Table3SouthDeed()
        {
        }

        public Table3SouthDeed(Serial serial) : base(serial)
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
    
    public class Table4EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table4EastDeed();


        [Constructible]
        public Table4EastAddon()
        {
            AddComponent(new AddonComponent(0x0B89), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B88), 1, 0, 0);
        }

        [Constructible]
        public Table4EastAddon(Serial serial) : base(serial)
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

    public class Table4EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table4EastAddon();

        public override int LabelNumber => 1022876; // table


        public Table4EastDeed()
        {
        }

        public Table4EastDeed(Serial serial) : base(serial)
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
    
    public class Table4SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table4SouthDeed();


        [Constructible]
        public Table4SouthAddon()
        {
            AddComponent(new AddonComponent(0x0B76), 0, 0, 0);
            AddComponent(new AddonComponent(0x0B75), 0, 1, 0);
        }

        [Constructible]
        public Table4SouthAddon(Serial serial) : base(serial)
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

    public class Table4SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table4SouthAddon();

        public override int LabelNumber => 1022876; // table


        public Table4SouthDeed()
        {
        }

        public Table4SouthDeed(Serial serial) : base(serial)
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
    
    public class Table5EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table5EastDeed();


        [Constructible]
        public Table5EastAddon()
        {
            AddComponent(new AddonComponent(0x11DC), 2, 0, 0);
            AddComponent(new AddonComponent(0x11DE), 1, 0, 0);
            AddComponent(new AddonComponent(0x11DD), 0, 0, 0);
        }

        [Constructible]
        public Table5EastAddon(Serial serial) : base(serial)
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

    public class Table5EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table5EastAddon();

        public override int LabelNumber => 1022876; // table


        public Table5EastDeed()
        {
        }

        public Table5EastDeed(Serial serial) : base(serial)
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
    
    public class Table5SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Table5SouthDeed();


        [Constructible]
        public Table5SouthAddon()
        {
            AddComponent(new AddonComponent(0x11DF), 0, 2, 0);
            AddComponent(new AddonComponent(0x11E1), 0, 1, 0);
            AddComponent(new AddonComponent(0x11E0), 0, 0, 0);
        }

        [Constructible]
        public Table5SouthAddon(Serial serial) : base(serial)
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

    public class Table5SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Table5SouthAddon();

        public override int LabelNumber => 1022876; // table


        public Table5SouthDeed()
        {
        }

        public Table5SouthDeed(Serial serial) : base(serial)
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