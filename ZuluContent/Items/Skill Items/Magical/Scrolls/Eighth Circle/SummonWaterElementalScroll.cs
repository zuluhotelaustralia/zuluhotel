namespace Server.Items
{
    public class SummonWaterElementalScroll : SpellScroll
    {
        [Constructible]
        public SummonWaterElementalScroll() : this(1)
        {
        }


        [Constructible]
        public SummonWaterElementalScroll(int amount) : base(Spells.SpellEntry.WaterElemental, 0x1F6C, amount)
        {
        }

        [Constructible]
        public SummonWaterElementalScroll(Serial serial) : base(serial)
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