// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DoomwoodBoard : Board
  {
    [Constructible]
    public DoomwoodBoard() : this(1)
    {
    }


    [Constructible]
    public DoomwoodBoard(int amount) : base(CraftResource.Doomwood, amount)
    {
      this.Hue = 2772;
    }

    [Constructible]
    public DoomwoodBoard(Serial serial) : base(serial)
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
