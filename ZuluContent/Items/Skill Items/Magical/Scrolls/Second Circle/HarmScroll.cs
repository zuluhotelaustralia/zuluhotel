namespace Server.Items
{
    public class HarmScroll : SpellScroll
    {
        [Constructible]
        public HarmScroll() : this(1)
        {
        }


        [Constructible]
        public HarmScroll(int amount) : base(Spells.SpellEntry.Harm, 0x1F38, amount)
        {
        }

        [Constructible]
        public HarmScroll(Serial serial) : base(serial)
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