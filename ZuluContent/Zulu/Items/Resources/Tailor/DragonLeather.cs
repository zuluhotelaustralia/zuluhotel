namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class DragonLeather : BaseLeather
  {
    [Constructible]
    public DragonLeather() : this(1)
    {
    }


    [Constructible]
    public DragonLeather(int amount) : base(CraftResource.DragonLeather, amount)
    {
      this.Hue = 2761;
    }

    [Constructible]
    public DragonLeather(Serial serial) : base(serial)
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
