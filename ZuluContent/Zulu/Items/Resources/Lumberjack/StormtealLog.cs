// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class StormtealLog : Log
  {
    [Constructible]
    public StormtealLog() : this(1)
    {
    }


    [Constructible]
    public StormtealLog(int amount) : base(CraftResource.Stormteal, amount)
    {
      this.Hue = 1346;
    }

    [Constructible]
    public StormtealLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 114, new StormtealBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
