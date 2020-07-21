namespace Server.Items
{
  public class AnvilSouthAddon : BaseAddon
  {
    public override BaseAddonDeed Deed
    {
      get { return new AnvilSouthDeed(); }
    }


    [Constructible]
public AnvilSouthAddon()
    {
      AddComponent(new AnvilComponent(0xFB0), 0, 0, 0);
    }

    [Constructible]
public AnvilSouthAddon(Serial serial) : base(serial)
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

  public class AnvilSouthDeed : BaseAddonDeed
  {
    public override BaseAddon Addon
    {
      get { return new AnvilSouthAddon(); }
    }

    public override int LabelNumber
    {
      get { return 1044334; }
    } // anvil (south)


    public AnvilSouthDeed()
    {
    }

    public AnvilSouthDeed(Serial serial) : base(serial)
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
