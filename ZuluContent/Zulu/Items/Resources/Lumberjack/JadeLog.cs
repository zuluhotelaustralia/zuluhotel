// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class JadeLog : BaseLog
    {
        [Constructible]
        public JadeLog() : this(1)
        {
        }


        [Constructible]
        public JadeLog(int amount) : base(CraftResource.Jadewood, amount)
        {
        }

        [Constructible]
        public JadeLog(Serial serial) : base(serial)
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