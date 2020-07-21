// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class CherryLog : Log
  {
    [Constructible]
    public CherryLog() : this(1)
    {
    }


    [Constructible]
    public CherryLog(int amount) : base(CraftResource.Cherry, amount)
    {
      this.Hue = 2206;
    }

    [Constructible]
    public CherryLog(Serial serial) : base(serial)
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

    public override bool Axe(Mobile from, BaseAxe axe)
    {
      if (!TryCreateBoards(from, 28, new CherryBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
