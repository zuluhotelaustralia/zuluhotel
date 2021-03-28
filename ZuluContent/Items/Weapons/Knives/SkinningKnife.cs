namespace Server.Items
{
    [FlipableAttribute(0xEC4, 0xEC5)]
    public class SkinningKnife : BaseKnife
    {
        public override int DefaultMinDamage => 1;
        public override int DefaultMaxDamage => 8;
        public override int DefaultSpeed => 50;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public SkinningKnife() : base(0xEC4)
        {
            Weight = 1.0;
        }

        [Constructible]
        public SkinningKnife(Serial serial) : base(serial)
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