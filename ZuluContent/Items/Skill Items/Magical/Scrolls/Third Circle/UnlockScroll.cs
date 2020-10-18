namespace Server.Items
{
    public class UnlockScroll : SpellScroll
    {
        [Constructible]
        public UnlockScroll() : this(1)
        {
        }


        [Constructible]
        public UnlockScroll(int amount) : base(Spells.SpellEntry.Unlock, 0x1F43, amount)
        {
        }

        [Constructible]
        public UnlockScroll(Serial serial) : base(serial)
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