// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class FruityOre : BaseOre
  {
    [Constructible]
    public FruityOre() : this(1)
    {
    }


    [Constructible]
    public FruityOre(int amount) : base(CraftResource.Fruity, amount)
    {
      this.Hue = 0x46e;
    }

    [Constructible]
    public FruityOre(Serial serial) : base(serial)
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
      return new FruityIngot();
    }
  }
}
