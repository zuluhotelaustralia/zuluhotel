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
    public class EarthbookGump : Gump
    {
        private Earthbook m_Book;

        public Earthbook Book
        {
            get { return m_Book; }
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
            AddImage(100, 10, 2203);

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
            List<string> entries = m_Book.Entries;

            for (int i = 0; i < 16; ++i)
            {
                string desc;
                int hue;

                if (i < entries.Count)
                {
                    desc = GetName(entries[i]);
                    hue = 0;
                }
                else
                {
                    desc = "Empty";
                    hue = 0;
                }

                // Use spell button
                AddButton(130 + i / 8 * 160, 50 + i % 8 * 20, 2103, 2104, 2 + i, GumpButtonType.Reply, 0);

                // Description label
                AddLabelCropped(145 + i / 8 * 160, 45 + i % 8 * 20, 115, 17, hue, desc);
            }
        }

        public EarthbookGump(Mobile from, Earthbook book) : base(150, 200)
        {
            m_Book = book;

            AddBackground();
            AddIndex();
        }

        public static bool HasSpell(Mobile from, SpellEntry spellId)
        {
            Spellbook book = Spellbook.Find(from, spellId);

            return book != null && book.HasSpell(spellId);
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

            if (buttonID >= 0 && buttonID < m_Book.Entries.Count)
            {
                switch (buttonID)
                {
                    case 0:
                    {
                        new AntidoteSpell(from, m_Book).Cast();
                        break;
                    }
                }
            }
            
            m_Book.Openers.Remove(from);
        }
    }
}