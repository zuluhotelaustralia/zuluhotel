namespace Server.Items
{
    public class HealScroll : SpellScroll
    {
        [Constructible]
        public HealScroll() : this(1)
        {
        }


        [Constructible]
        public HealScroll(int amount) : base(Spells.SpellEntry.Heal, 0x1F31, amount)
        {
        }

        [Constructible]
        public HealScroll(Serial serial) : base(serial)
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