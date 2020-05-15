// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class ElvenLog : BaseLog
    {
        [Constructable]
        public ElvenLog() : this(1) { }

        [Constructable]
        public ElvenLog(int amount) : base(CraftResource.Elven, amount)
        {
            this.Hue = 1165;
        }

        public ElvenLog(Serial serial) : base(serial) { }

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
            if (!TryCreateBoards(from, 145, new ElvenBoard()))
            {
                return false;
            }
            return true;
        }
    }
}
