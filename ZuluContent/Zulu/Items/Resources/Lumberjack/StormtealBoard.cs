// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class StormtealBoard : Board
  {
    [Constructible]
    public StormtealBoard() : this(1)
    {
    }


    [Constructible]
    public StormtealBoard(int amount) : base(CraftResource.Stormteal, amount)
    {
      this.Hue = 1346;
    }

    [Constructible]
    public StormtealBoard(Serial serial) : base(serial)
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
