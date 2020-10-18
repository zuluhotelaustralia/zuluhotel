namespace Server.Items
{
    public class MassCurseScroll : SpellScroll
    {
        [Constructible]
        public MassCurseScroll() : this(1)
        {
        }


        [Constructible]
        public MassCurseScroll(int amount) : base(Spells.SpellEntry.MassCurse, 0x1F5A, amount)
        {
        }

        [Constructible]
        public MassCurseScroll(Serial serial) : base(serial)
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