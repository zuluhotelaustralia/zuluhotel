using Server.Spells;

namespace Server.Items
{
    public class FlameSpiritScroll : EarthSpellScroll
    {
        [Constructible]
        public FlameSpiritScroll() : this(1)
        {
        }


        [Constructible]
        public FlameSpiritScroll(int amount) : base(SpellEntry.FireSpirit, 0x1F32, amount)
        {
        }

        [Constructible]
        public FlameSpiritScroll(Serial serial) : base(serial)
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