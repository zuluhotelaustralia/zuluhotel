using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Server.Items;
using Server.Network;
using Server.Spells.Fourth;
using Server.Spells.Seventh;
using Server.Prompts;
using Server.Spells;

namespace Server.Gumps
{
    public class CustomSpellbookGump : Gump
    {
        private CustomSpellbook m_Book;

        private int m_GumpID;

        public CustomSpellbook Book
        {
            get => m_Book;
        }

        public string GetName(string name)
        {
            if (name == null || (name = name.Trim()).Length <= 0)
                return "(indescript)";

            return name;
        }

        private void AddBackground()
        {
            AddPage(0);

            // Background image
            AddImage(100, 10, m_GumpID);

            // Circle 1 spells
            AddHtml(130, 22, 100, 18, "Circle 1 Spells", false, false);

            // Circle 2 spells
            AddHtml(290, 22, 100, 18, "Circle 2 Spells", false, false);
        }

        private void AddIndex()
        {
            // Index
            AddPage(1);

            // List of entries
            ulong entries = m_Book.Entries;

            int negativeOffset = 0;

            for (int i = 0; i < 16; ++i)
            {
                if (i % 8 == 0)
                    negativeOffset = 0;

                if ((entries & ((ulong) 1 << i)) != 0)
                {
                    var desc = GetName(SpellRegistry.GetInfo((SpellEntry) m_Book.BookOffset + i).Name);
                    var hue = 0;

                    // Use spell button
                    AddButton(130 + i / 8 * 160, 50 + ((i % 8 * 20) - (negativeOffset % 8 * 20)), 2103, 2104, 2 + i,
                        GumpButtonType.Reply, 0);

                    // Description label
                    AddLabelCropped(145 + i / 8 * 160, 45 + ((i % 8 * 20) - (negativeOffset % 8 * 20)), 115, 17, hue,
                        desc);
                }
                else
                {
                    negativeOffset++;
                }
            }
        }

        public CustomSpellbookGump(Mobile from, CustomSpellbook book, int gumpID) : base(150, 200)
        {
            m_Book = book;
            m_GumpID = gumpID;

            AddBackground();
            AddIndex();
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            if (m_Book.Deleted || !from.InRange(m_Book.GetWorldLocation(), 1))
            {
                m_Book.Openers.Remove(from);
                return;
            }

            var buttonID = info.ButtonID;
            buttonID -= 2;

            if (buttonID >= 0 && buttonID < 16)
            {
                OnSpellClick(from, (SpellEntry) (m_Book.BookOffset + buttonID));
            }

            m_Book.Openers.Remove(from);
        }

        public virtual void OnSpellClick(Mobile from, SpellEntry entry)
        {
            SpellRegistry.Create(entry, from, m_Book).Cast();
        }
    }
}