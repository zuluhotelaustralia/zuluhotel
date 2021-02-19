// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class GoldenReflectionsLog : BaseLog
    {
        [Constructible]
        public GoldenReflectionsLog() : this(1)
        {
        }


        [Constructible]
        public GoldenReflectionsLog(int amount) : base(CraftResource.GoldenReflections, amount)
        {
        }

        [Constructible]
        public GoldenReflectionsLog(Serial serial) : base(serial)
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