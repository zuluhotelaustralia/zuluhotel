// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class SpikeOre : BaseOre
    {
        [Constructible]
        public SpikeOre() : this(1)
        {
        }


        [Constructible]
        public SpikeOre(int amount) : base(CraftResource.Spike, amount)
        {
        }

        [Constructible]
        public SpikeOre(Serial serial) : base(serial)
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
            return new SpikeIngot();
        }
    }
}