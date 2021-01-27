using Server.Commands;
using Server.Gumps;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
    public class Earthbook : CustomSpellbook
    {
        public static void Initialize()
        {
            CommandSystem.Register("AllEarthSpells", AccessLevel.GameMaster, AllSpells_OnCommand);
        }

        [Usage("AllEarthSpells")]
        [Description("Completely fills a targeted Earth Book with scrolls.")]
        private static void AllSpells_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, AllSpells_OnTarget);
            e.Mobile.SendMessage("Target the Earth Book to fill.");
        }

        private static void AllSpells_OnTarget(Mobile from, object obj)
        {
            if (obj is Earthbook book)
            {
                if (book.BookCount == 16)
                    book.Entries = ulong.MaxValue;
                else
                    book.Entries = (1ul << book.BookCount) - 1;

                from.SendMessage("The Earth Book has been filled.");

                CommandLogging.WriteLine(from, "{0} {1} filling Earth Book {2}", from.AccessLevel,
                    CommandLogging.Format(from), CommandLogging.Format(book));
            }
            else
            {
                from.BeginTarget(-1, false, TargetFlags.None, AllSpells_OnTarget);
                from.SendMessage("That is not an Earth Book. Try again.");
            }
        }

        [Constructible]
        public Earthbook() : base(0x0FF2)
        {
            Name = "Book of the Earth";
            Hue = 0x48A;
        }

        [Constructible]
        public Earthbook(Serial serial) : base(serial)
        {
        }

        public override int BookOffset
        {
            get { return 600; }
        }

        public override SpellCircle SpellbookCircle
        {
            get => SpellCircle.Earth;
        }

        public override void OnOpenSpellbook(Mobile from)
        {
            from.SendGump(new EarthbookGump(from, this));
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}