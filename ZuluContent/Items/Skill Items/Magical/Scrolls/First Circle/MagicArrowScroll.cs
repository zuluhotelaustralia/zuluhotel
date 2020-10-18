namespace Server.Items
{
    public class MagicArrowScroll : SpellScroll
    {
        [Constructible]
        public MagicArrowScroll() : this(1)
        {
        }


        [Constructible]
        public MagicArrowScroll(int amount) : base(Spells.SpellEntry.MagicArrow, 0x1F32, amount)
        {
        }

        [Constructible]
        public MagicArrowScroll(Serial serial) : base(serial)
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