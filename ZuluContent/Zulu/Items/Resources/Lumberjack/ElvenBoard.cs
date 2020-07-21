// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class ElvenBoard : Board
  {
    [Constructible]
    public ElvenBoard() : this(1)
    {
    }


    [Constructible]
    public ElvenBoard(int amount) : base(CraftResource.Elven, amount)
    {
      this.Hue = 1165;
    }

    [Constructible]
    public ElvenBoard(Serial serial) : base(serial)
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
