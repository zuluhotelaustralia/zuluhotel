namespace Server.Items
{
    public class MagicTrapScroll : SpellScroll
    {
        [Constructible]
        public MagicTrapScroll() : this(1)
        {
        }


        [Constructible]
        public MagicTrapScroll(int amount) : base(Spells.SpellEntry.MagicTrap, 0x1F39, amount)
        {
        }

        [Constructible]
        public MagicTrapScroll(Serial serial) : base(serial)
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