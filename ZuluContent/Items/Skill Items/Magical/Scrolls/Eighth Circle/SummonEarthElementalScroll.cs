namespace Server.Items
{
    public class SummonEarthElementalScroll : SpellScroll
    {
        [Constructible]
        public SummonEarthElementalScroll() : this(1)
        {
        }


        [Constructible]
        public SummonEarthElementalScroll(int amount) : base(Spells.SpellEntry.EarthElemental, 0x1F6A, amount)
        {
        }

        [Constructible]
        public SummonEarthElementalScroll(Serial serial) : base(serial)
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