using Server.Spells;

namespace Server.Items
{
    public class NecroSpellScroll : CustomSpellScroll
    {
        [Constructible]
        public NecroSpellScroll(SpellEntry spellEntry, int itemId, int amount) : base(spellEntry, itemId, amount, 0x66D)
        {
        }

        [Constructible]
        public NecroSpellScroll(Serial serial) : base(serial)
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