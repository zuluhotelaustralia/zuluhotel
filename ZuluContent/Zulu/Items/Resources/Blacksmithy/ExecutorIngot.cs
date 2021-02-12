// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ExecutorIngot : BaseIngot
    {
        [Constructible]
        public ExecutorIngot() : this(1)
        {
        }


        [Constructible]
        public ExecutorIngot(int amount) : base(CraftResource.Executor, amount)
        {
        }

        [Constructible]
        public ExecutorIngot(Serial serial) : base(serial)
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