namespace Server.Items
{
    [FlipableAttribute(0xE87, 0xE88)]
    public class Pitchfork : BaseSpear
    {
        public override int DefaultStrengthReq => 15;

        public override int DefaultMinDamage => 4;

        public override int DefaultMaxDamage => 16;

        public override int DefaultSpeed => 45;

        public override int InitMinHits => 31;

        public override int InitMaxHits => 60;

        public override int DefaultMaxRange => 2;

        [Constructible]
        public Pitchfork() : base(0xE87)
        {
            Weight = 11.0;
        }

        [Constructible]
        public Pitchfork(Serial serial) : base(serial)
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

            if (Weight == 10.0)
                Weight = 11.0;
        }
    }
}