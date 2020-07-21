// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class VirginityIngot : BaseIngot
  {
    [Constructible]
    public VirginityIngot() : this(1)
    {
    }


    [Constructible]
    public VirginityIngot(int amount) : base(CraftResource.Virginity, amount)
    {
      this.Hue = 0x482;
    }

    [Constructible]
    public VirginityIngot(Serial serial) : base(serial)
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
