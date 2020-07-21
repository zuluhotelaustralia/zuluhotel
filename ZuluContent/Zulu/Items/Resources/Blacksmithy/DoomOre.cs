// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DoomOre : BaseOre
  {
    [Constructible]
    public DoomOre() : this(1)
    {
    }


    [Constructible]
    public DoomOre(int amount) : base(CraftResource.Doom, amount)
    {
      this.Hue = 2772;
    }

    [Constructible]
    public DoomOre(Serial serial) : base(serial)
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
      return new DoomIngot();
    }
  }
}
