namespace Server.Items
{
    [FlipableAttribute(0xf4b, 0xf4c)]
    public class DoubleAxe : BaseAxe
    {
        public override int DefaultStrengthReq => 35;
        public override int DefaultMinDamage => 7;
        public override int DefaultMaxDamage => 22;
        public override int DefaultSpeed => 35;
        public override int InitMinHits => 110;
        public override int InitMaxHits => 110;


        [Constructible]
        public DoubleAxe() : base(0xF4B)
        {
            Weight = 8.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public DoubleAxe(Serial serial) : base(serial)
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