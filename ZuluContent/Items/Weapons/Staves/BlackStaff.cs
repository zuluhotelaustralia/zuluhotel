namespace Server.Items
{
    [FlipableAttribute(0xDF1, 0xDF0)]
    public class BlackStaff : BaseStaff
    {
        public override int DefaultStrengthReq => 35;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 21;
        public override int DefaultSpeed => 40;
        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;


        [Constructible]
        public BlackStaff() : base(0xDF0)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public BlackStaff(Serial serial) : base(serial)
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