// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class PurplePassionLog : Log
  {
    [Constructible]
    public PurplePassionLog() : this(1)
    {
    }


    [Constructible]
    public PurplePassionLog(int amount) : base(CraftResource.PurplePassion, amount)
    {
      this.Hue = 515;
    }

    [Constructible]
    public PurplePassionLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 50, new PurplePassionBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
