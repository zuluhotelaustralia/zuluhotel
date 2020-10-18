namespace Server.Items
{
    public class SpidersSilk : BaseReagent
    {
        [Constructible]
        public SpidersSilk() : this(1)
        {
        }


        [Constructible]
        public SpidersSilk(int amount) : base(0xF8D, amount)
        {
        }

        [Constructible]
        public SpidersSilk(Serial serial) : base(serial)
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