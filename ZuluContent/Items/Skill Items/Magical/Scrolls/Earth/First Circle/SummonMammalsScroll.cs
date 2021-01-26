using Server.Spells;

namespace Server.Items
{
    public class SummonMammalsScroll : EarthSpellScroll
    {
        [Constructible]
        public SummonMammalsScroll() : this(1)
        {
        }


        [Constructible]
        public SummonMammalsScroll(int amount) : base(SpellEntry.SummonMammals, 0x1F32, amount)
        {
        }

        [Constructible]
        public SummonMammalsScroll(Serial serial) : base(serial)
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