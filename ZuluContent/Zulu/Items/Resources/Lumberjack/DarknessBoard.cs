// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DarknessBoard : Board
  {
    [Constructible]
    public DarknessBoard() : this(1)
    {
    }


    [Constructible]
    public DarknessBoard(int amount) : base(CraftResource.Darkness, amount)
    {
      this.Hue = 1175;
    }

    [Constructible]
    public DarknessBoard(Serial serial) : base(serial)
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
