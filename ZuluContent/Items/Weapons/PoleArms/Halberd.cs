namespace Server.Items
{
    [FlipableAttribute(0x143E, 0x143F)]
    public class Halberd : BasePoleArm
    {
        public override int DefaultStrengthReq => 45;
        public override int DefaultMinDamage => 10;
        public override int DefaultMaxDamage => 35;
        public override int DefaultSpeed => 15;
        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;
        public override int DefaultHitSound => 0x237;

        [Constructible]
        public Halberd() : base(0x143E)
        {
            Weight = 16.0;
            Layer = Layer.TwoHanded;
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