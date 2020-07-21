//Generated file.  Do not modify by hand.

namespace Server.Items
{
  [FlipableAttribute(0x1079, 0x1078)]
  public class BearHides : BaseHides, IScissorable
  {
    [Constructible]
    public BearHides() : this(1)
    {
    }


    [Constructible]
    public BearHides(int amount) : base(CraftResource.BearLeather, amount)
    {
      this.Hue = 44;
    }

    [Constructible]
    public BearHides(Serial serial) : base(serial)
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

    public bool Scissor(Mobile from, Scissors scissors)
    {
      if (Deleted || !from.CanSee(this)) return false;

      if (!IsChildOf(from.Backpack))
      {
        from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
        return false;
      }

      base.ScissorHelper(from, new BearLeather(), 1);

      return true;
    }
  }
}
