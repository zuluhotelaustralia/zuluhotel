// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class HardrangerLog : BaseLog
    {
        [Constructible]
        public HardrangerLog() : this(1)
        {
        }


        [Constructible]
        public HardrangerLog(int amount) : base(CraftResource.Hardranger, amount)
        {
        }

        [Constructible]
        public HardrangerLog(Serial serial) : base(serial)
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