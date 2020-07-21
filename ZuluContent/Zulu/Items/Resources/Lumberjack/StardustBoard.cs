// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class StardustBoard : Board
  {
    [Constructible]
    public StardustBoard() : this(1)
    {
    }


    [Constructible]
    public StardustBoard(int amount) : base(CraftResource.Stardust, amount)
    {
      this.Hue = 2751;
    }

    [Constructible]
    public StardustBoard(Serial serial) : base(serial)
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
