// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class BlackDwarfOre : BaseOre
  {
    [Constructible]
    public BlackDwarfOre() : this(1)
    {
    }


    [Constructible]
    public BlackDwarfOre(int amount) : base(CraftResource.BlackDwarf, amount)
    {
      this.Hue = 0x451;
    }

    [Constructible]
    public BlackDwarfOre(Serial serial) : base(serial)
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
      return new BlackDwarfIngot();
    }
  }
}
