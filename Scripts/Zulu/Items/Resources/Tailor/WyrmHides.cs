//Generated file.  Do not modify by hand.
namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class WyrmHides : BaseHides, IScissorable
    {
        [Constructable]
        public WyrmHides() : this(1)
        {
        }

        [Constructable]
        public WyrmHides(int amount) : base(CraftResource.WyrmLeather, amount)
        {
            this.Hue = 2747;
        }

        public WyrmHides(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
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
            base.ScissorHelper(from, new WyrmLeather(), 1);

            return true;
        }
    }
}
