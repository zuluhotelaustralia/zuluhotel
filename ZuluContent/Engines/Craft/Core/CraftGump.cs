using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Engines.Craft
{
    public class CraftGump : Gump, ICraftGump
    {
        public Mobile From { get; private set; }
        public CraftSystem CraftSystem { get; private set; }
        public BaseTool Tool { get; private set; }

        private CraftPage m_Page;

        private const int LabelHue = 0x480;
        private const int LabelColor = 0x7FFF;
        private const int FontColor = 0xFFFFFF;

        private enum CraftPage
        {
            None,
            PickResource,
            PickResource2
        }

        /*public CraftGump( Mobile from, CraftSystem craftSystem, BaseTool tool ): this( from, craftSystem, -1, -1, tool, null )
        {
        }*/

        public CraftGump(Mobile from, CraftSystem craftSystem, BaseTool tool, object notice) : this(from, craftSystem,
            tool, notice, CraftPage.None)
        {
        }

        private CraftGump(Mobile from, CraftSystem craftSystem, BaseTool tool, object notice, CraftPage page) : base(40,
            40)
        {
            From = from;
            CraftSystem = craftSystem;
            Tool = tool;
            m_Page = page;

            CraftContext context = craftSystem.GetContext(from);

            from.CloseGump<CraftGump>();
            from.CloseGump<CraftGumpItem>();

            AddPage(0);

            AddBackground(0, 0, 530, 487, 5054);
            AddImageTiled(10, 10, 510, 22, 2624);
            AddImageTiled(10, 292, 150, 45, 2624);
            AddImageTiled(165, 292, 355, 45, 2624);
            AddImageTiled(10, 342, 150, 45, 2624);
            AddImageTiled(165, 342, 355, 45, 2624);
            AddImageTiled(10, 392, 510, 85, 2624);
            AddImageTiled(10, 37, 200, 250, 2624);
            AddImageTiled(215, 37, 305, 250, 2624);
            AddAlphaRegion(10, 10, 510, 467);

            if (craftSystem.GumpTitleNumber > 0)
                AddHtmlLocalized(10, 12, 510, 20, craftSystem.GumpTitleNumber, LabelColor, false, false);
            else
                AddHtml(10, 12, 510, 20, craftSystem.GumpTitleString, false, false);

            AddHtmlLocalized(10, 37, 200, 22, 1044010, LabelColor, false, false); // <CENTER>CATEGORIES</CENTER>
            AddHtmlLocalized(215, 37, 305, 22, 1044011, LabelColor, false, false); // <CENTER>SELECTIONS</CENTER>
            AddHtml(10, 302, 150, 25, "<BASEFONT COLOR=#FFFFFF><CENTER>MAKE AMOUNT</CENTER></BASEFONT>", false,
                false);
            AddHtmlLocalized(10, 352, 150, 25, 1044012, LabelColor, false, false); // <CENTER>NOTICES</CENTER>

            AddButton(15, 452, 4017, 4019, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(50, 455, 150, 18, 1011441, LabelColor, false, false); // EXIT

            AddButton(320, 452, 4005, 4007, GetButtonID(6, 2), GumpButtonType.Reply, 0);
            AddHtmlLocalized(355, 455, 150, 18, 1044013, LabelColor, false, false); // MAKE LAST

            // Mark option
            if (craftSystem.MarkOption)
            {
                AddButton(320, 412, 4005, 4007, GetButtonID(6, 6), GumpButtonType.Reply, 0);
                AddHtmlLocalized(355, 415, 150, 18, 1044017 + (context == null ? 0 : (int) context.MarkOption),
                    LabelColor, false, false); // MARK ITEM
            }
            // ****************************************

            // Resmelt option
            if (craftSystem.Resmelt)
            {
                AddButton(15, 392, 4005, 4007, GetButtonID(6, 1), GumpButtonType.Reply, 0);
                AddHtmlLocalized(50, 395, 150, 18, 1044259, LabelColor, false, false); // SMELT ITEM
            }
            // ****************************************

            // Repair option
            if (craftSystem.Repair)
            {
                AddButton(320, 392, 4005, 4007, GetButtonID(6, 5), GumpButtonType.Reply, 0);
                AddHtmlLocalized(355, 395, 150, 18, 1044260, LabelColor, false, false); // REPAIR ITEM
            }
            // ****************************************

            // Make amount
            AddImage(180, 302, 2443);
            AddTextEntry(190, 304, 30, 18, LabelHue, 0, context.CraftNumber.ToString());
            // ****************************************

            if (notice is int && (int) notice > 0)
                AddHtmlLocalized(170, 345, 350, 40, (int) notice, LabelColor, false, false);
            else if (notice is string)
                AddHtml(170, 345, 350, 40, $"<BASEFONT COLOR=#{FontColor:X6}>{notice}</BASEFONT>", false, false);

            // If the system has more than one resource
            if (craftSystem.CraftSubRes.Init)
            {
                string nameString = craftSystem.CraftSubRes.NameString;
                int nameNumber = craftSystem.CraftSubRes.NameNumber;

                int resIndex = context == null ? -1 : context.LastResourceIndex;

                Type resourceType = craftSystem.CraftSubRes.ResType;

                if (resIndex > -1)
                {
                    CraftSubRes subResource = craftSystem.CraftSubRes.GetAt(resIndex);

                    nameString = subResource.NameString;
                    nameNumber = subResource.NameNumber;
                    resourceType = subResource.ItemType;
                }

                int resourceCount = 0;

                if (from.Backpack != null)
                {
                    Item[] items = from.Backpack.FindItemsByType(resourceType, true);

                    for (int i = 0; i < items.Length; ++i)
                        resourceCount += items[i].Amount;
                }

                AddButton(15, 412, 4005, 4007, GetButtonID(6, 0), GumpButtonType.Reply, 0);

                if (nameNumber > 0)
                    AddHtmlLocalized(50, 415, 250, 18, nameNumber, resourceCount.ToString(), LabelColor, false, false);
                else
                    AddLabel(50, 412, LabelHue, $"{nameString} ({resourceCount} Available)");
            }
            // ****************************************

            // For dragon scales
            if (craftSystem.CraftSubRes2.Init)
            {
                string nameString = craftSystem.CraftSubRes2.NameString;
                int nameNumber = craftSystem.CraftSubRes2.NameNumber;

                int resIndex = context == null ? -1 : context.LastResourceIndex2;

                Type resourceType = craftSystem.CraftSubRes2.ResType;

                if (resIndex > -1)
                {
                    CraftSubRes subResource = craftSystem.CraftSubRes2.GetAt(resIndex);

                    nameString = subResource.NameString;
                    nameNumber = subResource.NameNumber;
                    resourceType = subResource.ItemType;
                }

                int resourceCount = 0;

                if (from.Backpack != null)
                {
                    Item[] items = from.Backpack.FindItemsByType(resourceType, true);

                    for (int i = 0; i < items.Length; ++i)
                        resourceCount += items[i].Amount;
                }

                AddButton(15, 432, 4005, 4007, GetButtonID(6, 7), GumpButtonType.Reply, 0);

                if (nameNumber > 0)
                    AddHtmlLocalized(50, 435, 250, 18, nameNumber, resourceCount.ToString(), LabelColor, false, false);
                else
                    AddLabel(50, 435, LabelHue, $"{nameString} ({resourceCount} Available)");
            }
            // ****************************************

            CreateGroupList();

            if (page == CraftPage.PickResource)
                CreateResList(false, from);
            else if (page == CraftPage.PickResource2)
                CreateResList(true, from);
            else if (context != null && context.LastGroupIndex > -1)
                CreateItemList(context.LastGroupIndex);
        }

        public void CreateResList(bool opt, Mobile from)
        {
            CraftSubResCol res = opt ? CraftSystem.CraftSubRes2 : CraftSystem.CraftSubRes;

            for (int i = 0; i < res.Count; ++i)
            {
                int index = i % 10;

                CraftSubRes subResource = res.GetAt(i);

                if (index == 0)
                {
                    if (i > 0)
                        AddButton(485, 260, 4005, 4007, 0, GumpButtonType.Page, i / 10 + 1);

                    AddPage(i / 10 + 1);

                    if (i > 0)
                        AddButton(455, 260, 4014, 4015, 0, GumpButtonType.Page, i / 10);

                    CraftContext context = CraftSystem.GetContext(From);

                    //AddButton( 220, 260, 4005, 4007, GetButtonID( 6, 4 ), GumpButtonType.Reply, 0 );
                    //AddHtmlLocalized( 255, 263, 200, 18, context == null || !context.DoNotColor ? 1061591 : 1061590, LabelColor, false, false );
                }

                int resourceCount = 0;

                if (from.Backpack != null)
                {
                    Item[] items = from.Backpack.FindItemsByType(subResource.ItemType, true);

                    for (int j = 0; j < items.Length; ++j)
                        resourceCount += items[j].Amount;
                }

                AddButton(220, 60 + index * 20, 4005, 4007, GetButtonID(5, i), GumpButtonType.Reply, 0);

                if (subResource.NameNumber > 0)
                    AddHtmlLocalized(255, 63 + index * 20, 250, 18, subResource.NameNumber, resourceCount.ToString(),
                        LabelColor, false, false);
                else
                    AddLabel(255, 60 + index * 20, LabelHue, $"{subResource.NameString} ({resourceCount})");
            }
        }

        public void CreateMakeLastList()
        {
            CraftContext context = CraftSystem.GetContext(From);

            if (context == null)
                return;

            List<CraftItem> items = context.Items;

            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; ++i)
                {
                    int index = i % 10;

                    CraftItem craftItem = items[i];

                    if (index == 0)
                    {
                        if (i > 0)
                        {
                            AddButton(370, 260, 4005, 4007, 0, GumpButtonType.Page, i / 10 + 1);
                            AddHtmlLocalized(405, 263, 100, 18, 1044045, LabelColor, false, false); // NEXT PAGE
                        }

                        AddPage(i / 10 + 1);

                        if (i > 0)
                        {
                            AddButton(220, 260, 4014, 4015, 0, GumpButtonType.Page, i / 10);
                            AddHtmlLocalized(255, 263, 100, 18, 1044044, LabelColor, false, false); // PREV PAGE
                        }
                    }

                    AddButton(220, 60 + index * 20, 4005, 4007, GetButtonID(3, i), GumpButtonType.Reply, 0);

                    if (craftItem.NameNumber > 0)
                        AddHtmlLocalized(255, 63 + index * 20, 220, 18, craftItem.NameNumber, LabelColor, false, false);
                    else
                        AddLabel(255, 60 + index * 20, LabelHue, craftItem.NameString);

                    AddButton(480, 60 + index * 20, 4011, 4012, GetButtonID(4, i), GumpButtonType.Reply, 0);
                }
            }
            else
            {
                // NOTE: This is not as OSI; it is an intentional difference

                AddHtmlLocalized(230, 62, 200, 22, 1044165, LabelColor, false, false); // You haven't made anything yet.
            }
        }

        public void CreateItemList(int selectedGroup)
        {
            if (selectedGroup == 501) // 501 : Last 10
            {
                CreateMakeLastList();
                return;
            }

            CraftGroupCol craftGroupCol = CraftSystem.CraftGroups;
            CraftGroup craftGroup = craftGroupCol.GetAt(selectedGroup);
            CraftItemCol craftItemCol = craftGroup.CraftItems;

            for (int i = 0; i < craftItemCol.Count; ++i)
            {
                int index = i % 10;

                CraftItem craftItem = craftItemCol.GetAt(i);

                if (index == 0)
                {
                    if (i > 0)
                    {
                        AddButton(370, 260, 4005, 4007, 0, GumpButtonType.Page, i / 10 + 1);
                        AddHtmlLocalized(405, 263, 100, 18, 1044045, LabelColor, false, false); // NEXT PAGE
                    }

                    AddPage(i / 10 + 1);

                    if (i > 0)
                    {
                        AddButton(220, 260, 4014, 4015, 0, GumpButtonType.Page, i / 10);
                        AddHtmlLocalized(255, 263, 100, 18, 1044044, LabelColor, false, false); // PREV PAGE
                    }
                }

                AddButton(220, 60 + index * 20, 4005, 4007, GetButtonID(1, i), GumpButtonType.Reply, 0);

                if (craftItem.NameNumber > 0)
                    AddHtmlLocalized(255, 63 + index * 20, 220, 18, craftItem.NameNumber, LabelColor, false, false);
                else
                    AddLabel(255, 60 + index * 20, LabelHue, craftItem.NameString);

                AddButton(480, 60 + index * 20, 4011, 4012, GetButtonID(2, i), GumpButtonType.Reply, 0);
            }
        }

        public int CreateGroupList()
        {
            CraftGroupCol craftGroupCol = CraftSystem.CraftGroups;

            AddButton(15, 60, 4005, 4007, GetButtonID(6, 3), GumpButtonType.Reply, 0);
            AddHtmlLocalized(50, 63, 150, 18, 1044014, LabelColor, false, false); // LAST TEN

            for (int i = 0; i < craftGroupCol.Count; i++)
            {
                CraftGroup craftGroup = craftGroupCol.GetAt(i);

                AddButton(15, 80 + i * 20, 4005, 4007, GetButtonID(0, i), GumpButtonType.Reply, 0);

                if (craftGroup.NameNumber > 0)
                    AddHtmlLocalized(50, 83 + i * 20, 150, 18, craftGroup.NameNumber, LabelColor, false, false);
                else
                    AddLabel(50, 80 + i * 20, LabelHue, craftGroup.NameString);
            }

            return craftGroupCol.Count;
        }

        public static int GetButtonID(int type, int index)
        {
            return 1 + type + index * 7;
        }

        public async void CraftItem(CraftItem item)
        {
            var num = CraftSystem.CanCraft(From, Tool, item.ItemType);

            if (num > 0)
            {
                From.SendGump(new CraftGump(From, CraftSystem, Tool, num));
            }
            else
            {
                Type type = null;

                CraftContext context = CraftSystem.GetContext(From);

                if (context != null)
                {
                    CraftSubResCol res = item.UseSubRes2 ? CraftSystem.CraftSubRes2 : CraftSystem.CraftSubRes;
                    int resIndex = item.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex;

                    if (resIndex >= 0 && resIndex < res.Count)
                        type = res.GetAt(resIndex).ItemType;
                }

                var success = false;
                var fromPrevLocation = From.Location;

                for (var i = context.CraftNumber; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        From.SendAsciiMessage(89, "You finished looping.");
                        return;
                    }

                    var badCraft = CraftSystem.CanCraft(From, Tool, item.ItemType);
                    if (badCraft > 0)
                    {
                        From.SendAsciiMessage(89, "Looping aborted.");
                        return;
                    }

                    if (From.Location != fromPrevLocation)
                    {
                        From.SendAsciiMessage(89, "You moved. Looping aborted.");
                        return;
                    }

                    From.CloseGump<CraftGump>();
                    From.CloseGump<CraftGumpItem>();

                    CraftSystem.CreateItem(From, item.ItemType, type, Tool, item, ref success);

                    if (!success)
                    {
                        return;
                    }

                    From.SendAsciiMessage(89, $"Looping [{i} more to go].");

                    await Timer.Pause(((int) (CraftSystem.Delay * CraftSystem.MaxCraftEffect) + 6) * 1000);
                }
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID <= 0)
                return; // Canceled

            int buttonID = info.ButtonID - 1;
            int type = buttonID % 7;
            int index = buttonID / 7;

            CraftSystem system = CraftSystem;
            CraftGroupCol groups = system.CraftGroups;
            CraftContext context = system.GetContext(From);

            var craftNumberEntry = info.GetTextEntry(0);
            Int32.TryParse(craftNumberEntry.Text.Trim(), out var craftNumber);
            context.CraftNumber = craftNumber;

            switch (type)
            {
                case 0: // Show group
                {
                    if (context == null)
                        break;

                    if (index >= 0 && index < groups.Count)
                    {
                        context.LastGroupIndex = index;
                        From.SendGump(new CraftGump(From, system, Tool, null));
                    }

                    break;
                }
                case 1: // Create item
                {
                    if (context == null)
                        break;

                    int groupIndex = context.LastGroupIndex;

                    if (groupIndex >= 0 && groupIndex < groups.Count)
                    {
                        CraftGroup group = groups.GetAt(groupIndex);

                        if (index >= 0 && index < group.CraftItems.Count)
                            CraftItem(group.CraftItems.GetAt(index));
                    }

                    break;
                }
                case 2: // Item details
                {
                    if (context == null)
                        break;

                    int groupIndex = context.LastGroupIndex;

                    if (groupIndex >= 0 && groupIndex < groups.Count)
                    {
                        CraftGroup group = groups.GetAt(groupIndex);

                        if (index >= 0 && index < group.CraftItems.Count)
                            From.SendGump(new CraftGumpItem(From, system, group.CraftItems.GetAt(index), Tool));
                    }

                    break;
                }
                case 3: // Create item (last 10)
                {
                    if (context == null)
                        break;

                    List<CraftItem> lastTen = context.Items;

                    if (index >= 0 && index < lastTen.Count)
                        CraftItem(lastTen[index]);

                    break;
                }
                case 4: // Item details (last 10)
                {
                    if (context == null)
                        break;

                    List<CraftItem> lastTen = context.Items;

                    if (index >= 0 && index < lastTen.Count)
                        From.SendGump(new CraftGumpItem(From, system, lastTen[index], Tool));

                    break;
                }
                case 5: // Resource selected
                {
                    if (m_Page == CraftPage.PickResource && index >= 0 && index < system.CraftSubRes.Count)
                    {
                        int groupIndex = context == null ? -1 : context.LastGroupIndex;

                        CraftSubRes res = system.CraftSubRes.GetAt(index);

                        if (From.Skills[system.MainSkill].Base < res.RequiredSkill)
                        {
                            From.SendGump(new CraftGump(From, system, Tool, res.Message));
                        }
                        else
                        {
                            if (context != null)
                                context.LastResourceIndex = index;

                            From.SendGump(new CraftGump(From, system, Tool, null));
                        }
                    }
                    else if (m_Page == CraftPage.PickResource2 && index >= 0 && index < system.CraftSubRes2.Count)
                    {
                        int groupIndex = context == null ? -1 : context.LastGroupIndex;

                        CraftSubRes res = system.CraftSubRes2.GetAt(index);

                        if (From.Skills[system.MainSkill].Base < res.RequiredSkill)
                        {
                            From.SendGump(new CraftGump(From, system, Tool, res.Message));
                        }
                        else
                        {
                            if (context != null)
                                context.LastResourceIndex2 = index;

                            From.SendGump(new CraftGump(From, system, Tool, null));
                        }
                    }

                    break;
                }
                case 6: // Misc. buttons
                {
                    switch (index)
                    {
                        case 0: // Resource selection
                        {
                            if (system.CraftSubRes.Init)
                                From.SendGump(new CraftGump(From, system, Tool, null, CraftPage.PickResource));

                            break;
                        }
                        case 1: // Smelt item
                        {
                            if (system.Resmelt)
                                Resmelt.Do(From, system, Tool);

                            break;
                        }
                        case 2: // Make last
                        {
                            if (context == null)
                                break;

                            CraftItem item = context.LastMade;

                            if (item != null)
                                CraftItem(item);
                            else
                                From.SendGump(new CraftGump(From, CraftSystem, Tool, 1044165,
                                    m_Page)); // You haven't made anything yet.

                            break;
                        }
                        case 3: // Last 10
                        {
                            if (context == null)
                                break;

                            context.LastGroupIndex = 501;
                            From.SendGump(new CraftGump(From, system, Tool, null));

                            break;
                        }
                        case 4: // Toggle use resource hue
                        {
                            if (context == null)
                                break;

                            context.DoNotColor = !context.DoNotColor;

                            From.SendGump(new CraftGump(From, CraftSystem, Tool, null, m_Page));

                            break;
                        }
                        case 5: // Repair item
                        {
                            if (system.Repair)
                                Repair.Do(From, system, Tool);

                            break;
                        }
                        case 6: // Toggle mark option
                        {
                            if (context == null || !system.MarkOption)
                                break;

                            switch (context.MarkOption)
                            {
                                case CraftMarkOption.MarkItem:
                                    context.MarkOption = CraftMarkOption.DoNotMark;
                                    break;
                                case CraftMarkOption.DoNotMark:
                                    context.MarkOption = CraftMarkOption.PromptForMark;
                                    break;
                                case CraftMarkOption.PromptForMark:
                                    context.MarkOption = CraftMarkOption.MarkItem;
                                    break;
                            }

                            From.SendGump(new CraftGump(From, CraftSystem, Tool, null, m_Page));

                            break;
                        }
                        case 7: // Resource selection 2
                        {
                            if (system.CraftSubRes2.Init)
                                From.SendGump(new CraftGump(From, system, Tool, null, CraftPage.PickResource2));

                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}