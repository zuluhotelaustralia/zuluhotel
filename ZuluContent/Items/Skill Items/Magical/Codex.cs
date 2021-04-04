using Server.Commands;
using Server.Gumps;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
    public class Codex : CustomSpellbook
    {
        public static void Initialize()
        {
            CommandSystem.Register("AllCodexSpells", AccessLevel.GameMaster, AllSpells_OnCommand);
        }

        [Usage("AllCodexSpells")]
        [Description("Completely fills a targeted Codex with scrolls.")]
        private static void AllSpells_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, AllSpells_OnTarget);
            e.Mobile.SendMessage("Target the Codex to fill.");
        }

        private static void AllSpells_OnTarget(Mobile from, object obj)
        {
            if (obj is Codex book)
            {
                if (book.BookCount == 16)
                    book.Entries = ulong.MaxValue;
                else
                    book.Entries = (1ul << book.BookCount) - 1;

                from.SendMessage("The Codex has been filled.");

                CommandLogging.WriteLine(from, "{0} {1} filling Codex {2}", from.AccessLevel,
                    CommandLogging.Format(from), CommandLogging.Format(book));
            }
            else
            {
                from.BeginTarget(-1, false, TargetFlags.None, AllSpells_OnTarget);
                from.SendMessage("That is not a Codex. Try again.");
            }
        }
        
        public override int Hue { get; set; } = 0x66D;
        public override string DefaultName { get; } = "Codex Damnorum";
        public override int BookOffset { get; } = 100;

        [Constructible]
        public Codex() : base(0x1C13) { }

        [Constructible]
        public Codex(Serial serial) : base(serial) { }
        
        public override bool CanAddEntry(Mobile from, CustomSpellScroll scroll)
        {
            return scroll.SpellEntry >= SpellEntry.ControlUndead && scroll.SpellEntry <= SpellEntry.Spellbind &&
                   base.CanAddEntry(from, scroll);
        }

        public override void OnOpenSpellbook(Mobile from)
        {
            from.SendGump(new CustomSpellbookGump(from, this, 0x898));
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