// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class BronzeOre : BaseOre
  {
    [Constructible]
    public BronzeOre() : this(1)
    {
    }


    [Constructible]
    public BronzeOre(int amount) : base(CraftResource.Bronze, amount)
    {
      this.Hue = 0x45e;
    }

    [Constructible]
    public BronzeOre(Serial serial) : base(serial)
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

    public override BaseIngot GetIngot()
    {
      return new BronzeIngot();
    }
  }
}
