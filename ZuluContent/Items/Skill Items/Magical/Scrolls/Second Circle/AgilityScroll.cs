namespace Server.Items
{
    public class AgilityScroll : SpellScroll
    {
        [Constructible]
        public AgilityScroll() : this(1)
        {
        }


        [Constructible]
        public AgilityScroll(int amount) : base(Spells.SpellEntry.Agility, 0x1F35, amount)
        {
        }

        [Constructible]
        public AgilityScroll(Serial serial) : base(serial)
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