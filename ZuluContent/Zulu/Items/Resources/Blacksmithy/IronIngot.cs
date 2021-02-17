// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class IronIngot : BaseIngot
    {
        [Constructible]
        public IronIngot() : this(1)
        {
        }


        [Constructible]
        public IronIngot(int amount) : base(CraftResource.Iron, amount)
        {
        }

        [Constructible]
        public IronIngot(Serial serial) : base(serial)
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