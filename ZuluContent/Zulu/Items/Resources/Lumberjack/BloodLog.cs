// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class BloodLog : BaseLog
    {
        [Constructible]
        public BloodLog() : this(1)
        {
        }


        [Constructible]
        public BloodLog(int amount) : base(CraftResource.Bloodwood, amount)
        {
        }

        [Constructible]
        public BloodLog(Serial serial) : base(serial)
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