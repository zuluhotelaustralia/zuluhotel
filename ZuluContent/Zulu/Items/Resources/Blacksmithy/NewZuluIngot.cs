// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  [FlipableAttribute(0x1BF2, 0x1BEF)]
  public class NewZuluIngot : BaseIngot
  {
    [Constructible]
    public NewZuluIngot() : this(1)
    {
    }


    [Constructible]
    public NewZuluIngot(int amount) : base(CraftResource.NewZulu, amount)
    {
      this.Hue = 2749;
    }

    [Constructible]
    public NewZuluIngot(Serial serial) : base(serial)
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
