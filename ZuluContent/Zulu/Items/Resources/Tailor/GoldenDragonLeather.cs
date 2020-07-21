namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class GoldenDragonLeather : BaseLeather
  {
    [Constructible]
    public GoldenDragonLeather() : this(1)
    {
    }


    [Constructible]
    public GoldenDragonLeather(int amount) : base(CraftResource.GoldenDragonLeather, amount)
    {
      this.Hue = 48;
    }

    [Constructible]
    public GoldenDragonLeather(Serial serial) : base(serial)
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
