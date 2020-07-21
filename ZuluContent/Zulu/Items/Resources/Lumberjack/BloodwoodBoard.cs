// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class BloodwoodBoard : Board
  {
    [Constructible]
    public BloodwoodBoard() : this(1)
    {
    }


    [Constructible]
    public BloodwoodBoard(int amount) : base(CraftResource.Bloodwood, amount)
    {
      this.Hue = 1645;
    }

    [Constructible]
    public BloodwoodBoard(Serial serial) : base(serial)
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
