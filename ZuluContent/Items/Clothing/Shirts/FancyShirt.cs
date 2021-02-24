namespace Server.Items
{
    [Flipable(0x1efd, 0x1efe)]
    public class FancyShirt : BaseShirt
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public FancyShirt() : this(0)
        {
        }


        [Constructible]
        public FancyShirt(int hue) : base(0x1EFD, hue)
        {
            Weight = 2.0;
        }

        [Constructible]
        public FancyShirt(Serial serial) : base(serial)
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