// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DarknessLog : Log
  {
    [Constructible]
    public DarknessLog() : this(1)
    {
    }


    [Constructible]
    public DarknessLog(int amount) : base(CraftResource.Darkness, amount)
    {
      this.Hue = 1175;
    }

    [Constructible]
    public DarknessLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 140, new DarknessBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
