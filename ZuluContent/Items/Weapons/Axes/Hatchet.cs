namespace Server.Items
{
    [FlipableAttribute(0xF43, 0xF44)]
    public class Hatchet : BaseAxe
    {
        public override int DefaultMinDamage => 2;
        public override int DefaultMaxDamage => 10;
        public override int DefaultSpeed => 40;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash1H;


        [Constructible]
        public Hatchet() : base(0xF43)
        {
            Weight = 4.0;
        }

        [Constructible]
        public Hatchet(Serial serial) : base(serial)
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