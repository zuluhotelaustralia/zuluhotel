// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class NewZuluOre : BaseOre
  {
    [Constructible]
    public NewZuluOre() : this(1)
    {
    }


    [Constructible]
    public NewZuluOre(int amount) : base(CraftResource.NewZulu, amount)
    {
      this.Hue = 2749;
    }

    [Constructible]
    public NewZuluOre(Serial serial) : base(serial)
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
      return new NewZuluIngot();
    }
  }
}
