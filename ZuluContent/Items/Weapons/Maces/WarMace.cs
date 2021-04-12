namespace Server.Items
{
    [FlipableAttribute(0x1407, 0x1406)]
    public class WarMace : BaseBashing
    {
        public override int DefaultStrengthReq => 30;
        public override int DefaultMinDamage => 6;
        public override int DefaultMaxDamage => 26;
        public override int DefaultSpeed => 30;
        public override int InitMinHits => 110;
        public override int InitMaxHits => 110;


        [Constructible]
        public WarMace() : base(0x1407)
        {
            Weight = 17.0;
        }

        [Constructible]
        public WarMace(Serial serial) : base(serial)
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