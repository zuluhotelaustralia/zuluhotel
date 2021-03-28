namespace Server.Items
{
    [FlipableAttribute(0xE89, 0xE8a)]
    public class QuarterStaff : BaseStaff
    {
        public override int DefaultStrengthReq => 30;
        public override int DefaultMinDamage => 4;
        public override int DefaultMaxDamage => 16;
        public override int DefaultSpeed => 50;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public QuarterStaff() : base(0xE89)
        {
            Weight = 4.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public QuarterStaff(Serial serial) : base(serial)
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