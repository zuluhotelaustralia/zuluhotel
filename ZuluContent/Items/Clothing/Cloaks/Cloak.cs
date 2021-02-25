namespace Server.Items
{
    [Flipable]
    public class Cloak : BaseCloak
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public Cloak() : this(0)
        {
        }


        [Constructible]
        public Cloak(int hue) : base(0x1515, hue)
        {
            Weight = 5.0;
        }

        [Constructible]
        public Cloak(Serial serial) : base(serial)
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