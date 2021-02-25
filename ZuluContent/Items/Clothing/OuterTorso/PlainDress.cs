namespace Server.Items
{
    [Flipable(0x1f01, 0x1f02)]
    public class PlainDress : BaseOuterTorso
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public PlainDress() : this(0)
        {
        }


        [Constructible]
        public PlainDress(int hue) : base(0x1F01, hue)
        {
            Weight = 2.0;
        }

        [Constructible]
        public PlainDress(Serial serial) : base(serial)
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

            if (Weight == 3.0)
                Weight = 2.0;
        }
    }
}