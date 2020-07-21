// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class BloodwoodLog : Log
  {
    [Constructible]
    public BloodwoodLog() : this(1)
    {
    }


    [Constructible]
    public BloodwoodLog(int amount) : base(CraftResource.Bloodwood, amount)
    {
      this.Hue = 1645;
    }

    [Constructible]
    public BloodwoodLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 122, new BloodwoodBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
