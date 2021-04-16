namespace Server.Items
{
    public class Seaweed : Item
    {
        [Constructible]
        public Seaweed() : this(1)
        {
        }


        [Constructible]
        public Seaweed(int amount) : base(0xDBA)
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;
        }

        [Constructible]
        public Seaweed(Serial serial) : base(serial)
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