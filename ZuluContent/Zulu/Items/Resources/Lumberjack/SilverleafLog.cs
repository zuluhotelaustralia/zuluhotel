// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class SilverleafLog : BaseLog
    {
        [Constructible]
        public SilverleafLog() : this(1)
        {
        }


        [Constructible]
        public SilverleafLog(int amount) : base(CraftResource.Silverleaf, amount)
        {
        }

        [Constructible]
        public SilverleafLog(Serial serial) : base(serial)
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