using Server.Spells;

namespace Server.Items
{
    public class NaturesTouchScroll : EarthSpellScroll
    {
        [Constructible]
        public NaturesTouchScroll() : this(1)
        {
        }


        [Constructible]
        public NaturesTouchScroll(int amount) : base(SpellEntry.NaturesTouch, 0x1F32, amount)
        {
        }

        [Constructible]
        public NaturesTouchScroll(Serial serial) : base(serial)
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