namespace Server.Items
{
    public class GreaterHealScroll : SpellScroll
    {
        [Constructible]
        public GreaterHealScroll() : this(1)
        {
        }


        [Constructible]
        public GreaterHealScroll(int amount) : base(Spells.SpellEntry.GreaterHeal, 0x1F49, amount)
        {
        }

        [Constructible]
        public GreaterHealScroll(Serial serial) : base(serial)
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