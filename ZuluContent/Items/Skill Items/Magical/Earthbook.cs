using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Server.Commands;
using Server.Gumps;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
    public class Earthbook : CustomSpellbook
    {
        public override int Hue { get; set; } = 0x48A;
        public override string DefaultName { get; } = "Book of the Earth";
        public override int BookOffset { get; } = 600;
        
        public override Dictionary<SpellEntry, int> SpellIcons { get; } = new()
        {
            { SpellEntry.Antidote, 20481 }, { SpellEntry.OwlSight, 23002 }, { SpellEntry.ShiftingEarth, 20999 },
            { SpellEntry.SummonMammals, 20491 }, { SpellEntry.CallLightning, 24000 }, { SpellEntry.EarthsBlessing, 23001 },
            { SpellEntry.EarthPortal, 2275 }, { SpellEntry.NaturesTouch, 20740 }, { SpellEntry.GustOfAir, 24014 },
            { SpellEntry.RisingFire, 2290 }, { SpellEntry.Shapeshift, 24005 }, { SpellEntry.IceStrike, 24011 },
            { SpellEntry.EarthSpirit, 2301 }, { SpellEntry.FireSpirit, 2302 }, { SpellEntry.StormSpirit, 2299 },
            { SpellEntry.WaterSpirit, 2303 }
        };

        public override Type SpellType { get; } = typeof(EarthSpell);

        [Constructible]
        public Earthbook() : base(0x42B7) { }

        [Constructible]
        public Earthbook(Serial serial) : base(serial) { }
        
        public override void OnOpenSpellbook(Mobile from)
        {
            from.SendGump(new CustomSpellbookGump(from, this, 0x2B2F));
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
        
        #region Commands
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
                    book.Content = ulong.MaxValue;
                else
                    book.Content = (1ul << book.BookCount) - 1;

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
        #endregion

    }
}