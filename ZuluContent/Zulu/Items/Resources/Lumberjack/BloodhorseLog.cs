// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class BloodhorseLog : BaseLog
    {
        [Constructible]
        public BloodhorseLog() : this(1)
        {
        }


        [Constructible]
        public BloodhorseLog(int amount) : base(CraftResource.Bloodhorse, amount)
        {
        }

        [Constructible]
        public BloodhorseLog(Serial serial) : base(serial)
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