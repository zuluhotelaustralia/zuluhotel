// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class CrystalLog : BaseLog
    {
        [Constructible]
        public CrystalLog() : this(1)
        {
        }


        [Constructible]
        public CrystalLog(int amount) : base(CraftResource.Crystalwood, amount)
        {
        }

        [Constructible]
        public CrystalLog(Serial serial) : base(serial)
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