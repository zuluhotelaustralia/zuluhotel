namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class SerpentLeather : BaseLeather
  {
    [Constructible]
    public SerpentLeather() : this(1)
    {
    }


    [Constructible]
    public SerpentLeather(int amount) : base(CraftResource.SerpentLeather, amount)
    {
      this.Hue = 0x8fd;
    }

    [Constructible]
    public SerpentLeather(Serial serial) : base(serial)
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
