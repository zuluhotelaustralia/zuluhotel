// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class YoungOakLog : BaseLog
    {
        [Constructible]
        public YoungOakLog() : this(1)
        {
        }


        [Constructible]
        public YoungOakLog(int amount) : base(CraftResource.YoungOak, amount)
        {
        }

        [Constructible]
        public YoungOakLog(Serial serial) : base(serial)
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