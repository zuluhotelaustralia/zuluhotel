namespace Server.Items
{
    [FlipableAttribute(0x143E, 0x143F)]
    public class Halberd : BasePoleArm
    {
        public override int DefaultStrengthReq => 45;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 49;
        public override int DefaultSpeed => 25;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 80;


        [Constructible]
        public Halberd() : base(0x143E)
        {
            Weight = 16.0;
        }

        [Constructible]
        public Halberd(Serial serial) : base(serial)
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