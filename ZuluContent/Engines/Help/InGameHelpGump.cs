using System;
using Server.Gumps;
using Server.Menus.Questions;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Regions;

namespace Server.Engines.Help
{
    public class ContainedMenu : QuestionMenu
    {
        private readonly Mobile m_From;

        public ContainedMenu(Mobile from) : base(
            "You already have an open help request. We will have someone assist you as soon as possible.  What would you like to do?",
            new[] { "Leave my old help request like it is.", "Remove my help request from the queue." })
        {
            m_From = from;
        }

        public override void OnCancel(NetState state)
        {
            m_From.SendLocalizedMessage(1005306, "", 0x35); // Help request unchanged.
        }

        public override void OnResponse(NetState state, int index)
        {
            if (index == 0)
            {
                m_From.SendLocalizedMessage(1005306, "", 0x35); // Help request unchanged.
            }
            else if (index == 1)
            {
                var entry = PageQueue.GetEntry(m_From);

                if (entry != null && entry.Handler == null)
                {
                    m_From.SendLocalizedMessage(1005307, "", 0x35); // Removed help request.
                    // entry.AddResponse(entry.Sender, "[Canceled]");
                    PageQueue.Remove(entry);
                }
                else
                {
                    m_From.SendLocalizedMessage(1005306, "", 0x35); // Help request unchanged.
                }
            }
        }
    }

    public class InGameHelpGump : Gump
    {
        public static bool CheckCombat(Mobile m)
        {
            for (var i = 0; i < m.Aggressed.Count; ++i)
            {
                var info = m.Aggressed[i];

                if (DateTime.UtcNow - info.LastCombatTime < TimeSpan.FromSeconds(30.0))
                    return true;
            }

            return false;
        }

        public InGameHelpGump(Mobile from) : base(0, 0)
        {
            from.CloseGump<InGameHelpGump>();
            ;

            AddBackground(50, 25, 540, 430, 2600);

            AddPage(0);

            AddHtmlLocalized(150, 50, 360, 40, 1001002); // <CENTER><U>Ultima Online Help Menu</U></CENTER>
            AddButton(425, 415, 2073, 2072, 0); // Close

            AddPage(1);

            AddButton(80, 90, 5540, 5541, 1, GumpButtonType.Reply, 2);
            AddHtml(110, 90, 450, 74,
                @"<u>General question about Ultima Online.</u> Select this option if you have a general gameplay question, need help learning to use a skill, or if you would like to search the UO Knowledge Base.",
                true, true);

            AddButton(80, 170, 5540, 5541, 2);
            AddHtml(110, 170, 450, 74,
                @"<u>My character is physically stuck in the game.</u> This choice only covers cases where your character is physically stuck in a location they cannot move out of. This option will only work two times in 24 hours.",
                true, true);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            var from = state.Mobile;

            var type = (PageType)(-1);

            switch (info.ButtonID)
            {
                case 0: // Close/Cancel
                {
                    from.SendLocalizedMessage(501235, "", 0x35); // Help request aborted.

                    break;
                }
                case 1: // General question
                {
                    type = PageType.Question;
                    break;
                }
                case 2: // Stuck
                {
                    var house = BaseHouse.FindHouseAt(from);

                    if (house != null)
                    {
                        from.Location = house.BanLocation;
                    }
                    else if (from.Region.IsPartOf<JailRegion>())
                    {
                        from.SendLocalizedMessage(1114345, "", 0x35); // You'll need a better jailbreak plan than that!
                    }
                    else if (from is PlayerMobile && from.Region.CanUseStuckMenu(from) && !CheckCombat(from) &&
                             !from.Frozen && !from.Criminal)
                    {
                        var menu = new StuckMenu(from, from, true);

                        menu.BeginClose();

                        from.SendGump(menu);
                    }
                    else
                    {
                        type = PageType.Stuck;
                    }

                    break;
                }
            }

            if (type != (PageType)(-1) && PageQueue.CheckAllowedToPage(from))
                from.SendGump(new PagePromptGump(from, type));
        }
    }
}