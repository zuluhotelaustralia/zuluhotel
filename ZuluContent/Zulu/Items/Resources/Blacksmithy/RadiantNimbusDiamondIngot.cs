// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class RadiantNimbusDiamondIngot : BaseIngot
  {
    [Constructible]
    public RadiantNimbusDiamondIngot() : this(1)
    {
    }


    [Constructible]
    public RadiantNimbusDiamondIngot(int amount) : base(CraftResource.RadiantNimbusDiamond, amount)
    {
      this.Hue = 2765;
    }

    [Constructible]
    public RadiantNimbusDiamondIngot(Serial serial) : base(serial)
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
