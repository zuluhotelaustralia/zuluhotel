using System;
using System.Linq;
using Server.Items;

namespace Server.Engines.Craft
{
    public class DefBowFletching : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Fletching; }
        }

        public override int GumpTitleNumber
        {
            get { return 1044006; } // <CENTER>BOWCRAFT AND FLETCHING MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefBowFletching();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0;
        }

        private DefBowFletching() : base(3, 3, 2)
        {
        }

        public override int GetCraftSkillRequired(int itemSkillRequired, Type craftResourceType)
        {
            var resource = CraftResources.GetFromType(craftResourceType);
            return itemSkillRequired + (int) (CraftResources.GetCraftSkillRequired(resource) / 4);
        }

        public override int GetCraftPoints(int itemSkillRequired, int materialAmount)
        {
            return (itemSkillRequired + materialAmount) * 8;
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x56);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality,
            bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
            int index = -1;

            // Materials
            AddCraft(typeof(Kindling), 1044457, 1023553, 0.0, 00.0, typeof(Log), 1044041, 1, 1044351);

            index = AddCraft(typeof(Shaft), 1044457, 1027124, 0.0, 40.0, typeof(Log), 1044041, 1, 1044351);
            SetUseAllRes(index, true);

            // Ammunition
            index = AddCraft(typeof(Arrow), 1044565, 1023903, 0.0, 40.0, typeof(Shaft), 1044560, 1, 1044561);
            AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            SetUseAllRes(index, true);

            index = AddCraft(typeof(Bolt), 1044565, 1027163, 0.0, 40.0, typeof(Shaft), 1044560, 1, 1044561);
            AddRes(index, typeof(Feather), 1044562, 1, 1044563);
            SetUseAllRes(index, true);

            // Weapons
            AddCraft(typeof(Bow), 1044566, 1025042, 30.0, 70.0, typeof(Log), 1044041, 7, 1044351);
            AddCraft(typeof(Crossbow), 1044566, 1023919, 60.0, 100.0, typeof(Log), 1044041, 7, 1044351);
            AddCraft(typeof(HeavyCrossbow), 1044566, 1025117, 80.0, 120.0, typeof(Log), 1044041, 10, 1044351);

            // Set the overridable material
            SetSubRes(typeof(Log), 1027136);

            ZhConfig.Resources.Logs.Entries.ToList()
                .ForEach(e => AddSubRes(e.ResourceType, e.Name.Length > 0 ? e.Name : "Log", e.CraftSkillRequired,
                    1044022, e.Name));

            MarkOption = true;
            Repair = true;
        }
    }
}