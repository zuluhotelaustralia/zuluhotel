namespace Server.Items
{
    [FlipableAttribute(0xF47, 0xF48)]
    public class BattleAxe : BaseAxe
    {
        public override int DefaultStrengthReq => 45;
        public override int DefaultMinDamage => 3;
        public override int DefaultMaxDamage => 18;
        public override int DefaultSpeed => 38;
        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;

        [Constructible]
        public BattleAxe() : base(0xF47)
        {
            Weight = 4.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public BattleAxe(Serial serial) : base(serial)
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