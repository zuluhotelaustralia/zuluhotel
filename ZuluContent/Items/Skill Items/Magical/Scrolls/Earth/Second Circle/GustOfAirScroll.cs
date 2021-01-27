using Server.Spells;

namespace Server.Items
{
    public class GustOfAirScroll : EarthSpellScroll
    {
        [Constructible]
        public GustOfAirScroll() : this(1)
        {
        }


        [Constructible]
        public GustOfAirScroll(int amount) : base(SpellEntry.GustOfAir, 0x1F32, amount)
        {
        }

        [Constructible]
        public GustOfAirScroll(Serial serial) : base(serial)
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