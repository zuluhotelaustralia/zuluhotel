using Scripts.Zulu.Engines.CustomSpellHotBar;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Gumps
{
    public class CustomSpellbookGump : Gump
    {
        private readonly int m_GumpID;

        public CustomSpellbook Book { get; }

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
            
            // First four page buttons
            for (int i = 0, xOffset = 155, gumpID = 2225; i < 4; ++i, xOffset += 35, ++gumpID)
                AddButton(xOffset, 195, gumpID, gumpID, 0, GumpButtonType.Page, 2 + i);

            // Next four page buttons
            for (int i = 0, xOffset = 325, gumpID = 2229; i < 4; ++i, xOffset += 35, ++gumpID)
                AddButton(xOffset, 195, gumpID, gumpID, 0, GumpButtonType.Page, 6 + i);
            
            AddButton(105, 113, 2224, 2224, 18);
        }

        private void AddIndex()
        {
            // Index
            AddPage(1);
            
            // Circle 1 spells
            AddHtml(160, 22, 100, 18, "Circle 1 Spells");

            // Circle 2 spells
            AddHtml(320, 22, 100, 18, "Circle 2 Spells");

            // List of entries
            var entries = Book.Content;

            var negativeOffset = 0;

            for (var i = 0; i < 16; ++i)
            {
                if (i % 8 == 0)
                    negativeOffset = 0;

                if ((entries & ((ulong)1 << i)) != 0)
                {
                    var desc = GetName(SpellRegistry.GetInfo((SpellEntry)Book.BookOffset + i).Name);
                    var hue = 0;

                    // Use spell button
                    AddButton(160 + i / 8 * 160, 48 + (i % 8 * 18 - negativeOffset % 8 * 18), 2103, 2104, 2 + i);

                    // Description label
                    AddLabelCropped(175 + i / 8 * 160, 43 + (i % 8 * 18 - negativeOffset % 8 * 18), 115, 17, hue,
                        desc);
                }
                else
                {
                    negativeOffset++;
                }
            }
            
            // Turn page button
            AddButton(420, 18, 2206, 2206, 0, GumpButtonType.Page, 2);
        }
        
        private void AddDetails(int index, int half)
        {
            var spellEntry = (SpellEntry) Book.BookOffset + index;
            var info = SpellRegistry.GetInfo(spellEntry);
            var spellIcon = Book.SpellIcons[spellEntry];
            var desc = GetName(info.Name);
            
            // Pin gump button
            AddImage(155 + half * 160, 40, spellIcon, Book.HasSpell(spellEntry) ? 0 : 37);

            // Description label
            AddLabelCropped(205 + half * 160, 40, 80, 17, 20, desc);
            
            AddImage(155 + half * 160, 90, 57);
            AddImageTiled(160 + half * 160, 90, 100, 20, 58);
            AddImage(260 + half * 160, 90, 59);
            
            // Reagents label
            AddLabelCropped(155 + half * 160, 110, 115, 17, 20, "Reagents");

            for (var i = 0; i < info.Reagents.Length; i++)
            {
                var yPos = i % 4;
                AddLabelCropped((i < 4 ? 155 : 225) + half * 160, 130 + (yPos * 15), 70, 17, 0, info.Reagents[i].Name);
            }
        }

        public CustomSpellbookGump(Mobile from, CustomSpellbook book, int gumpID) : base(150, 200)
        {
            Book = book;
            m_GumpID = gumpID;

            AddBackground();
            AddIndex();
            
            for (int page = 0; page < 8; ++page)
            {
                AddPage(2 + page);

                AddButton(150, 18, 2205, 2205, 0, GumpButtonType.Page, 1 + page);

                if (page < 7)
                    AddButton(420, 18, 2206, 2206, 0, GumpButtonType.Page, 3 + page);

                for (int half = 0; half < 2; ++half)
                    AddDetails(page * 2 + half, half);
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            var from = state.Mobile as PlayerMobile;

            if (Book.Deleted || !from.InRange(Book.GetWorldLocation(), 1))
            {
                Book.Openers.Remove(from);
                return;
            }

            var buttonID = info.ButtonID;
            buttonID -= 2;

            if (buttonID >= 0 && buttonID < 16)
            {
                OnSpellClick(from, (SpellEntry)(Book.BookOffset + buttonID));
            }

            if (buttonID == 16)
            {
                var customSpellHotBar = from.CustomSpellHotBars.Find(hotBar => hotBar.Book == Book);

                if (customSpellHotBar == null)
                {
                    customSpellHotBar = new CustomSpellHotBar(from, new Point2D(50, 50), Book);
                    from.CustomSpellHotBars.Add(customSpellHotBar);
                }
                
                from.SendGump(new CustomSpellHotBarGump(customSpellHotBar));
            }

            Book.Openers.Remove(from);
        }

        public virtual void OnSpellClick(Mobile from, SpellEntry entry)
        {
            SpellRegistry.Create(entry, from).Cast();
        }
    }
}