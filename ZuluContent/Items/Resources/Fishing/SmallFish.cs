namespace Server.Items
{
    public class SmallFish : Item
    {
        [Constructible]
        public SmallFish() : this(1)
        {
        }


        [Constructible]
        public SmallFish(int amount) : base(Utility.Random(0x0DD6, 1))
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
        }

        [Constructible]
        public SmallFish(Serial serial) : base(serial)
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