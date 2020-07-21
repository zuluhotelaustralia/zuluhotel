// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class EmeraldwoodBoard : Board
  {
    [Constructible]
    public EmeraldwoodBoard() : this(1)
    {
    }


    [Constructible]
    public EmeraldwoodBoard(int amount) : base(CraftResource.Emeraldwood, amount)
    {
      this.Hue = 2748;
    }

    [Constructible]
    public EmeraldwoodBoard(Serial serial) : base(serial)
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
