namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class TrollLeather : BaseLeather
  {
    [Constructible]
    public TrollLeather() : this(1)
    {
    }


    [Constructible]
    public TrollLeather(int amount) : base(CraftResource.TrollLeather, amount)
    {
      this.Hue = 0x54a;
    }

    [Constructible]
    public TrollLeather(Serial serial) : base(serial)
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
