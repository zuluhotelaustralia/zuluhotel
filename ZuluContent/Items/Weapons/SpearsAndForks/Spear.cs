namespace Server.Items
{
    [FlipableAttribute(0xF62, 0xF63)]
    public class Spear : BaseSpear
    {
        public override int DefaultStrengthReq { get; } = 30;

        public override int DefaultMinDamage { get; } = 2;

        public override int DefaultMaxDamage { get; } = 36;

        public override int DefaultSpeed { get; } = 46;

        public override int InitMinHits { get; } = 31;

        public override int InitMaxHits { get; } = 80;

        public override int DefaultMaxRange { get; } = 2;


        [Constructible]
        public Spear() : base(0xF62)
        {
            Weight = 7.0;
        }

        [Constructible]
        public Spear(Serial serial) : base(serial)
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