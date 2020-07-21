// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class JadewoodLog : Log
  {
    [Constructible]
    public JadewoodLog() : this(1)
    {
    }


    [Constructible]
    public JadewoodLog(int amount) : base(CraftResource.Jadewood, amount)
    {
      this.Hue = 1162;
    }

    [Constructible]
    public JadewoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 68, new JadewoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
