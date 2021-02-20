using System;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
    public class CraftGumpItem : Gump, ICraftGump
    {
        public Mobile From { get; }
        public CraftSystem CraftSystem { get; }
        public CraftItem CraftItem { get; }
        public BaseTool Tool { get; }

        private const int LabelHue = 0x480; // 0x384
        private const int RedLabelHue = 0x20;

        private const int LabelColor = 0x7FFF;
        private const int RedLabelColor = 0x6400;

        private const int GreyLabelColor = 0x3DEF;

        private int m_OtherCount;

        public CraftGumpItem(Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool) : base(40, 40)
        {
            From = from;
            CraftSystem = craftSystem;
            CraftItem = craftItem;
            Tool = tool;

            from.CloseGump<CraftGump>();
            ;
            from.CloseGump<CraftGumpItem>();
            ;

            AddPage(0);
            AddBackground(0, 0, 530, 417, 5054);
            AddImageTiled(10, 10, 510, 22, 2624);
            AddImageTiled(10, 37, 150, 148, 2624);
            AddImageTiled(165, 37, 355, 90, 2624);
            AddImageTiled(10, 190, 155, 22, 2624);
            AddImageTiled(10, 217, 150, 53, 2624);
            AddImageTiled(165, 132, 355, 80, 2624);
            AddImageTiled(10, 275, 155, 22, 2624);
            AddImageTiled(10, 302, 150, 53, 2624);
            AddImageTiled(165, 217, 355, 80, 2624);
            AddImageTiled(10, 360, 155, 22, 2624);
            AddImageTiled(165, 302, 355, 80, 2624);
            AddImageTiled(10, 387, 510, 22, 2624);
            AddAlphaRegion(10, 10, 510, 399);

            AddHtmlLocalized(170, 40, 150, 20, 1044053, LabelColor, false, false); // ITEM
            AddHtmlLocalized(10, 192, 150, 22, 1044054, LabelColor, false, false); // <CENTER>SKILLS</CENTER>
            AddHtmlLocalized(10, 277, 150, 22, 1044055, LabelColor, false, false); // <CENTER>MATERIALS</CENTER>
            AddHtmlLocalized(10, 362, 150, 22, 1044056, LabelColor, false, false); // <CENTER>OTHER</CENTER>

            if (craftSystem.GumpTitleNumber > 0)
                AddHtmlLocalized(10, 12, 510, 20, craftSystem.GumpTitleNumber, LabelColor, false, false);
            else
                AddHtml(10, 12, 510, 20, craftSystem.GumpTitleString, false, false);

            AddButton(15, 387, 4014, 4016, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(50, 390, 150, 18, 1044150, LabelColor, false, false); // BACK

            AddButton(270, 387, 4005, 4007, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(305, 390, 150, 18, 1044151, LabelColor, false, false); // MAKE NOW

            if (craftItem.NameNumber > 0)
                AddHtmlLocalized(330, 40, 180, 18, craftItem.NameNumber, LabelColor, false, false);
            else
                AddLabel(330, 40, LabelHue, craftItem.NameString);

            if (craftItem.UseAllRes)
                AddHtmlLocalized(170, 302 + m_OtherCount++ * 20, 310, 18, 1048176, LabelColor, false,
                    false); // Makes as many as possible at once

            DrawItem();
            DrawSkill();
            DrawResource();
        }

        private bool m_ShowExceptionalChance;

        public void DrawItem()
        {
            Type type = CraftItem.ItemType;

            AddItem(20, 50, CraftItem.ItemIDOf(type), CraftItem.ItemHue);

            if (CraftItem.IsMarkable(type))
            {
                AddHtmlLocalized(170, 302 + m_OtherCount++ * 20, 310, 18, 1044059, LabelColor, false,
                    false); // This item may hold its maker's mark
                m_ShowExceptionalChance = true;
            }
        }

        public void DrawSkill()
        {
            for (int i = 0; i < CraftItem.Skills.Count; i++)
            {
                var skill = CraftItem.Skills[i];
                double minSkill = skill.MinSkill, maxSkill = skill.MaxSkill;

                if (minSkill < 0)
                    minSkill = 0;

                AddHtmlLocalized(170, 132 + i * 20, 200, 18, 1044060 + (int) skill.SkillToMake, LabelColor, false,
                    false);
                AddLabel(430, 132 + i * 20, LabelHue, $"{minSkill:F1}");
            }

            var res = CraftItem.UseSubRes2 ? CraftSystem.CraftSubRes2 : CraftSystem.CraftSubRes;
            var resIndex = -1;

            var context = CraftSystem.GetContext(From);

            if (context != null)
                resIndex = CraftItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex;

            var allRequiredSkills = true;
            var chance = CraftItem.GetSuccessChance(From, resIndex > -1 ? res.GetAt(resIndex).ItemType : null,
                CraftSystem, false, ref allRequiredSkills);
            var craftSkillRequired = CraftItem.GetCraftSkillRequired(From,
                resIndex > -1 ? res.GetAt(resIndex).ItemType : null, CraftSystem);
            var excepChance = CraftItem.GetExceptionalChance(From, CraftSystem, ref craftSkillRequired);

            AddHtmlLocalized(170, 80, 250, 18, 1044057, LabelColor, false, false); // Success Chance:
            AddLabel(430, 80, LabelHue, $"{chance * 100:F1}%");

            if (m_ShowExceptionalChance)
            {
                AddHtmlLocalized(170, 100, 250, 18, 1044058, 32767, false, false); // Exceptional Chance:
                AddLabel(430, 100, LabelHue, $"{excepChance:F1}%");
            }
        }

        private static Type typeofBlankScroll = typeof(BlankScroll);
        private static Type typeofSpellScroll = typeof(SpellScroll);

        public void DrawResource()
        {
            bool retainedColor = false;

            CraftContext context = CraftSystem.GetContext(From);

            CraftSubResCol res = CraftItem.UseSubRes2 ? CraftSystem.CraftSubRes2 : CraftSystem.CraftSubRes;
            int resIndex = -1;

            if (context != null)
                resIndex = CraftItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex;

            bool cropScroll = CraftItem.Resources.Count > 1
                              && CraftItem.Resources[CraftItem.Resources.Count - 1].ItemType == typeofBlankScroll
                              && typeofSpellScroll.IsAssignableFrom(CraftItem.ItemType);

            for (int i = 0; i < CraftItem.Resources.Count - (cropScroll ? 1 : 0) && i < 4; i++)
            {
                Type type;
                string nameString;
                int nameNumber;

                CraftRes craftResource = CraftItem.Resources[i];

                type = craftResource.ItemType;
                nameString = craftResource.NameString;
                nameNumber = craftResource.NameNumber;

                // Resource Mutation
                if (type == res.ResType && resIndex > -1)
                {
                    CraftSubRes subResource = res.GetAt(resIndex);

                    type = subResource.ItemType;

                    nameString = subResource.NameString;
                    nameNumber = subResource.GenericNameNumber;

                    if (nameNumber <= 0)
                        nameNumber = subResource.NameNumber;
                }
                // ******************

                if (!retainedColor && CraftItem.RetainsColorFrom(CraftSystem, type))
                {
                    retainedColor = true;
                    AddHtmlLocalized(170, 302 + m_OtherCount++ * 20, 310, 18, 1044152, LabelColor, false,
                        false); // * The item retains the color of this material
                    AddLabel(500, 219 + i * 20, LabelHue, "*");
                }

                if (nameNumber > 0)
                    AddHtmlLocalized(170, 219 + i * 20, 310, 18, nameNumber, LabelColor, false, false);
                else
                    AddLabel(170, 219 + i * 20, LabelHue, nameString);

                AddLabel(430, 219 + i * 20, LabelHue, craftResource.Amount.ToString());
            }

            if (CraftItem.NameNumber == 1041267) // runebook
            {
                AddHtmlLocalized(170, 219 + CraftItem.Resources.Count * 20, 310, 18, 1044447, LabelColor, false, false);
                AddLabel(430, 219 + CraftItem.Resources.Count * 20, LabelHue, "1");
            }

            if (cropScroll)
                AddHtmlLocalized(170, 302 + m_OtherCount++ * 20, 360, 18, 1044379, LabelColor, false,
                    false); // Inscribing scrolls also requires a blank scroll and mana.
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            // Back Button
            if (info.ButtonID == 0)
            {
                CraftGump craftGump = new CraftGump(From, CraftSystem, Tool, null);
                From.SendGump(craftGump);
            }
            else // Make Button
            {
                int num = CraftSystem.CanCraft(From, Tool, CraftItem.ItemType);

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
                        CraftSubResCol res = CraftItem.UseSubRes2 ? CraftSystem.CraftSubRes2 : CraftSystem.CraftSubRes;
                        int resIndex = CraftItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex;

                        if (resIndex > -1)
                            type = res.GetAt(resIndex).ItemType;
                    }

                    CraftSystem.CreateItem(From, CraftItem.ItemType, type, Tool, CraftItem);
                }
            }
        }
    }
}