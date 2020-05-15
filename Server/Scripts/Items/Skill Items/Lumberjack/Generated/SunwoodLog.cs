// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class SunwoodLog : BaseLog
    {
        [Constructable]
        public SunwoodLog() : this(1) { }

        [Constructable]
        public SunwoodLog(int amount) : base(CraftResource.Sunwood, amount)
        {
            this.Hue = 2766;
        }

        public SunwoodLog(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override bool Axe(Mobile from, BaseAxe axe)
        {
            if (!TryCreateBoards(from, 91, new SunwoodBoard()))
            {
                return false;
            }
            return true;
        }
    }
}
