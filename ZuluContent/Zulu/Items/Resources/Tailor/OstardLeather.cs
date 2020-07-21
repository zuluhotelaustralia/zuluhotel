namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class OstardLeather : BaseLeather
  {
    [Constructible]
    public OstardLeather() : this(1)
    {
    }


    [Constructible]
    public OstardLeather(int amount) : base(CraftResource.OstardLeather, amount)
    {
      this.Hue = 0x415;
    }

    [Constructible]
    public OstardLeather(Serial serial) : base(serial)
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
