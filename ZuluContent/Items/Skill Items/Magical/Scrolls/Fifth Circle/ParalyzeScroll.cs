namespace Server.Items
{
    public class ParalyzeScroll : SpellScroll
    {
        [Constructible]
        public ParalyzeScroll() : this(1)
        {
        }


        [Constructible]
        public ParalyzeScroll(int amount) : base(Spells.SpellEntry.Paralyze, 0x1F52, amount)
        {
        }

        [Constructible]
        public ParalyzeScroll(Serial serial) : base(serial)
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