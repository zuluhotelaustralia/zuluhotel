using System;
using System.Linq;
using Server.Items;
using Scripts.Configuration;

namespace Server.Engines.Craft
{
    public class DefBlacksmithy : CraftSystem
    {
        public static CraftSystem CraftSystem => new DefBlacksmithy(ZhConfig.Crafting.Blacksmithy);

        private DefBlacksmithy(CraftSettings settings) : base(settings) // 3 3 2
        {
        }

        private static Type typeofAnvil = typeof(AnvilAttribute);
        private static Type typeofForge = typeof(ForgeAttribute);

        public static bool CheckAnvil(Mobile from, int range)
        {
            var anvil = false;

            Map map = from.Map;

            if (map == null)
                return false;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, range);

            foreach (Item item in eable)
            {
                Type type = item.GetType();

                bool isAnvil = type.IsDefined(typeofAnvil, false) || item.ItemID == 4015 || item.ItemID == 4016 ||
                               item.ItemID == 0x2DD5 || item.ItemID == 0x2DD6;

                if (isAnvil)
                {
                    if (@from.Z + 16 < item.Z || item.Z + 16 < from.Z || !from.InLOS(item))
                        continue;

                    anvil = anvil || isAnvil;

                    if (anvil)
                        break;
                }
            }

            eable.Free();

            for (int x = -range; !anvil && x <= range; ++x)
            {
                for (int y = -range; !anvil && y <= range; ++y)
                {
                    StaticTile[] tiles = map.Tiles.GetStaticTiles(from.X + x, from.Y + y, true);

                    for (int i = 0; !anvil && i < tiles.Length; ++i)
                    {
                        int id = tiles[i].ID;

                        bool isAnvil = id == 4015 || id == 4016 || id == 0x2DD5 || id == 0x2DD6;

                        if (isAnvil)
                        {
                            if (@from.Z + 16 < tiles[i].Z || tiles[i].Z + 16 < from.Z ||
                                !from.InLOS(new Point3D(from.X + x, from.Y + y, tiles[i].Z + tiles[i].Height / 2 + 1)))
                                continue;

                            anvil = anvil || isAnvil;
                        }
                    }
                }
            }

            return anvil;
        }

        public static bool CheckForge(Mobile from, int range)
        {
            var forge = false;

            Map map = from.Map;

            if (map == null)
                return false;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, range);

            foreach (Item item in eable)
            {
                Type type = item.GetType();

                bool isForge = type.IsDefined(typeofForge, false) || item.ItemID == 4017 ||
                               item.ItemID >= 6522 && item.ItemID <= 6569 || item.ItemID == 0x2DD8;

                if (isForge)
                {
                    if (@from.Z + 16 < item.Z || item.Z + 16 < from.Z || !from.InLOS(item))
                        continue;

                    forge = forge || isForge;

                    if (forge)
                        break;
                }
            }

            eable.Free();

            for (int x = -range; !forge && x <= range; ++x)
            {
                for (int y = -range; !forge && y <= range; ++y)
                {
                    StaticTile[] tiles = map.Tiles.GetStaticTiles(from.X + x, from.Y + y, true);

                    for (int i = 0; !forge && i < tiles.Length; ++i)
                    {
                        int id = tiles[i].ID;

                        bool isForge = id == 4017 || id >= 6522 && id <= 6569 || id == 0x2DD8;

                        if (isForge)
                        {
                            if (@from.Z + 16 < tiles[i].Z || tiles[i].Z + 16 < from.Z ||
                                !from.InLOS(new Point3D(from.X + x, from.Y + y, tiles[i].Z + tiles[i].Height / 2 + 1)))
                                continue;

                            forge = forge || isForge;
                        }
                    }
                }
            }

            return forge;
        }

        public override int GetCraftSkillRequired(int itemSkillRequired, Type craftResourceType)
        {
            var resource = CraftResources.GetFromType(craftResourceType);
            return itemSkillRequired + (int) (CraftResources.GetCraftSkillRequired(resource) / 3);
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

            if (CheckAnvil(from, 1))
                return 0;

            return 1044266; // You must be near an anvil
        }

        // Delay to synchronize the sound with the hit on the anvil
        private class InternalTimer : Timer
        {
            private Mobile m_From;

            public InternalTimer(Mobile from) : base(TimeSpan.FromSeconds(0.7))
            {
                m_From = from;
            }

            protected override void OnTick()
            {
                m_From.PlaySound(0x2A);
            }
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
            SetSubRes(typeof(IronIngot), 1044022);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            ZhConfig.Resources.Ores.Entries.ToList()
                .ForEach(e => AddSubRes(e.SmeltType, e.Name, e.CraftSkillRequired, 1044022, e.Name));

            Resmelt = true;
            Repair = true;
            MarkOption = true;
            CanEnhance = false;
        }
    }

    public class ForgeAttribute : Attribute
    {
        public ForgeAttribute()
        {
        }
    }

    public class AnvilAttribute : Attribute
    {
        public AnvilAttribute()
        {
        }
    }
}