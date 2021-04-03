namespace Server.Items
{
    public class Obsidian : BaseReagent
    {
        [Constructible]
        public Obsidian()
            : this(1)
        {
        }


        [Constructible]
        public Obsidian(int amount)
            : base(0xF89, amount)
        {
        }

        [Constructible]
        public Obsidian(Serial serial)
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