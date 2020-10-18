namespace Server.Items
{
    public class TelekinisisScroll : SpellScroll
    {
        [Constructible]
        public TelekinisisScroll() : this(1)
        {
        }


        [Constructible]
        public TelekinisisScroll(int amount) : base(Spells.SpellEntry.Telekinesis, 0x1F41, amount)
        {
        }

        [Constructible]
        public TelekinisisScroll(Serial serial) : base(serial)
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