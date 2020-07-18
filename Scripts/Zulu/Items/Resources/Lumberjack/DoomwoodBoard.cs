// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class DoomwoodBoard : Board
    {
        [Constructable]
        public DoomwoodBoard() : this(1) { }

        [Constructable]
        public DoomwoodBoard(int amount) : base(CraftResource.Doomwood, amount)
        {
            this.Hue = 2772;
        }

        public DoomwoodBoard(Serial serial) : base(serial) { }

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
    }
}
