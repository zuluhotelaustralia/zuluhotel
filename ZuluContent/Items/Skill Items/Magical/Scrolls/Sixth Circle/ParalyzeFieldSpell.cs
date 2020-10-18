namespace Server.Items
{
    public class ParalyzeFieldScroll : SpellScroll
    {
        [Constructible]
        public ParalyzeFieldScroll() : this(1)
        {
        }


        [Constructible]
        public ParalyzeFieldScroll(int amount) : base(Spells.SpellEntry.ParalyzeField, 0x1F5B, amount)
        {
        }

        [Constructible]
        public ParalyzeFieldScroll(Serial serial) : base(serial)
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