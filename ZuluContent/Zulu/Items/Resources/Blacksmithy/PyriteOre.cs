// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class PyriteOre : BaseOre
  {
    [Constructible]
    public PyriteOre() : this(1)
    {
    }


    [Constructible]
    public PyriteOre(int amount) : base(CraftResource.Pyrite, amount)
    {
      this.Hue = 0x6b8;
    }

    [Constructible]
    public PyriteOre(Serial serial) : base(serial)
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
      return new PyriteIngot();
    }
  }
}
