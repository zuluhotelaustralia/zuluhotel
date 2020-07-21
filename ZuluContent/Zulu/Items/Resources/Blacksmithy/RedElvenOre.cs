// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class RedElvenOre : BaseOre
  {
    [Constructible]
    public RedElvenOre() : this(1)
    {
    }


    [Constructible]
    public RedElvenOre(int amount) : base(CraftResource.RedElven, amount)
    {
      this.Hue = 0x4b9;
    }

    [Constructible]
    public RedElvenOre(Serial serial) : base(serial)
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
      return new RedElvenIngot();
    }
  }
}
