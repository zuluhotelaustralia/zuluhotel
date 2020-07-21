// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class PinetreeBoard : Board
  {
    [Constructible]
    public PinetreeBoard() : this(1)
    {
    }


    [Constructible]
    public PinetreeBoard(int amount) : base(CraftResource.Pinetree, amount)
    {
      this.Hue = 1132;
    }

    [Constructible]
    public PinetreeBoard(Serial serial) : base(serial)
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
