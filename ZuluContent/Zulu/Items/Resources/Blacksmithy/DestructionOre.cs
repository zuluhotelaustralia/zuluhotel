// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
  public class DestructionOre : BaseOre
  {
    [Constructible]
    public DestructionOre() : this(1)
    {
    }


    [Constructible]
    public DestructionOre(int amount) : base(CraftResource.Destruction, amount)
    {
      this.Hue = 2773;
    }

    [Constructible]
    public DestructionOre(Serial serial) : base(serial)
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
      return new DestructionIngot();
    }
  }
}
