namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class BearLeather : BaseLeather
  {
    [Constructible]
    public BearLeather() : this(1)
    {
    }


    [Constructible]
    public BearLeather(int amount) : base(CraftResource.BearLeather, amount)
    {
      this.Hue = 44;
    }

    [Constructible]
    public BearLeather(Serial serial) : base(serial)
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
