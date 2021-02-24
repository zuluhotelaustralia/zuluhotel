namespace Server.Items
{
    [Flipable(0x1f7b, 0x1f7c)]
    public class Doublet : BaseMiddleTorso
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public Doublet() : this(0)
        {
        }


        [Constructible]
        public Doublet(int hue) : base(0x1F7B, hue)
        {
            Weight = 2.0;
        }

        [Constructible]
        public Doublet(Serial serial) : base(serial)
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