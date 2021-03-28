namespace Server.Items
{
    [FlipableAttribute(0xF52, 0xF51)]
    public class Dagger : BaseKnife
    {
        public override int DefaultMinDamage => 3;
        public override int DefaultMaxDamage => 9;
        public override int DefaultSpeed => 50;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;

        [Constructible]
        public Dagger() : base(0xF52)
        {
            Weight = 1.0;
        }

        [Constructible]
        public Dagger(Serial serial) : base(serial)
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