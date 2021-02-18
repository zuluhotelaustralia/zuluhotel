// Generated File. DO NOT MODIFY BY HAND.

namespace Server.Items
{
    public class GauntletLog : BaseLog
    {
        [Constructible]
        public GauntletLog() : this(1)
        {
        }


        [Constructible]
        public GauntletLog(int amount) : base(CraftResource.Gauntlet, amount)
        {
        }

        [Constructible]
        public GauntletLog(Serial serial) : base(serial)
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