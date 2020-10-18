namespace Server.Items
{
    public class SummonCreatureScroll : SpellScroll
    {
        [Constructible]
        public SummonCreatureScroll() : this(1)
        {
        }


        [Constructible]
        public SummonCreatureScroll(int amount) : base(Spells.SpellEntry.SummonCreature, 0x1F54, amount)
        {
        }

        [Constructible]
        public SummonCreatureScroll(Serial serial) : base(serial)
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