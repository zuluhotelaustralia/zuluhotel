// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class PurplePassionBoard : Board
  {
    [Constructible]
    public PurplePassionBoard() : this(1)
    {
    }


    [Constructible]
    public PurplePassionBoard(int amount) : base(CraftResource.PurplePassion, amount)
    {
      this.Hue = 515;
    }

    [Constructible]
    public PurplePassionBoard(Serial serial) : base(serial)
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
