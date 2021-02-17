// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class SpikeIngot : BaseIngot
    {
        [Constructible]
        public SpikeIngot() : this(1)
        {
        }


        [Constructible]
        public SpikeIngot(int amount) : base(CraftResource.Spike, amount)
        {
        }

        [Constructible]
        public SpikeIngot(Serial serial) : base(serial)
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