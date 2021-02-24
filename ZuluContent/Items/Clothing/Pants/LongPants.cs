namespace Server.Items
{
    [Flipable(0x1539, 0x153a)]
    public class LongPants : BasePants
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public LongPants() : this(0)
        {
        }


        [Constructible]
        public LongPants(int hue) : base(0x1539, hue)
        {
            Weight = 2.0;
        }

        [Constructible]
        public LongPants(Serial serial) : base(serial)
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