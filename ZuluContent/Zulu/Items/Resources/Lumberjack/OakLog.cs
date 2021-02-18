// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class OakLog : BaseLog
    {
        [Constructible]
        public OakLog() : this(1)
        {
        }


        [Constructible]
        public OakLog(int amount) : base(CraftResource.Oak, amount)
        {
        }

        [Constructible]
        public OakLog(Serial serial) : base(serial)
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