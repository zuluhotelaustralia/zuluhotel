// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SwampwoodBoard : Board
  {
    [Constructible]
    public SwampwoodBoard() : this(1)
    {
    }


    [Constructible]
    public SwampwoodBoard(int amount) : base(CraftResource.Swampwood, amount)
    {
      this.Hue = 2767;
    }

    [Constructible]
    public SwampwoodBoard(Serial serial) : base(serial)
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
