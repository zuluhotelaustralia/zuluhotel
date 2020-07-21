// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SilverleafBoard : Board
  {
    [Constructible]
    public SilverleafBoard() : this(1)
    {
    }


    [Constructible]
    public SilverleafBoard(int amount) : base(CraftResource.Silverleaf, amount)
    {
      this.Hue = 2301;
    }

    [Constructible]
    public SilverleafBoard(Serial serial) : base(serial)
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
