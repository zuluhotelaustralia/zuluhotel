namespace Server.Items
{
    [FlipableAttribute(0x13B6, 0x13B5)]
    public class Scimitar : BaseSword
    {
        public override int DefaultMinDamage => 4;
        public override int DefaultMaxDamage => 16;
        public override int DefaultSpeed => 47;
        public override int InitMinHits => 90;
        public override int InitMaxHits => 90;


        [Constructible]
        public Scimitar() : base(0x13B6)
        {
            Weight = 5.0;
        }

        [Constructible]
        public Scimitar(Serial serial) : base(serial)
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