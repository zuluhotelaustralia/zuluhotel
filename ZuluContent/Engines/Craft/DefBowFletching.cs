using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Server.Items;
using Server.Json;
using Scripts.Configuration;

namespace Server.Engines.Craft
{
    public class DefBowFletching : CraftSystem
    {
        public static CraftSystem CraftSystem => new DefBowFletching(ZhConfig.Crafting.Fletching);

        private DefBowFletching(CraftSettings settings) : base(settings)
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
            base.InitCraftList();

            // Set the overridable material
            SetSubRes(typeof(Log), 1027136);

            ZhConfig.Resources.Logs.Entries.ToList()
                .ForEach(e => AddSubRes(e.ResourceType, e.Name.Length > 0 ? e.Name : "Log", e.CraftSkillRequired,
                    1044022, 1072652));

            MarkOption = true;
            Repair = true;
        }
    }
}