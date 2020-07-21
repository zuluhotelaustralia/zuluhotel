namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class BalronLeather : BaseLeather
  {
    [Constructible]
    public BalronLeather() : this(1)
    {
    }


    [Constructible]
    public BalronLeather(int amount) : base(CraftResource.BalronLeather, amount)
    {
      this.Hue = 1175;
    }

    [Constructible]
    public BalronLeather(Serial serial) : base(serial)
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
