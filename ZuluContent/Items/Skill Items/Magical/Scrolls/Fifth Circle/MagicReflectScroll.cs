namespace Server.Items
{
    public class MagicReflectScroll : SpellScroll
    {
        [Constructible]
        public MagicReflectScroll() : this(1)
        {
        }


        [Constructible]
        public MagicReflectScroll(int amount) : base(Spells.SpellEntry.MagicReflect, 0x1F50, amount)
        {
        }

        [Constructible]
        public MagicReflectScroll(Serial serial) : base(serial)
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