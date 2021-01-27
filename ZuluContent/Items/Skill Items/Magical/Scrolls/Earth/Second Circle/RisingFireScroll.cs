using Server.Spells;

namespace Server.Items
{
    public class RisingFireScroll : EarthSpellScroll
    {
        [Constructible]
        public RisingFireScroll() : this(1)
        {
        }


        [Constructible]
        public RisingFireScroll(int amount) : base(SpellEntry.RisingFire, 0x1F32, amount)
        {
        }

        [Constructible]
        public RisingFireScroll(Serial serial) : base(serial)
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