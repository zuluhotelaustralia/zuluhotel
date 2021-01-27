using Server.Spells;

namespace Server.Items
{
    public class AntidoteScroll : EarthSpellScroll
    {
        [Constructible]
        public AntidoteScroll() : this(1)
        {
        }


        [Constructible]
        public AntidoteScroll(int amount) : base(SpellEntry.Antidote, 0x1F32, amount)
        {
        }

        [Constructible]
        public AntidoteScroll(Serial serial) : base(serial)
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