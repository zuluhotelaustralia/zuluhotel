// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class DarknessBoard : Board
    {
        [Constructable]
        public DarknessBoard() : this(1) { }

        [Constructable]
        public DarknessBoard(int amount) : base(CraftResource.Darkness, amount)
        {
            this.Hue = 1175;
        }

        public DarknessBoard(Serial serial) : base(serial) { }

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
