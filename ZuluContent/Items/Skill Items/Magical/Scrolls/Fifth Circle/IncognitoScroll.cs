namespace Server.Items
{
    public class IncognitoScroll : SpellScroll
    {
        [Constructible]
        public IncognitoScroll() : this(1)
        {
        }


        [Constructible]
        public IncognitoScroll(int amount) : base(Spells.SpellEntry.Incognito, 0x1F4F, amount)
        {
        }

        [Constructible]
        public IncognitoScroll(Serial serial) : base(serial)
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