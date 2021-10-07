namespace Server.Items
{
    [FlipableAttribute(0x13B9, 0x13BA)]
    public class VikingSword : BaseSword
    {
        public override int DefaultStrengthReq => 50;
        public override int DefaultMinDamage => 1;
        public override int DefaultMaxDamage => 23;
        public override int DefaultSpeed => 25;
        public override int InitMinHits => 95;
        public override int InitMaxHits => 95;
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash2H;

        [Constructible]
        public VikingSword() : base(0x13B9)
        {
            Weight = 6.0;
        }

        [Constructible]
        public VikingSword(Serial serial) : base(serial)
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