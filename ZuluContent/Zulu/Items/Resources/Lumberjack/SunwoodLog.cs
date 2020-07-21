// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SunwoodLog : Log
  {
    [Constructible]
    public SunwoodLog() : this(1)
    {
    }


    [Constructible]
    public SunwoodLog(int amount) : base(CraftResource.Sunwood, amount)
    {
      this.Hue = 2766;
    }

    [Constructible]
    public SunwoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 91, new SunwoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
