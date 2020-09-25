namespace Server.Items
{
    [FlipableAttribute(0xF4D, 0xF4E)]
    public class Bardiche : BasePoleArm
    {
        public override int DefaultStrengthReq => 40;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 43;
        public override int DefaultSpeed => 26;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 100;


        [Constructible]
        public Bardiche() : base(0xF4D)
        {
            Weight = 7.0;
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