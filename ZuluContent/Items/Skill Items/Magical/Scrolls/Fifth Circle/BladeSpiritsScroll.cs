namespace Server.Items
{
    public class BladeSpiritsScroll : SpellScroll
    {
        [Constructible]
        public BladeSpiritsScroll() : this(1)
        {
        }


        [Constructible]
        public BladeSpiritsScroll(int amount) : base(Spells.SpellEntry.BladeSpirits, 0x1F4D, amount)
        {
        }

        [Constructible]
        public BladeSpiritsScroll(Serial serial) : base(serial)
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