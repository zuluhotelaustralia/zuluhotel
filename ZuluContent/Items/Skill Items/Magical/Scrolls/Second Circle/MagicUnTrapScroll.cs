namespace Server.Items
{
    public class MagicUnTrapScroll : SpellScroll
    {
        [Constructible]
        public MagicUnTrapScroll() : this(1)
        {
        }


        [Constructible]
        public MagicUnTrapScroll(int amount) : base(Spells.SpellEntry.RemoveTrap, 0x1F3A, amount)
        {
        }

        [Constructible]
        public MagicUnTrapScroll(Serial serial) : base(serial)
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