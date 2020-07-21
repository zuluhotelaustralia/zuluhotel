// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class IceRockIngot : BaseIngot
  {
    [Constructible]
    public IceRockIngot() : this(1)
    {
    }


    [Constructible]
    public IceRockIngot(int amount) : base(CraftResource.IceRock, amount)
    {
      this.Hue = 0x480;
    }

    [Constructible]
    public IceRockIngot(Serial serial) : base(serial)
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
