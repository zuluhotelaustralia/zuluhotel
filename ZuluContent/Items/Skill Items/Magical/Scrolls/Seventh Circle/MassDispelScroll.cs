namespace Server.Items
{
    public class MassDispelScroll : SpellScroll
    {
        [Constructible]
        public MassDispelScroll() : this(1)
        {
        }


        [Constructible]
        public MassDispelScroll(int amount) : base(Spells.SpellEntry.MassDispel, 0x1F62, amount)
        {
        }

        [Constructible]
        public MassDispelScroll(Serial serial) : base(serial)
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