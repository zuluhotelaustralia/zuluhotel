namespace Server.Items
{
    [FlipableAttribute(0x1403, 0x1402)]
    public class ShortSpear : BaseSpear
    {
        public override int DefaultStrengthReq => 15;
        public override int DefaultMinDamage => 6;
        public override int DefaultMaxDamage => 21;
        public override int DefaultSpeed => 40;

        public override int DefaultHitSound => 0x23C;
        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;

        [Constructible]
        public ShortSpear() : base(0x1403)
        {
            Weight = 4.0;
        }

        [Constructible]
        public ShortSpear(Serial serial) : base(serial)
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