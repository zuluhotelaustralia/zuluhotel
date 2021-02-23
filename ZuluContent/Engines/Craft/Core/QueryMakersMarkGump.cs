using System;
using Server.Gumps;
using Server.Items;

namespace Server.Engines.Craft
{
    public class QueryMakersMarkGump : Gump, ICraftGump
    {
        public Mobile From { get; }
        public CraftItem CraftItem { get; }
        public CraftSystem CraftSystem { get; }
        public BaseTool Tool { get; }
        private readonly int m_Mark;
        private readonly double m_Quality;
        private readonly Type m_TypeRes;

        public QueryMakersMarkGump(int mark, double quality, Mobile from, CraftItem craftItem, CraftSystem craftSystem,
            Type typeRes, BaseTool tool) : base(100, 200)
        {
            from.CloseGump<QueryMakersMarkGump>();

            m_Mark = mark;
            m_Quality = quality;
            From = from;
            CraftItem = craftItem;
            CraftSystem = craftSystem;
            m_TypeRes = typeRes;
            Tool = tool;

            AddPage(0);

            AddBackground(0, 0, 220, 170, 5054);
            AddBackground(10, 10, 200, 150, 3000);

            AddHtmlLocalized(20, 20, 180, 80, 1018317, false,
                false); // Do you wish to place your maker's mark on this item?

            AddHtmlLocalized(55, 100, 140, 25, 1011011, false, false); // CONTINUE
            AddButton(20, 100, 4005, 4007, 1, GumpButtonType.Reply, 0);

            AddHtmlLocalized(55, 125, 140, 25, 1011012, false, false); // CANCEL
            AddButton(20, 125, 4005, 4007, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(Network.NetState sender, RelayInfo info)
        {
            bool makersMark = info.ButtonID == 1;

            if (makersMark)
                From.SendLocalizedMessage(501808); // You mark the item.
            else
                From.SendLocalizedMessage(501809); // Cancelled mark.

            CraftItem.CompleteCraft(m_Mark, m_Quality, makersMark, From, CraftSystem, m_TypeRes, Tool, null);
        }
    }
}