using System;
using System.Linq;
using Server.Items;
using Scripts.Configuration;

namespace Server.Engines.Craft
{
    public sealed class DefAlchemy : CraftSystem
    {
        public static CraftSystem NormalCraftSystem { get; } = new DefAlchemy(ZhConfig.Crafting.Alchemy);
        public static CraftSystem PlusCraftSystem { get; } = new DefAlchemy(ZhConfig.Crafting.AlchemyPlus);
        
        private static Type TypeOfPotion => typeof(BasePotion);

        private DefAlchemy(CraftSettings settings) : base(settings)
        {
        }

        public override int GetCraftPoints(int itemSkillRequired, int materialAmount)
        {
            return itemSkillRequired * 15;
        }
        
        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public static bool IsPotion(Type type)
        {
            return TypeOfPotion.IsAssignableFrom(type);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality,
            bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (IsPotion(item.ItemType))
                {
                    RecycleBottles(from, item);
                    return 500287; // You fail to create a useful potion.
                }
                else
                {
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                }
            }
            else
            {
                from.PlaySound(CraftEndSound); // Sound of a filling bottle

                if (IsPotion(item.ItemType))
                {
                    return quality == -1 ? 1048136 : 500279;
                }
                else
                {
                    return 1044154; // You create the item.
                }
            }
        }

        public static void RecycleBottles(Mobile from, CraftItem craftItem)
        {
            var usedBottles = craftItem.Resources
                .Where(r => r.ItemType == typeof(Bottle) || r.ItemType.IsSubclassOf(typeof(BasePotion)))
                .Sum(r => r.Amount);
                        
            if (usedBottles > 0) 
                from.AddToBackpack(new Bottle(usedBottles));
        }
    }
}