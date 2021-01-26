using Server.Spells;

namespace Server.Items
{
    public class WaterSpiritScroll : EarthSpellScroll
    {
        [Constructible]
        public WaterSpiritScroll() : this(1)
        {
        }


        [Constructible]
        public WaterSpiritScroll(int amount) : base(SpellEntry.WaterSpirit, 0x1F32, amount)
        {
        }

        [Constructible]
        public WaterSpiritScroll(Serial serial) : base(serial)
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