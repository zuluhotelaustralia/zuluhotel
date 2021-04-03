namespace Server.Items
{
    public class CrystalBall : Item
    {
        [Constructible]
        public CrystalBall() : base(0xE2D)
        {
            Weight = 1.0;
        }

        [Constructible]
        public CrystalBall(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}