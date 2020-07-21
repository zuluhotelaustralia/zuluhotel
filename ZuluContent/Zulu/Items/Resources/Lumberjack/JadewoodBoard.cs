// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class JadewoodBoard : Board
  {
    [Constructible]
    public JadewoodBoard() : this(1)
    {
    }


    [Constructible]
    public JadewoodBoard(int amount) : base(CraftResource.Jadewood, amount)
    {
      this.Hue = 1162;
    }

    [Constructible]
    public JadewoodBoard(Serial serial) : base(serial)
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
