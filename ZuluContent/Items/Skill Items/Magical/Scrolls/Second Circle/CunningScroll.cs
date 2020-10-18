namespace Server.Items
{
    public class CunningScroll : SpellScroll
    {
        [Constructible]
        public CunningScroll() : this(1)
        {
        }


        [Constructible]
        public CunningScroll(int amount) : base(Spells.SpellEntry.Cunning, 0x1F36, amount)
        {
        }

        [Constructible]
        public CunningScroll(Serial serial) : base(serial)
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