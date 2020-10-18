namespace Server.Items
{
    public class ManaDrainScroll : SpellScroll
    {
        [Constructible]
        public ManaDrainScroll() : this(1)
        {
        }


        [Constructible]
        public ManaDrainScroll(int amount) : base(Spells.SpellEntry.ManaDrain, 0x1F4B, amount)
        {
        }

        [Constructible]
        public ManaDrainScroll(Serial serial) : base(serial)
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