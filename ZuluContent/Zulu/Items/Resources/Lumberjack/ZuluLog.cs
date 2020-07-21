// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class ZuluLog : Log
  {
    [Constructible]
    public ZuluLog() : this(1)
    {
    }


    [Constructible]
    public ZuluLog(int amount) : base(CraftResource.Zulu, amount)
    {
      this.Hue = 2749;
    }

    [Constructible]
    public ZuluLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 130, new ZuluBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
