namespace Server.Items
{
    public class Brimstone : BaseReagent
    {
        [Constructible]
        public Brimstone()
            : this(1)
        {
        }


        [Constructible]
        public Brimstone(int amount)
            : base(0xF7F, amount)
        {
        }

        [Constructible]
        public Brimstone(Serial serial)
            : base(serial)
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