// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class DoomLog : BaseLog
    {
        [Constructible]
        public DoomLog() : this(1)
        {
        }


        [Constructible]
        public DoomLog(int amount) : base(CraftResource.Doomwood, amount)
        {
        }

        [Constructible]
        public DoomLog(Serial serial) : base(serial)
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