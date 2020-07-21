// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class OldBritainIngot : BaseIngot
  {
    [Constructible]
    public OldBritainIngot() : this(1)
    {
    }


    [Constructible]
    public OldBritainIngot(int amount) : base(CraftResource.OldBritain, amount)
    {
      this.Hue = 0x852;
    }

    [Constructible]
    public OldBritainIngot(Serial serial) : base(serial)
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
