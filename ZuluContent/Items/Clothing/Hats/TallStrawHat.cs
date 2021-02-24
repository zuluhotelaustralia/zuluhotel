namespace Server.Items
{
    public class TallStrawHat : BaseHat
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;


        [Constructible]
        public TallStrawHat() : this(0)
        {
        }


        [Constructible]
        public TallStrawHat(int hue) : base(0x1716, hue)
        {
            Weight = 1.0;
        }

        [Constructible]
        public TallStrawHat(Serial serial) : base(serial)
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