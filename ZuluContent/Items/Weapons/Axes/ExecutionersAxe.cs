namespace Server.Items
{
    [FlipableAttribute(0xf45, 0xf46)]
    public class ExecutionersAxe : BaseAxe
    {
        public override int DefaultStrengthReq => 35;
        public override int DefaultMinDamage => 3;
        public override int DefaultMaxDamage => 15;
        public override int DefaultSpeed => 33;
        public override int InitMinHits => 90;
        public override int InitMaxHits => 90;


        [Constructible]
        public ExecutionersAxe() : base(0xF45)
        {
            Weight = 8.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public ExecutionersAxe(Serial serial) : base(serial)
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