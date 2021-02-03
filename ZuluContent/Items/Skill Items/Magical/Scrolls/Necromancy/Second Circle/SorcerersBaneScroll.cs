using Server.Spells;

namespace Server.Items
{
    public class SorcerersBaneScroll : NecroSpellScroll
    {
        [Constructible]
        public SorcerersBaneScroll() : this(1)
        {
        }


        [Constructible]
        public SorcerersBaneScroll(int amount) : base(SpellEntry.SorcerersBane, 0x1F36, amount)
        {
        }

        [Constructible]
        public SorcerersBaneScroll(Serial serial) : base(serial)
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