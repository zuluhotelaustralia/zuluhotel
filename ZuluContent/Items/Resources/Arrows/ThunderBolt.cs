namespace Server.Items
{
    public class ThunderBolt : Item
    {
        public override double DefaultWeight => 0.1;

        public override string DefaultName => "thunder bolt";

        [Constructible]
        public ThunderBolt() : this(1)
        {
        }


        [Constructible]
        public ThunderBolt(int amount) : base(0x1BFB)
        {
            Hue = 0x502;
            Stackable = true;
            Amount = amount;
        }

        [Constructible]
        public ThunderBolt(Serial serial) : base(serial)
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