// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SunwoodBoard : Board
  {
    [Constructible]
    public SunwoodBoard() : this(1)
    {
    }


    [Constructible]
    public SunwoodBoard(int amount) : base(CraftResource.Sunwood, amount)
    {
      this.Hue = 2766;
    }

    [Constructible]
    public SunwoodBoard(Serial serial) : base(serial)
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
