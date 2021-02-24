namespace Server.Items
{
    [Flipable(0x1517, 0x1518)]
    public class Shirt : BaseShirt
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public Shirt() : this(0)
        {
        }


        [Constructible]
        public Shirt(int hue) : base(0x1517, hue)
        {
            Weight = 1.0;
        }

        [Constructible]
        public Shirt(Serial serial) : base(serial)
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

            if (Weight == 2.0)
                Weight = 1.0;
        }
    }
}