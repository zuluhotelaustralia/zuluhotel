namespace Server.Items
{
    [FlipableAttribute(0x1441, 0x1440)]
    public class Cutlass : BaseSword
    {
        public override int DefaultStrengthReq => 10;
        public override int DefaultMinDamage => 4;
        public override int DefaultMaxDamage => 16;
        public override int DefaultSpeed => 45;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Cutlass() : base(0x1441)
        {
            Weight = 8.0;
        }

        [Constructible]
        public Cutlass(Serial serial) : base(serial)
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