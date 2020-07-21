// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class EmeraldwoodLog : Log
  {
    [Constructible]
    public EmeraldwoodLog() : this(1)
    {
    }


    [Constructible]
    public EmeraldwoodLog(int amount) : base(CraftResource.Emeraldwood, amount)
    {
      this.Hue = 2748;
    }

    [Constructible]
    public EmeraldwoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 118, new EmeraldwoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
