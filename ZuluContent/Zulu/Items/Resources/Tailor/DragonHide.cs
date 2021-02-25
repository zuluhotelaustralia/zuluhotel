//Generated file.  Do not modify by hand.

namespace Server.Items
{
    [FlipableAttribute(0x1079, 0x1078)]
    public class DragonHide : BaseHide
    {
        [Constructible]
        public DragonHide() : this(1)
        {
        }


        [Constructible]
        public DragonHide(int amount) : base(CraftResource.DragonLeather, amount)
        {
        }

        [Constructible]
        public DragonHide(Serial serial) : base(serial)
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