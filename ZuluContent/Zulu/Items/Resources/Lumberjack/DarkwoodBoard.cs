// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DarkwoodBoard : Board
  {
    [Constructible]
    public DarkwoodBoard() : this(1)
    {
    }


    [Constructible]
    public DarkwoodBoard(int amount) : base(CraftResource.Darkwood, amount)
    {
      this.Hue = 1109;
    }

    [Constructible]
    public DarkwoodBoard(Serial serial) : base(serial)
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
