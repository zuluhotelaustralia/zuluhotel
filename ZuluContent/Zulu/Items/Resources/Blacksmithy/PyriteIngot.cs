// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class PyriteIngot : BaseIngot
  {
    [Constructible]
    public PyriteIngot() : this(1)
    {
    }


    [Constructible]
    public PyriteIngot(int amount) : base(CraftResource.Pyrite, amount)
    {
      this.Hue = 0x6b8;
    }

    [Constructible]
    public PyriteIngot(Serial serial) : base(serial)
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
