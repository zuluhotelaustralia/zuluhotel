namespace Server.Items
{
    [FlipableAttribute(0x1439, 0x1438)]
    public class WarHammer : BaseBashing
    {
        public override int DefaultStrengthReq => 40;
        public override int DefaultMinDamage => 6;
        public override int DefaultMaxDamage => 26;
        public override int DefaultSpeed => 34;
        public override int InitMinHits => 110;
        public override int InitMaxHits => 110;

        [Constructible]
        public WarHammer() : base(0x1439)
        {
            Weight = 10.0;
        }

        [Constructible]
        public WarHammer(Serial serial) : base(serial)
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