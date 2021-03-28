namespace Server.Items
{
    [FlipableAttribute(0xF49, 0xF4a)]
    public class Axe : BaseAxe
    {
        public override int DefaultStrengthReq => 35;
        public override int DefaultMinDamage => 4;
        public override int DefaultMaxDamage => 28;
        public override int DefaultSpeed => 33;
        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;


        [Constructible]
        public Axe() : base(0xF49)
        {
            Weight = 4.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public Axe(Serial serial) : base(serial)
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