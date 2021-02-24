namespace Server.Items
{
    [Flipable(0x1F00, 0x1EFF)]
    public class FancyDress : BaseOuterTorso
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public FancyDress() : this(0)
        {
        }


        [Constructible]
        public FancyDress(int hue) : base(0x1F00, hue)
        {
            Weight = 3.0;
        }

        [Constructible]
        public FancyDress(Serial serial) : base(serial)
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