namespace Server.Items
{
    public class ResurrectionScroll : SpellScroll
    {
        [Constructible]
        public ResurrectionScroll() : this(1)
        {
        }


        [Constructible]
        public ResurrectionScroll(int amount) : base(Spells.SpellEntry.Resurrection, 0x1F67, amount)
        {
        }

        [Constructible]
        public ResurrectionScroll(Serial serial) : base(serial)
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