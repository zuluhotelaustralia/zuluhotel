// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class SilverRockIngot : BaseIngot
  {
    [Constructible]
    public SilverRockIngot() : this(1)
    {
    }


    [Constructible]
    public SilverRockIngot(int amount) : base(CraftResource.SilverRock, amount)
    {
      this.Hue = 0x3e9;
    }

    [Constructible]
    public SilverRockIngot(Serial serial) : base(serial)
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
