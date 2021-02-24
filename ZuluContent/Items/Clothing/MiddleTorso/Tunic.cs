namespace Server.Items
{
    [Flipable(0x1fa1, 0x1fa2)]
    public class Tunic : BaseMiddleTorso
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public Tunic() : this(0)
        {
        }


        [Constructible]
        public Tunic(int hue) : base(0x1FA1, hue)
        {
            Weight = 5.0;
        }

        [Constructible]
        public Tunic(Serial serial) : base(serial)
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