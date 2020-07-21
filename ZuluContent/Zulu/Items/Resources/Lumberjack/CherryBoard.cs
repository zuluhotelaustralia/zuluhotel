// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class CherryBoard : Board
  {
    [Constructible]
    public CherryBoard() : this(1)
    {
    }


    [Constructible]
    public CherryBoard(int amount) : base(CraftResource.Cherry, amount)
    {
      this.Hue = 2206;
    }

    [Constructible]
    public CherryBoard(Serial serial) : base(serial)
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
