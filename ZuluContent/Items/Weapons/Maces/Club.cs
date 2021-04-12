namespace Server.Items
{
    [FlipableAttribute(0x13b4, 0x13b3)]
    public class Club : BaseBashing
    {
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 25;
        public override int DefaultSpeed => 30;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Club() : base(0x13B4)
        {
            Weight = 9.0;
        }

        [Constructible]
        public Club(Serial serial) : base(serial)
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