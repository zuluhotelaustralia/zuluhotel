//Generated file.  Do not modify by hand.

namespace Server.Items
{
  [FlipableAttribute(0x1079, 0x1078)]
  public class TrollHides : BaseHides, IScissorable
  {
    [Constructible]
    public TrollHides() : this(1)
    {
    }


    [Constructible]
    public TrollHides(int amount) : base(CraftResource.TrollLeather, amount)
    {
      this.Hue = 0x54a;
    }

    [Constructible]
    public TrollHides(Serial serial) : base(serial)
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

      base.ScissorHelper(from, new TrollLeather(), 1);

      return true;
    }
  }
}
