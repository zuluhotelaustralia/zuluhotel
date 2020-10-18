namespace Server.Items
{
    public class Garlic : BaseReagent
    {
        [Constructible]
        public Garlic() : this(1)
        {
        }


        [Constructible]
        public Garlic(int amount) : base(0xF84, amount)
        {
        }

        [Constructible]
        public Garlic(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}