// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class GoldenReflectionBoard : Board
  {
    [Constructible]
    public GoldenReflectionBoard() : this(1)
    {
    }


    [Constructible]
    public GoldenReflectionBoard(int amount) : base(CraftResource.GoldenReflection, amount)
    {
      this.Hue = 48;
    }

    [Constructible]
    public GoldenReflectionBoard(Serial serial) : base(serial)
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
