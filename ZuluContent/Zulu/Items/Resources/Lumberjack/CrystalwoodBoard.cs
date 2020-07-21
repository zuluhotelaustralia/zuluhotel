// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class CrystalwoodBoard : Board
  {
    [Constructible]
    public CrystalwoodBoard() : this(1)
    {
    }


    [Constructible]
    public CrystalwoodBoard(int amount) : base(CraftResource.Crystalwood, amount)
    {
      this.Hue = 2759;
    }

    [Constructible]
    public CrystalwoodBoard(Serial serial) : base(serial)
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
