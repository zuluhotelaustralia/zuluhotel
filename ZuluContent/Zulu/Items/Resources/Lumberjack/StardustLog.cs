// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class StardustLog : BaseLog
    {
        [Constructible]
        public StardustLog() : this(1)
        {
        }


        [Constructible]
        public StardustLog(int amount) : base(CraftResource.Stardust, amount)
        {
        }

        [Constructible]
        public StardustLog(Serial serial) : base(serial)
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