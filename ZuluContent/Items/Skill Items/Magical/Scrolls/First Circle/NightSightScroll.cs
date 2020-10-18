namespace Server.Items
{
    public class NightSightScroll : SpellScroll
    {
        [Constructible]
        public NightSightScroll() : this(1)
        {
        }


        [Constructible]
        public NightSightScroll(int amount) : base(Spells.SpellEntry.NightSight, 0x1F33, amount)
        {
        }

        [Constructible]
        public NightSightScroll(Serial ser) : base(ser)
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