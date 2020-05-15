// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class GauntletBoard : BaseBoard
    {
        [Constructable]
        public GauntletBoard() : this(1) { }

        [Constructable]
        public GauntletBoard(int amount) : base(CraftResource.Gauntlet, amount)
        {
            this.Hue = 2777;
        }

        public GauntletBoard(Serial serial) : base(serial) { }

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
