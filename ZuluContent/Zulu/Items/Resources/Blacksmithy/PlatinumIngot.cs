// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class PlatinumIngot : BaseIngot
  {
    [Constructible]
    public PlatinumIngot() : this(1)
    {
    }


    [Constructible]
    public PlatinumIngot(int amount) : base(CraftResource.Platinum, amount)
    {
      this.Hue = 0x457;
    }

    [Constructible]
    public PlatinumIngot(Serial serial) : base(serial)
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
