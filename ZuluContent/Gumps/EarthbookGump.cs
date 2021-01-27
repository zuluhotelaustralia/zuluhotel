using Scripts.Zulu.Spells.Earth;
using Server.Items;
using Server.Spells;

namespace Server.Gumps
{
    public class EarthbookGump : CustomSpellbookGump
    {
        public EarthbookGump(Mobile from, Earthbook book) : base(from, book, 0x89B)
        {
        }

        public override void OnSpellClick(Mobile from, SpellEntry entry)
        {
            SpellRegistry.Create(entry, from, Book).Cast();
        }
    }
}