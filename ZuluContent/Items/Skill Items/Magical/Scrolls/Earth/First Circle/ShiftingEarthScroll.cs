using Server.Spells;

namespace Server.Items
{
    public class ShiftingEarthScroll : EarthSpellScroll
    {
        [Constructible]
        public ShiftingEarthScroll() : this(1)
        {
        }


        [Constructible]
        public ShiftingEarthScroll(int amount) : base(SpellEntry.ShiftingEarth, 0x1F31, amount)
        {
        }

        [Constructible]
        public ShiftingEarthScroll(Serial serial) : base(serial)
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