using Server.Spells;
using static Server.Spells.SpellEntries;

namespace Server.Items
{
    public class CustomSpellScroll : SpellScroll
    {
        [Constructible]
        public CustomSpellScroll(SpellEntry spellEntry, int itemId, int amount, int hue) : base(spellEntry, itemId,
            amount)
        {
            Hue = hue;
            Name = GetSpellEntryName(spellEntry);
        }

        [Constructible]
        public CustomSpellScroll(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (!string.IsNullOrEmpty(Name))
                LabelTo(from, Name);
        }
    }
}