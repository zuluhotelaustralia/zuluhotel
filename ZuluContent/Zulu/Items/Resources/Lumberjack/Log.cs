// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class Log : BaseLog
    {
        [Constructible]
        public Log() : this(1)
        {
        }


        [Constructible]
        public Log(int amount) : base(CraftResource.RegularWood, amount)
        {
        }

        [Constructible]
        public Log(Serial serial) : base(serial)
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