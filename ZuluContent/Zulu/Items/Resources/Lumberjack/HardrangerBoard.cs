// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class HardrangerBoard : Board
  {
    [Constructible]
    public HardrangerBoard() : this(1)
    {
    }


    [Constructible]
    public HardrangerBoard(int amount) : base(CraftResource.Hardranger, amount)
    {
      this.Hue = 2778;
    }

    [Constructible]
    public HardrangerBoard(Serial serial) : base(serial)
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
