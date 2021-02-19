// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class SunLog : BaseLog
    {
        [Constructible]
        public SunLog() : this(1)
        {
        }


        [Constructible]
        public SunLog(int amount) : base(CraftResource.Sunwood, amount)
        {
        }

        [Constructible]
        public SunLog(Serial serial) : base(serial)
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