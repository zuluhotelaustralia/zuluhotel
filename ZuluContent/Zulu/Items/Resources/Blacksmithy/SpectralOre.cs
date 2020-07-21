// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class SpectralOre : BaseOre
  {
    [Constructible]
    public SpectralOre() : this(1)
    {
    }


    [Constructible]
    public SpectralOre(int amount) : base(CraftResource.Spectral, amount)
    {
      this.Hue = 2744;
    }

    [Constructible]
    public SpectralOre(Serial serial) : base(serial)
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
      return new SpectralIngot();
    }
  }
}
