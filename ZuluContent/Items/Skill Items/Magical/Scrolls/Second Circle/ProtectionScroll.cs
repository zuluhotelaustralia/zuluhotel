namespace Server.Items
{
    public class ProtectionScroll : SpellScroll
    {
        [Constructible]
        public ProtectionScroll() : this(1)
        {
        }


        [Constructible]
        public ProtectionScroll(int amount) : base(Spells.SpellEntry.Protection, 0x1F3B, amount)
        {
        }

        [Constructible]
        public ProtectionScroll(Serial serial) : base(serial)
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