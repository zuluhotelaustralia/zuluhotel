// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SwampwoodLog : Log
  {
    [Constructible]
    public SwampwoodLog() : this(1)
    {
    }


    [Constructible]
    public SwampwoodLog(int amount) : base(CraftResource.Swampwood, amount)
    {
      this.Hue = 2767;
    }

    [Constructible]
    public SwampwoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 98, new SwampwoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
