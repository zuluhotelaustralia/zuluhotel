// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class BloodhorseBoard : Board
  {
    [Constructible]
    public BloodhorseBoard() : this(1)
    {
    }


    [Constructible]
    public BloodhorseBoard(int amount) : base(CraftResource.Bloodhorse, amount)
    {
      this.Hue = 2780;
    }

    [Constructible]
    public BloodhorseBoard(Serial serial) : base(serial)
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
