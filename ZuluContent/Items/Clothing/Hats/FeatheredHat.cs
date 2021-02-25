namespace Server.Items
{
    public class FeatheredHat : BaseHat
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;


        [Constructible]
        public FeatheredHat() : this(0)
        {
        }


        [Constructible]
        public FeatheredHat(int hue) : base(0x171A, hue)
        {
            Weight = 1.0;
        }

        [Constructible]
        public FeatheredHat(Serial serial) : base(serial)
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