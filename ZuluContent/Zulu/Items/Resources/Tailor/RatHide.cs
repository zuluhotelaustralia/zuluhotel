//Generated file.  Do not modify by hand.

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class RatHide : BaseHide
    {
        [Constructible]
        public RatHide() : this(1)
        {
        }


        [Constructible]
        public RatHide(int amount) : base(CraftResource.RatLeather, amount)
        {
            this.Hue = 0x7e2;
        }

        [Constructible]
        public RatHide(Serial serial) : base(serial)
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