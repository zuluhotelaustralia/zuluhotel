using Server.Spells;

namespace Server.Items
{
    public class OwlSightScroll : EarthSpellScroll
    {
        [Constructible]
        public OwlSightScroll() : this(1)
        {
        }


        [Constructible]
        public OwlSightScroll(int amount) : base(SpellEntry.OwlSight, 0x1F31, amount)
        {
        }

        [Constructible]
        public OwlSightScroll(Serial serial) : base(serial)
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