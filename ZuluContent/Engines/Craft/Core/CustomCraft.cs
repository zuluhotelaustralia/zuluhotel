using System;
using Server.Items;

namespace Server.Engines.Craft
{
    public abstract class CustomCraft
    {
        public Mobile From { get; set; }

        public CraftItem CraftItem { get; set; }

        public CraftSystem CraftSystem { get; set; }

        public Type TypeRes { get; set; }

        public BaseTool Tool { get; set; }

        public int Mark { get; set; }

        public double Quality { get; set; }

        public CustomCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool,
            int mark, double quality)
        {
            From = from;
            CraftItem = craftItem;
            CraftSystem = craftSystem;
            TypeRes = typeRes;
            Tool = tool;
            Mark = mark;
            Quality = quality;
        }

        public abstract void EndCraftAction();
        public abstract Item CompleteCraft(out int message);
    }
}