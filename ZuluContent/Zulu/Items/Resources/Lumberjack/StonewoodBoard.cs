// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class StonewoodBoard : Board
  {
    [Constructible]
    public StonewoodBoard() : this(1)
    {
    }


    [Constructible]
    public StonewoodBoard(int amount) : base(CraftResource.Stonewood, amount)
    {
      this.Hue = 1154;
    }

    [Constructible]
    public StonewoodBoard(Serial serial) : base(serial)
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
