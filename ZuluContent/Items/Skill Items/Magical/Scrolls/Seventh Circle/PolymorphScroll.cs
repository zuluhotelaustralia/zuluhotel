namespace Server.Items
{
    public class PolymorphScroll : SpellScroll
    {
        [Constructible]
        public PolymorphScroll() : this(1)
        {
        }


        [Constructible]
        public PolymorphScroll(int amount) : base(Spells.SpellEntry.Polymorph, 0x1F64, amount)
        {
        }

        [Constructible]
        public PolymorphScroll(Serial serial) : base(serial)
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