namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class LizardLeather : BaseLeather
  {
    [Constructible]
    public LizardLeather() : this(1)
    {
    }


    [Constructible]
    public LizardLeather(int amount) : base(CraftResource.LizardLeather, amount)
    {
      this.Hue = 0x852;
    }

    [Constructible]
    public LizardLeather(Serial serial) : base(serial)
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
