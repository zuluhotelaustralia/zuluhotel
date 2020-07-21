namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class LavaLeather : BaseLeather
  {
    [Constructible]
    public LavaLeather() : this(1)
    {
    }


    [Constructible]
    public LavaLeather(int amount) : base(CraftResource.LavaLeather, amount)
    {
      this.Hue = 2747;
    }

    [Constructible]
    public LavaLeather(Serial serial) : base(serial)
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
