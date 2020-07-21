// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class CopperOre : BaseOre
  {
    [Constructible]
    public CopperOre() : this(1)
    {
    }


    [Constructible]
    public CopperOre(int amount) : base(CraftResource.Copper, amount)
    {
      this.Hue = 0x602;
    }

    [Constructible]
    public CopperOre(Serial serial) : base(serial)
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
      return new CopperIngot();
    }
  }
}
