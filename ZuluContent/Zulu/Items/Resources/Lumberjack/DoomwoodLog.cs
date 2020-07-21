// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DoomwoodLog : Log
  {
    [Constructible]
    public DoomwoodLog() : this(1)
    {
    }


    [Constructible]
    public DoomwoodLog(int amount) : base(CraftResource.Doomwood, amount)
    {
      this.Hue = 2772;
    }

    [Constructible]
    public DoomwoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 128, new DoomwoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
