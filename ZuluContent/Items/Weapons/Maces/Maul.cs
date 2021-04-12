namespace Server.Items
{
    [FlipableAttribute(0x143B, 0x143A)]
    public class Maul : BaseBashing
    {
        public override int DefaultStrengthReq => 20;
        public override int DefaultMinDamage => 6;
        public override int DefaultMaxDamage => 18;
        public override int DefaultSpeed => 40;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;

        [Constructible]
        public Maul() : base(0x143B)
        {
            Weight = 10.0;
        }

        [Constructible]
        public Maul(Serial serial) : base(serial)
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

            if (Weight == 14.0)
                Weight = 10.0;
        }
    }
}