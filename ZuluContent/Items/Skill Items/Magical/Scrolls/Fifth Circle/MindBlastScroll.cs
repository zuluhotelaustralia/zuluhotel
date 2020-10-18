namespace Server.Items
{
    public class MindBlastScroll : SpellScroll
    {
        [Constructible]
        public MindBlastScroll() : this(1)
        {
        }


        [Constructible]
        public MindBlastScroll(int amount) : base(Spells.SpellEntry.MindBlast, 0x1F51, amount)
        {
        }

        [Constructible]
        public MindBlastScroll(Serial serial) : base(serial)
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