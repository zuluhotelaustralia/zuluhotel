namespace Server.Items
{
    [FlipableAttribute(0xF61, 0xF60)]
    public class Longsword : BaseSword
    {
        public override int DefaultStrengthReq => 25;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 17;
        public override int DefaultSpeed => 45;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Longsword() : base(0xF61)
        {
            Weight = 7.0;
        }

        [Constructible]
        public Longsword(Serial serial) : base(serial)
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