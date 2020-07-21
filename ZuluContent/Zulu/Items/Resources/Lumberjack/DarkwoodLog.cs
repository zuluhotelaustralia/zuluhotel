// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DarkwoodLog : Log
  {
    [Constructible]
    public DarkwoodLog() : this(1)
    {
    }


    [Constructible]
    public DarkwoodLog(int amount) : base(CraftResource.Darkwood, amount)
    {
      this.Hue = 1109;
    }

    [Constructible]
    public DarkwoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 77, new DarkwoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
