// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class ElvenLog : BaseLog
    {
        [Constructible]
        public ElvenLog() : this(1)
        {
        }


        [Constructible]
        public ElvenLog(int amount) : base(CraftResource.Elven, amount)
        {
        }

        [Constructible]
        public ElvenLog(Serial serial) : base(serial)
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