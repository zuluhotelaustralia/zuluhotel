namespace Server.Items
{
  [FlipableAttribute(0x1081, 0x1082)]
  public class NecromancerLeather : BaseLeather
  {
    [Constructible]
    public NecromancerLeather() : this(1)
    {
    }


    [Constructible]
    public NecromancerLeather(int amount) : base(CraftResource.NecromancerLeather, amount)
    {
      this.Hue = 84;
    }

    [Constructible]
    public NecromancerLeather(Serial serial) : base(serial)
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
