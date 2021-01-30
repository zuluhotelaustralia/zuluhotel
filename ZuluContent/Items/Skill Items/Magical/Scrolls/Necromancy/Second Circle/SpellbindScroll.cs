using Server.Spells;

namespace Server.Items
{
    public class SpellbindScroll : NecroSpellScroll
    {
        [Constructible]
        public SpellbindScroll() : this(1)
        {
        }


        [Constructible]
        public SpellbindScroll(int amount) : base(SpellEntry.Spellbind, 0x1F38, amount)
        {
        }

        [Constructible]
        public SpellbindScroll(Serial serial) : base(serial)
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