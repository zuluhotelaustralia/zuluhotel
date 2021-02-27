namespace Server.Items
{
    public class JesterHat : BaseHat
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 2;


        [Constructible]
        public JesterHat() : this(0)
        {
        }


        [Constructible]
        public JesterHat(int hue) : base(0x171C, hue)
        {
            Weight = 1.0;
        }

        [Constructible]
        public JesterHat(Serial serial) : base(serial)
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