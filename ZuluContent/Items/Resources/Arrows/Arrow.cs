namespace Server.Items
{
    public class Arrow : Item
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }


        [Constructible]
        public Arrow() : this(1)
        {
        }


        [Constructible]
        public Arrow(int amount) : base(0xF3F)
        {
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
        public Arrow(Serial serial) : base(serial)
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