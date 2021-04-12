namespace Server.Items
{
    [FlipableAttribute(0xF5C, 0xF5D)]
    public class Mace : BaseBashing
    {
        public override int DefaultStrengthReq => 20;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 13;
        public override int DefaultSpeed => 50;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Mace() : base(0xF5C)
        {
            Weight = 14.0;
        }

        [Constructible]
        public Mace(Serial serial) : base(serial)
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