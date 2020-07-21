// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class UndeadOre : BaseOre
  {
    [Constructible]
    public UndeadOre() : this(1)
    {
    }


    [Constructible]
    public UndeadOre(int amount) : base(CraftResource.Undead, amount)
    {
      this.Hue = 0x279;
    }

    [Constructible]
    public UndeadOre(Serial serial) : base(serial)
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
      return new UndeadIngot();
    }
  }
}
