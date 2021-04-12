namespace Server.Items
{
    [FlipableAttribute(0x13F8, 0x13F9)]
    public class GnarledStaff : BaseStaff
    {
        public override int DefaultStrengthReq => 20;
        public override int DefaultMinDamage => 9;
        public override int DefaultMaxDamage => 21;
        public override int DefaultSpeed => 30;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public GnarledStaff() : base(0x13F8)
        {
            Weight = 3.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public GnarledStaff(Serial serial) : base(serial)
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