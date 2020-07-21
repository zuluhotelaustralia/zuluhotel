// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class OnyxIngot : BaseIngot
  {
    [Constructible]
    public OnyxIngot() : this(1)
    {
    }


    [Constructible]
    public OnyxIngot(int amount) : base(CraftResource.Onyx, amount)
    {
      this.Hue = 0x455;
    }

    [Constructible]
    public OnyxIngot(Serial serial) : base(serial)
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
