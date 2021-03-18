namespace Server.Items
{
    public class Painting1Component : AddonComponent
    {
        public override bool NeedsWall => true;
        public override Point3D WallPosition => East ? new Point3D(-1, 0, 0) : new Point3D(0, -1, 0);

        public bool East => ItemID == 0x0EA4;

        [Constructible]
        public Painting1Component() : this(true)
        {
        }


        [Constructible]
        public Painting1Component(bool east) : base(east ? 0x0EA4 : 0x0EA3)
        {
        }

        [Constructible]
        public Painting1Component(Serial serial) : base(serial)
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

    public class Painting1EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Painting1EastDeed();

        public Painting1EastAddon()
        {
            AddComponent(new Painting1Component(true), 0, 0, 0);
        }

        public Painting1EastAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting1EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Painting1EastAddon();

        public override int LabelNumber => 1023744; // painting


        public Painting1EastDeed()
        {
        }

        public Painting1EastDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting1SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Painting1SouthDeed();

        public Painting1SouthAddon()
        {
            AddComponent(new Painting1Component(false), 0, 0, 0);
        }

        public Painting1SouthAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting1SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Painting1SouthAddon();

        public override int LabelNumber => 1023744; // painting


        public Painting1SouthDeed()
        {
        }

        public Painting1SouthDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting2Component : AddonComponent
    {
        public override bool NeedsWall => true;
        public override Point3D WallPosition => East ? new Point3D(-1, 0, 0) : new Point3D(0, -1, 0);

        public bool East => ItemID == 0x0EE7;

        [Constructible]
        public Painting2Component() : this(true)
        {
        }


        [Constructible]
        public Painting2Component(bool east) : base(east ? 0x0EE7 : 0xEC9)
        {
        }

        [Constructible]
        public Painting2Component(Serial serial) : base(serial)
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

    public class Painting2EastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Painting2EastDeed();

        public Painting2EastAddon()
        {
            AddComponent(new Painting2Component(true), 0, 0, 0);
        }

        public Painting2EastAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting2EastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Painting1EastAddon();

        public override int LabelNumber => 1023744; // painting


        public Painting2EastDeed()
        {
        }

        public Painting2EastDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting2SouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed => new Painting2SouthDeed();

        public Painting2SouthAddon()
        {
            AddComponent(new Painting2Component(false), 0, 0, 0);
        }

        public Painting2SouthAddon(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class Painting2SouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new Painting2SouthAddon();

        public override int LabelNumber => 1023744; // painting


        public Painting2SouthDeed()
        {
        }

        public Painting2SouthDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}