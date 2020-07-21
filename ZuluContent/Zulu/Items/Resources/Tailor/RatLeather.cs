namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class RatLeather : BaseLeather
  {
    [Constructible]
    public RatLeather() : this(1)
    {
    }


    [Constructible]
    public RatLeather(int amount) : base(CraftResource.RatLeather, amount)
    {
      this.Hue = 0x7e2;
    }

    [Constructible]
    public RatLeather(Serial serial) : base(serial)
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
