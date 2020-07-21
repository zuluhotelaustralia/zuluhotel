// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class CrystalIngot : BaseIngot
  {
    [Constructible]
    public CrystalIngot() : this(1)
    {
    }


    [Constructible]
    public CrystalIngot(int amount) : base(CraftResource.Crystal, amount)
    {
      this.Hue = 2759;
    }

    [Constructible]
    public CrystalIngot(Serial serial) : base(serial)
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
  }
}
