// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class DarkwoodLog : BaseLog
    {
        [Constructible]
        public DarkwoodLog() : this(1)
        {
        }


        [Constructible]
        public DarkwoodLog(int amount) : base(CraftResource.Darkwood, amount)
        {
        }

        [Constructible]
        public DarkwoodLog(Serial serial) : base(serial)
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