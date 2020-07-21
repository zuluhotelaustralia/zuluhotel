// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class OakBoard : Board
  {
    [Constructible]
    public OakBoard() : this(1)
    {
    }


    [Constructible]
    public OakBoard(int amount) : base(CraftResource.Oak, amount)
    {
      this.Hue = 1045;
    }

    [Constructible]
    public OakBoard(Serial serial) : base(serial)
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
