// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SilverRockOre : BaseOre
  {
    [Constructible]
    public SilverRockOre() : this(1)
    {
    }


    [Constructible]
    public SilverRockOre(int amount) : base(CraftResource.SilverRock, amount)
    {
      this.Hue = 0x3e9;
    }

    [Constructible]
    public SilverRockOre(Serial serial) : base(serial)
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

    public override BaseIngot GetIngot()
    {
      return new SilverRockIngot();
    }
  }
}
