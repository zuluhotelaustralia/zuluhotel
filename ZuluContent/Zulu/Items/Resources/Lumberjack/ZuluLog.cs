// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class ZuluLog : BaseLog
    {
        [Constructible]
        public ZuluLog() : this(1)
        {
        }


        [Constructible]
        public ZuluLog(int amount) : base(CraftResource.Zulu, amount)
        {
        }

        [Constructible]
        public ZuluLog(Serial serial) : base(serial)
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