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

        public static bool HasSpell(Mobile from, SpellEntry spellId)
        {
            Spellbook book = Spellbook.Find(from, spellId);

            return book != null && book.HasSpell(spellId);
        }

        public override void OnSpellClick(Mobile from, int buttonID)
        {
            switch (buttonID)
            {
                case 0:
                {
                    new AntidoteSpell(from, Book).Cast();
                    break;
                }
                case 1:
                {
                    new OwlSightSpell(from, Book).Cast();
                    break;
                }
                case 2:
                {
                    new ShiftingEarthSpell(from, Book).Cast();
                    break;
                }
                case 3:
                {
                    new SummonMammalsSpell(from, Book).Cast();
                    break;
                }
                case 4:
                {
                    new CallLightningSpell(from, Book).Cast();
                    break;
                }
                case 5:
                {
                    new EarthsBlessingSpell(from, Book).Cast();
                    break;
                }
                case 6:
                {
                    new EarthPortalSpell(from, Book).Cast();
                    break;
                }
                case 7:
                {
                    new NaturesTouchSpell(from, Book).Cast();
                    break;
                }
                case 8:
                {
                    new GustOfAirSpell(from, Book).Cast();
                    break;
                }
                case 9:
                {
                    new RisingFireSpell(from, Book).Cast();
                    break;
                }
                case 10:
                {
                    new ShapeshiftSpell(from, Book).Cast();
                    break;
                }
                case 11:
                {
                    new IceStrikeSpell(from, Book).Cast();
                    break;
                }
                case 12:
                {
                    new EarthSpiritSpell(from, Book).Cast();
                    break;
                }
                case 13:
                {
                    new FireSpiritSpell(from, Book).Cast();
                    break;
                }
                case 14:
                {
                    new StormSpiritSpell(from, Book).Cast();
                    break;
                }
                case 15:
                {
                    new WaterSpiritSpell(from, Book).Cast();
                    break;
                }
            }
        }
    }
}