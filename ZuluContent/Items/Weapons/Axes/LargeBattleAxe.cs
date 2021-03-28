namespace Server.Items
{
    [FlipableAttribute(0x13FB, 0x13FA)]
    public class LargeBattleAxe : BaseAxe
    {
        public override int DefaultStrengthReq => 45;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 20;
        public override int DefaultSpeed => 38;
        public override int InitMinHits => 110;
        public override int InitMaxHits => 110;


        [Constructible]
        public LargeBattleAxe() : base(0x13FB)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public LargeBattleAxe(Serial serial) : base(serial)
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