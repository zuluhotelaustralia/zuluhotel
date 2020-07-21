// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DripstoneOre : BaseOre
  {
    [Constructible]
    public DripstoneOre() : this(1)
    {
    }


    [Constructible]
    public DripstoneOre(int amount) : base(CraftResource.Dripstone, amount)
    {
      this.Hue = 2771;
    }

    [Constructible]
    public DripstoneOre(Serial serial) : base(serial)
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
      return new DripstoneIngot();
    }
  }
}
