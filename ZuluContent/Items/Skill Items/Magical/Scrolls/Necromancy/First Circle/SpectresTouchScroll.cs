using Server.Spells;

namespace Server.Items
{
    public class SpectresTouchScroll : NecroSpellScroll
    {
        [Constructible]
        public SpectresTouchScroll() : this(1)
        {
        }


        [Constructible]
        public SpectresTouchScroll(int amount) : base(SpellEntry.SpectresTouch, 0x1F34, amount)
        {
        }

        [Constructible]
        public SpectresTouchScroll(Serial serial) : base(serial)
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