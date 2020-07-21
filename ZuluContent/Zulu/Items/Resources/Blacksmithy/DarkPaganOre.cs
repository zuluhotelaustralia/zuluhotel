// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DarkPaganOre : BaseOre
  {
    [Constructible]
    public DarkPaganOre() : this(1)
    {
    }


    [Constructible]
    public DarkPaganOre(int amount) : base(CraftResource.DarkPagan, amount)
    {
      this.Hue = 0x46b;
    }

    [Constructible]
    public DarkPaganOre(Serial serial) : base(serial)
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
      return new DarkPaganIngot();
    }
  }
}
