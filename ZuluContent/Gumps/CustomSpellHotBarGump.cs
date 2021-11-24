using Scripts.Zulu.Engines.CustomSpellHotBar;
using Scripts.Zulu.Spells.Earth;
using Scripts.Zulu.Spells.Necromancy;
using Scripts.Zulu.Utilities;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Gumps
{
    public class CustomSpellHotBarGump : Gump
    {
        private readonly CustomSpellHotBar m_HotBar;

        public CustomSpellHotBarGump(CustomSpellHotBar hotBar) : base(hotBar.Location.X, hotBar.Location.Y)
        {
            m_HotBar = hotBar;

            AddPage(0);

            if (hotBar.Book.SpellType == typeof(EarthSpell))
            {
                AddImage(0, 0, 11053);
                
            }
            else if (hotBar.Book.SpellType == typeof(NecromancerSpell))
            {
                AddImage(0, 0, 11056);
            }
            
            if (hotBar.Direction == Direction.Right)
            {
                AddButton(20, 60, 5601, 5605, 18);
            }
            else if (hotBar.Direction == Direction.Down)
            {
                AddButton(55, 30, 5602, 5606, 19);
            }

            for (var i = 0; i < 16; i++)
            {
                var xPos = i % 8;
                var x = xPos * 44 + (xPos * 3);
                var y = i < 8 ? 0 : 50;
                var spellEntry = (SpellEntry) m_HotBar.Book.BookOffset + i;
                var spellIcon = hotBar.Book.SpellIcons[spellEntry];

                if (hotBar.Direction == Direction.Right)
                {
                    if (m_HotBar.Book.HasSpell(spellEntry))
                    {
                        AddButton(50 + x, y, spellIcon, spellIcon, 2 + i);
                    }
                    else
                    {
                        AddImage(50 + x, y, spellIcon, 37);
                    }
                }
                else if (hotBar.Direction == Direction.Down)
                {
                    if (m_HotBar.Book.HasSpell(spellEntry))
                    {
                        AddButton(y, 70 + x, spellIcon, spellIcon, 2 + i);
                    }
                    else
                    {
                        AddImage(y, 70 + x, spellIcon, 37);
                    }
                }
                
                AddTooltip(1042971, SpellRegistry.GetInfo(spellEntry).Name);
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            var from = state.Mobile;
            var buttonID = info.ButtonID - 2;

            if (from is PlayerMobile playerMobile)
            {
                if (buttonID == -2)
                {
                    playerMobile.CustomSpellHotBars.Remove(m_HotBar);
                }
                else if (buttonID is >= 0 and < 16)
                {
                    var spellEntry = (SpellEntry)m_HotBar.Book.BookOffset + buttonID;
                    
                    if (m_HotBar.Book.RootParent != from)
                    {
                        playerMobile.SendFailureMessage("That spellbook is no longer in your possession.");
                    }
                    else if (!m_HotBar.Book.HasSpell(spellEntry))
                    {
                        playerMobile.SendFailureMessage("That spellbook does not have that spell.");
                    }
                    else
                    {
                        SpellRegistry.Create(spellEntry, from).Cast();
                    }
                    
                    playerMobile.SendGump(new CustomSpellHotBarGump(m_HotBar));
                }
                else if (buttonID == 16)
                {
                    m_HotBar.Direction = Direction.Down;
                    playerMobile.SendGump(new CustomSpellHotBarGump(m_HotBar));
                }
                else if (buttonID == 17)
                {
                    m_HotBar.Direction = Direction.Right;
                    playerMobile.SendGump(new CustomSpellHotBarGump(m_HotBar));
                }
            }
        }
    }
}