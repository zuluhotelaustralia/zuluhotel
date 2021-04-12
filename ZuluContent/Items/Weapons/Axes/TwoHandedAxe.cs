namespace Server.Items
{
    [FlipableAttribute(0x1443, 0x1442)]
    public class TwoHandedAxe : BaseAxe
    {
        public override int DefaultStrengthReq => 40;
        public override int DefaultMinDamage => 8;
        public override int DefaultMaxDamage => 28;
        public override int DefaultSpeed => 30;
        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;


        [Constructible]
        public TwoHandedAxe() : base(0x1443)
        {
            Weight = 8.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public TwoHandedAxe(Serial serial) : base(serial)
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