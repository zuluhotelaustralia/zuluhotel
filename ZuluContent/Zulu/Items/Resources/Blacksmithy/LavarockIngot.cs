// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class LavarockIngot : BaseIngot
  {
    [Constructible]
    public LavarockIngot() : this(1)
    {
    }


    [Constructible]
    public LavarockIngot(int amount) : base(CraftResource.Lavarock, amount)
    {
      this.Hue = 2747;
    }

    [Constructible]
    public LavarockIngot(Serial serial) : base(serial)
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
