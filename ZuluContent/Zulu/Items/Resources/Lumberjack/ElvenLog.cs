// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class ElvenLog : Log
  {
    [Constructible]
    public ElvenLog() : this(1)
    {
    }


    [Constructible]
    public ElvenLog(int amount) : base(CraftResource.Elven, amount)
    {
      this.Hue = 1165;
    }

    [Constructible]
    public ElvenLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 145, new ElvenBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
