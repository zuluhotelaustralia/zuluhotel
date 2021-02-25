namespace Server.Items
{
    [Flipable(0x1516, 0x1531)]
    public class Skirt : BaseOuterLegs
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public Skirt() : this(0)
        {
        }


        [Constructible]
        public Skirt(int hue) : base(0x1516, hue)
        {
            Weight = 4.0;
        }

        [Constructible]
        public Skirt(Serial serial) : base(serial)
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