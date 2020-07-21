namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class LicheLeather : BaseLeather
  {
    [Constructible]
    public LicheLeather() : this(1)
    {
    }


    [Constructible]
    public LicheLeather(int amount) : base(CraftResource.LicheLeather, amount)
    {
      this.Hue = 2763;
    }

    [Constructible]
    public LicheLeather(Serial serial) : base(serial)
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
