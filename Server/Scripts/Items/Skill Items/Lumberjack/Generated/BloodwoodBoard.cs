// Generated File. DO NOT MODIFY BY HAND.
namespace Server.Items
{

    public class BloodwoodBoard : BaseBoard
    {
        [Constructable]
        public BloodwoodBoard() : this(1) { }

        [Constructable]
        public BloodwoodBoard(int amount) : base(CraftResource.Bloodwood, amount)
        {
            this.Hue = 1645;
        }

        public BloodwoodBoard(Serial serial) : base(serial) { }

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
