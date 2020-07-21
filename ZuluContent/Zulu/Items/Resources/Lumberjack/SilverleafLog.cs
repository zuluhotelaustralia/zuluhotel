// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SilverleafLog : Log
  {
    [Constructible]
    public SilverleafLog() : this(1)
    {
    }


    [Constructible]
    public SilverleafLog(int amount) : base(CraftResource.Silverleaf, amount)
    {
      this.Hue = 2301;
    }

    [Constructible]
    public SilverleafLog(Serial serial) : base(serial)
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
      if (!TryCreateBoards(from, 110, new SilverleafBoard()))
      {
        return false;
      }

      return true;
    }
  }
}
