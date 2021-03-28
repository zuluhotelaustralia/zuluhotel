namespace Server.Items
{
    [FlipableAttribute(0x1405, 0x1404)]
    public class WarFork : BaseSpear
    {
        public override int DefaultStrengthReq => 35;
        public override int DefaultMinDamage => 4;
        public override int DefaultMaxDamage => 24;
        public override int DefaultSpeed => 35;

        public override int DefaultHitSound => 0x23C;
        public override int DefaultMissSound => 0x23A;

        public override int InitMinHits => 110;
        public override int InitMaxHits => 110;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;


        [Constructible]
        public WarFork() : base(0x1405)
        {
            Weight = 9.0;
        }

        [Constructible]
        public WarFork(Serial serial) : base(serial)
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