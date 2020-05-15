// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class SwampwoodLog : BaseLog
    {
        [Constructable]
        public SwampwoodLog() : this(1) { }

        [Constructable]
        public SwampwoodLog(int amount) : base(CraftResource.Swampwood, amount)
        {
            this.Hue = 2767;
        }

        public SwampwoodLog(Serial serial) : base(serial) { }

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
            if (!TryCreateBoards(from, 98, new SwampwoodBoard()))
            {
                return false;
            }
            return true;
        }
    }
}
