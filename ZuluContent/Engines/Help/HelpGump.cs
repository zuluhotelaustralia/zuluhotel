using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.Help
{
    public class HelpGump : Gump
    {
        public static void Initialize()
        {
            EventSink.HelpRequest += EventSink_HelpRequest;
        }

        private static void EventSink_HelpRequest(Mobile mobile)
        {
            foreach (var g in mobile.NetState.Gumps)
                if (g is HelpGump)
                    return;

            if (!PageQueue.CheckAllowedToPage(mobile))
                return;

            if (PageQueue.Contains(mobile))
                mobile.SendMenu(new ContainedMenu(mobile));
            else
                mobile.SendGump(new HelpGump(mobile));
        }

        public HelpGump(Mobile from) : base(300, 200)
        {
            from.CloseGump<HelpGump>();

            AddBackground(0, 0, 540, 430, 2620);
            AddImage(140, -10, 1141);
            AddLabel(210, -7, 50, "Zulu Hotel Help Menu");

            AddPage(0);

            var buttonBgColor = 141;

            // Row 1

            AddLabel(40, 40, 60, "Website");
            AddImage(43, 65, 10460, buttonBgColor);
            AddButton(52, 75, 2103, 2104, 1);

            AddLabel(160, 40, 40, "Wiki");
            AddImage(163, 65, 10460, buttonBgColor);
            AddButton(172, 75, 2103, 2104, 2);

            AddLabel(280, 40, 15, "Discord");
            AddImage(283, 65, 10460, buttonBgColor);
            AddButton(292, 75, 2103, 2104, 3);

            AddLabel(400, 40, 50, "In-Game Help");
            AddImage(403, 65, 10460, buttonBgColor);
            AddButton(412, 75, 2103, 2104, 4);

            // Row 2

            AddLabel(40, 150, 250, "Target Classe");
            AddImage(43, 175, 10460, buttonBgColor);
            AddButton(52, 185, 2103, 2104, 5);

            AddLabel(160, 150, 250, "Show Classe");
            AddImage(163, 175, 10460, buttonBgColor);
            AddButton(172, 185, 2103, 2104, 6);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            var from = (PlayerMobile)state.Mobile;

            switch (info.ButtonID)
            {
                case 1: // Website
                {
                    from.LaunchBrowser("https://wiki.zhmodern.com");
                    break;
                }
                case 2: // Wiki
                {
                    from.LaunchBrowser("https://wiki.zhmodern.com");
                    break;
                }
                case 3: // Discord
                {
                    from.LaunchBrowser("https://discord.gg/dhYHdXrB");
                    break;
                }
                case 4: // In-game help
                {
                    from.SendGump(new InGameHelpGump(from));
                    break;
                }
                case 5: // Target classe
                {
                    from.SendGump(new TargetClasseGump());
                    break;
                }
                case 6: // Show classe
                {
                    from.SendGump(new ShowClasseGump(from));
                    break;
                }
            }

            if (info.ButtonID > 0)
                from.SendGump(new HelpGump(from));
        }
    }
}