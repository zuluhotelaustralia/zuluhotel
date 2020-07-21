namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class IceCrystalLeather : BaseLeather
  {
    [Constructible]
    public IceCrystalLeather() : this(1)
    {
    }


    [Constructible]
    public IceCrystalLeather(int amount) : base(CraftResource.IceCrystalLeather, amount)
    {
      this.Hue = 2759;
    }

    [Constructible]
    public IceCrystalLeather(Serial serial) : base(serial)
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
