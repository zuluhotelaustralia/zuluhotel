namespace Server.Items
{
    public class MagicLockScroll : SpellScroll
    {
        [Constructible]
        public MagicLockScroll() : this(1)
        {
        }


        [Constructible]
        public MagicLockScroll(int amount) : base(Spells.SpellEntry.MagicLock, 0x1F3F, amount)
        {
        }

        [Constructible]
        public MagicLockScroll(Serial serial) : base(serial)
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