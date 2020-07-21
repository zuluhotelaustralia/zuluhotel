// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class GoldenReflectionLog : Log
  {
    [Constructible]
    public GoldenReflectionLog() : this(1)
    {
    }


    [Constructible]
    public GoldenReflectionLog(int amount) : base(CraftResource.GoldenReflection, amount)
    {
      this.Hue = 48;
    }

    [Constructible]
    public GoldenReflectionLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 59, new GoldenReflectionBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
