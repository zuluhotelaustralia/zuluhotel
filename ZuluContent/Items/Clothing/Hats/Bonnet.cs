namespace Server.Items
{
    public class Bonnet : BaseHat
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 2;


        [Constructible]
        public Bonnet() : this(0)
        {
        }


        [Constructible]
        public Bonnet(int hue) : base(0x1719, hue)
        {
            Weight = 1.0;
        }

        [Constructible]
        public Bonnet(Serial serial) : base(serial)
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