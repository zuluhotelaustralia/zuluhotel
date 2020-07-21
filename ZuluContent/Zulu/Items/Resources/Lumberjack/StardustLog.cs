// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class StardustLog : Log
  {
    [Constructible]
    public StardustLog() : this(1)
    {
    }


    [Constructible]
    public StardustLog(int amount) : base(CraftResource.Stardust, amount)
    {
      this.Hue = 2751;
    }

    [Constructible]
    public StardustLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 105, new StardustBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
