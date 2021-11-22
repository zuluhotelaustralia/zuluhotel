using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server.Commands;
using Server.Gumps;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
    public class Codex : CustomSpellbook
    {
        public override int Hue { get; set; } = 0x66D;
        public override string DefaultName { get; } = "Codex Damnorum";
        public override int BookOffset { get; } = 100;

        public override Dictionary<SpellEntry, int> SpellIcons { get; } = new()
        {
            { SpellEntry.ControlUndead, 20480 }, { SpellEntry.Darkness, 24012 }, { SpellEntry.DecayingRay, 24015 },
            { SpellEntry.SpectresTouch, 20495 }, { SpellEntry.AbyssalFlame, 20488 }, { SpellEntry.AnimateDead, 24010 },
            { SpellEntry.Sacrifice, 20739 }, { SpellEntry.WraithBreath, 23013 }, { SpellEntry.SorcerersBane, 24008 },
            { SpellEntry.SummonSpirit, 23007 }, { SpellEntry.WraithForm, 20493 }, { SpellEntry.WyvernStrike, 20496 },
            { SpellEntry.Kill, 20993 }, { SpellEntry.LicheForm, 20486 }, { SpellEntry.Plague, 20489 },
            { SpellEntry.Spellbind, 20744 }
        };
        
        public override Type SpellType { get; } = typeof(NecromancerSpell);

        [Constructible]
        public Codex() : base(0x42BF) { }

        [Constructible]
        public Codex(Serial serial) : base(serial) { }
        
        public override void OnOpenSpellbook(Mobile from)
        {
            from.SendGump(new CustomSpellbookGump(from, this, 0x2B32));
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
                    book.Content = ulong.MaxValue;
                else
                    book.Content = (1ul << book.BookCount) - 1;

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
        #endregion
    }
}