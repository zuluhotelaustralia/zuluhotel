namespace Server.Items
{
    [FlipableAttribute(0xF4D, 0xF4E)]
    public class Bardiche : BasePoleArm
    {
        public override int DefaultStrengthReq => 40;
        public override int DefaultMinDamage => 8;
        public override int DefaultMaxDamage => 33;
        public override int DefaultSpeed => 22;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Bardiche() : base(0xF4D)
        {
            Weight = 7.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public Bardiche(Serial serial) : base(serial)
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