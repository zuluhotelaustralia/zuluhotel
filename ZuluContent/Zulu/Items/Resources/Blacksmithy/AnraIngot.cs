// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class AnraIngot : BaseIngot
  {
    [Constructible]
    public AnraIngot() : this(1)
    {
    }


    [Constructible]
    public AnraIngot(int amount) : base(CraftResource.Anra, amount)
    {
      this.Hue = 0x48b;
    }

    [Constructible]
    public AnraIngot(Serial serial) : base(serial)
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
