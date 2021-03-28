namespace Server.Items
{
    [FlipableAttribute(0xF62, 0xF63)]
    public class Spear : BaseSpear
    {
        public override int DefaultMaxRange => 2;
        public override int DefaultStrengthReq => 30;
        public override int DefaultMinDamage => 2;
        public override int DefaultMaxDamage => 21;
        public override int DefaultSpeed => 35;

        public override int DefaultHitSound => 0x23C;
        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;

        [Constructible]
        public Spear() : base(0xF62)
        {
            Weight = 7.0;
        }

        [Constructible]
        public Spear(Serial serial) : base(serial)
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