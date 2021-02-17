namespace Server.Items
{
    public class Bottle : Item
    {
        [Constructible]
        public Bottle() : this(1)
        {
        }


        [Constructible]
        public Bottle(int amount) : base(0xF0E)
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;
        }

        [Constructible]
        public Bottle(Serial serial) : base(serial)
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