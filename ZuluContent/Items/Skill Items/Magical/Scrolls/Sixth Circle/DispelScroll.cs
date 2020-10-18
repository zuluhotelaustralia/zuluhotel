namespace Server.Items
{
    public class DispelScroll : SpellScroll
    {
        [Constructible]
        public DispelScroll() : this(1)
        {
        }


        [Constructible]
        public DispelScroll(int amount) : base(Spells.SpellEntry.Dispel, 0x1F55, amount)
        {
        }

        [Constructible]
        public DispelScroll(Serial serial) : base(serial)
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