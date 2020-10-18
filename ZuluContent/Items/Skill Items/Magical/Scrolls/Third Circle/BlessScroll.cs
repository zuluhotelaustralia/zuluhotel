namespace Server.Items
{
    public class BlessScroll : SpellScroll
    {
        [Constructible]
        public BlessScroll() : this(1)
        {
        }


        [Constructible]
        public BlessScroll(int amount) : base(Spells.SpellEntry.Bless, 0x1F3D, amount)
        {
        }

        [Constructible]
        public BlessScroll(Serial serial) : base(serial)
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