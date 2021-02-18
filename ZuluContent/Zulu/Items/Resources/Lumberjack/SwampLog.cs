// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class SwampLog : BaseLog
    {
        [Constructible]
        public SwampLog() : this(1)
        {
        }


        [Constructible]
        public SwampLog(int amount) : base(CraftResource.Swampwood, amount)
        {
        }

        [Constructible]
        public SwampLog(Serial serial) : base(serial)
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