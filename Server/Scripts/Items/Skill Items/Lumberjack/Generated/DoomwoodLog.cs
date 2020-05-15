// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class DoomwoodLog : BaseLog
    {
        [Constructable]
        public DoomwoodLog() : this(1) { }

        [Constructable]
        public DoomwoodLog(int amount) : base(CraftResource.Doomwood, amount)
        {
            this.Hue = 2772;
        }

        public DoomwoodLog(Serial serial) : base(serial) { }

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
            if (!TryCreateBoards(from, 128, new DoomwoodBoard()))
            {
                return false;
            }
            return true;
        }
    }
}
