namespace Server.Items
{
    public class MagicWand : BaseBashing
    {
        public override int DefaultMinDamage => 2;
        public override int DefaultMaxDamage => 6;
        public override int DefaultSpeed => 35;
        public override int InitMinHits => 110;
        public override int InitMaxHits => 110;

        [Constructible]
        public MagicWand() : base(0xDF2)
        {
            Weight = 1.0;
        }

        [Constructible]
        public MagicWand(Serial serial) : base(serial)
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