namespace Server.Items
{
    public class SummonAirElementalScroll : SpellScroll
    {
        [Constructible]
        public SummonAirElementalScroll() : this(1)
        {
        }


        [Constructible]
        public SummonAirElementalScroll(int amount) : base(Spells.SpellEntry.AirElemental, 0x1F68, amount)
        {
        }

        [Constructible]
        public SummonAirElementalScroll(Serial serial) : base(serial)
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