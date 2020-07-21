// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class GauntletBoard : Board
  {
    [Constructible]
    public GauntletBoard() : this(1)
    {
    }


    [Constructible]
    public GauntletBoard(int amount) : base(CraftResource.Gauntlet, amount)
    {
      this.Hue = 2777;
    }

    [Constructible]
    public GauntletBoard(Serial serial) : base(serial)
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
