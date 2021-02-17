// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class DripstoneIngot : BaseIngot
    {
        [Constructible]
        public DripstoneIngot() : this(1)
        {
        }


        [Constructible]
        public DripstoneIngot(int amount) : base(CraftResource.Dripstone, amount)
        {
        }

        [Constructible]
        public DripstoneIngot(Serial serial) : base(serial)
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