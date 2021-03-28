namespace Server.Items
{
    [FlipableAttribute(0xF5E, 0xF5F)]
    public class Broadsword : BaseSword
    {
        public override int DefaultStrengthReq => 25;
        public override int DefaultMinDamage => 2;
        public override int DefaultMaxDamage => 19;
        public override int DefaultSpeed => 45;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Broadsword() : base(0xF5E)
        {
            Weight = 6.0;
        }

        [Constructible]
        public Broadsword(Serial serial) : base(serial)
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