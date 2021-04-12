namespace Server.Items
{
    [FlipableAttribute(0x13FF, 0x13FE)]
    public class Katana : BaseSword
    {
        public override int DefaultStrengthReq => 20;
        public override int DefaultMinDamage => 1;
        public override int DefaultMaxDamage => 16;
        public override int DefaultSpeed => 58;
        public override int InitMinHits => 90;
        public override int InitMaxHits => 90;


        [Constructible]
        public Katana() : base(0x13FF)
        {
            Weight = 6.0;
        }

        [Constructible]
        public Katana(Serial serial) : base(serial)
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