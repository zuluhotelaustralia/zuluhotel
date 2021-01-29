using Server.Spells;

namespace Server.Items
{
    public class SummonSpiritScroll : NecroSpellScroll
    {
        [Constructible]
        public SummonSpiritScroll() : this(1)
        {
        }


        [Constructible]
        public SummonSpiritScroll(int amount) : base(SpellEntry.SummonSpirit, 0x1F39, amount)
        {
        }

        [Constructible]
        public SummonSpiritScroll(Serial serial) : base(serial)
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