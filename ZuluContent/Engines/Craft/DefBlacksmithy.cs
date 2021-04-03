using System;
using System.Linq;
using Server.Items;
using static Server.Configurations.ResourceConfiguration;

namespace Server.Engines.Craft
{
    public class DefBlacksmithy : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Blacksmith; }
        }

        public override int GumpTitleNumber
        {
            get { return 1044002; } // <CENTER>BLACKSMITHY MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefBlacksmithy();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefBlacksmithy() : base(3, 3, 2)
        {
            /*
            
            base( MinCraftEffect, MaxCraftEffect, Delay )
            
            MinCraftEffect	: The minimum number of time the mobile will play the craft effect
            MaxCraftEffect	: The maximum number of time the mobile will play the craft effect
            Delay			: The delay between each craft effect
            
            Example: (3, 6, 1.7) would make the mobile do the PlayCraftEffect override
            function between 3 and 6 time, with a 1.7 second delay each time.
            
            */
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

        public override void PlayCraftEffect(Mobile from)
        {
            // no animation, instant sound
            //if ( from.Body.Type == BodyType.Human && !from.Mounted )
            //	from.Animate( 9, 5, 1, true, false, 0 );
            //new InternalTimer( from ).Start();

            from.PlaySound(0x2A);
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
            /*
            Synthax for a SIMPLE craft item
            AddCraft( ObjectType, Group, MinSkill, MaxSkill, ResourceType, Amount, Message )
            
            ObjectType		: The type of the object you want to add to the build list.
            Group			: The group in wich the object will be showed in the craft menu.
            MinSkill		: The minimum of skill value
            MaxSkill		: The maximum of skill value
            ResourceType	: The type of the resource the mobile need to create the item
            Amount			: The amount of the ResourceType it need to create the item
            Message			: String or Int for Localized.  The message that will be sent to the mobile, if the specified resource is missing.
            
            Synthax for a COMPLEXE craft item.  A complexe item is an item that need either more than
            only one skill, or more than only one resource.
            
            Coming soon....
            */

            #region Ringmail

            AddCraft(typeof(RingmailGloves), 1011076, 1025099, 22.0, 22.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(RingmailLegs), 1011076, 1025104, 29.0, 29.0, typeof(IronIngot), 1044036, 16, 1044037);
            AddCraft(typeof(RingmailArms), 1011076, 1025103, 27.0, 27.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(RingmailChest), 1011076, 1025100, 32.0, 32.0, typeof(IronIngot), 1044036, 18, 1044037);

            #endregion

            #region Chainmail

            AddCraft(typeof(ChainCoif), 1011077, 1025051, 24.0, 24.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(ChainLegs), 1011077, 1025054, 46.0, 46.0, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(ChainChest), 1011077, 1025055, 49.0, 49.0, typeof(IronIngot), 1044036, 20, 1044037);

            #endregion

            #region Platemail

            AddCraft(typeof(PlateArms), 1011078, 1025136, 76.0, 76.0, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(PlateGloves), 1011078, 1025140, 69.0, 69.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(PlateGorget), 1011078, 1025139, 66.0, 66.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(PlateLegs), 1011078, 1025137, 79.0, 79.0, typeof(IronIngot), 1044036, 20, 1044037);
            AddCraft(typeof(PlateChest), 1011078, 1046431, 85.0, 85.0, typeof(IronIngot), 1044036, 25, 1044037);
            AddCraft(typeof(FemalePlateChest), 1011078, 1046430, 54.0, 54.0, typeof(IronIngot), 1044036, 20, 1044037);

            #endregion

            #region Helmets

            AddCraft(typeof(Bascinet), 1011079, 1025132, 18.0, 18.0, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(CloseHelm), 1011079, 1025128, 48.0, 48.0, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(Helmet), 1011079, 1025130, 48.0, 48.0, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(NorseHelm), 1011079, 1025134, 48.0, 48.0, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(PlateHelm), 1011079, 1025138, 72.0, 72.0, typeof(IronIngot), 1044036, 15, 1044037);

            #endregion

            #region Shields

            AddCraft(typeof(Buckler), 1011080, 1027027, 5.0, 5.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(BronzeShield), 1011080, 1027026, 10.0, 10.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(HeaterShield), 1011080, 1027030, 34.0, 34.0, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(MetalShield), 1011080, 1027035, 15.0, 15.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(MetalKiteShield), 1011080, 1027028, 25.0, 25.0, typeof(IronIngot), 1044036, 16, 1044037);
            AddCraft(typeof(WoodenKiteShield), 1011080, 1027032, 25.0, 25.0, typeof(IronIngot), 1044036, 8, 1044037);

            #endregion

            #region Bladed

            AddCraft(typeof(Broadsword), 1011081, 1023934, 45.0, 45.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(Cutlass), 1011081, 1025185, 34.0, 34.0, typeof(IronIngot), 1044036, 8, 1044037);
            AddCraft(typeof(Dagger), 1011081, 1023921, 1.0, 1.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(Katana), 1011081, 1025119, 54.0, 54.0, typeof(IronIngot), 1044036, 8, 1044037);
            AddCraft(typeof(Kryss), 1011081, 1025121, 46.0, 46.0, typeof(IronIngot), 1044036, 8, 1044037);
            AddCraft(typeof(Longsword), 1011081, 1023937, 38.0, 38.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(Scimitar), 1011081, 1025046, 41.0, 41.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(VikingSword), 1011081, 1025049, 34.0, 34.0, typeof(IronIngot), 1044036, 14, 1044037);

            #endregion

            #region Axes

            AddCraft(typeof(Axe), 1011082, 1023913, 44.0, 44.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BattleAxe), 1011082, 1023911, 40.0, 40.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(DoubleAxe), 1011082, 1023915, 39.0, 39.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(ExecutionersAxe), 1011082, 1023909, 44.0, 44.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxe), 1011082, 1025115, 38.0, 38.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(TwoHandedAxe), 1011082, 1025187, 43.0, 43.0, typeof(IronIngot), 1044036, 16, 1044037);
            AddCraft(typeof(WarAxe), 1011082, 1025040, 49.0, 49.0, typeof(IronIngot), 1044036, 16, 1044037);

            #endregion

            #region Pole Arms

            AddCraft(typeof(Bardiche), 1011083, 1023917, 55.0, 55.0, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(Halberd), 1011083, 1025183, 59.0, 59.0, typeof(IronIngot), 1044036, 20, 1044037);
            AddCraft(typeof(ShortSpear), 1011083, 1025123, 45.0, 45.0, typeof(IronIngot), 1044036, 6, 1044037);
            AddCraft(typeof(Spear), 1011083, 1023938, 49.0, 49.0, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(WarFork), 1011083, 1025125, 48.0, 48.0, typeof(IronIngot), 1044036, 12, 1044037);

            #endregion

            #region Bashing

            AddCraft(typeof(HammerPick), 1011084, 1025181, 35.0, 35.0, typeof(IronIngot), 1044036, 16, 1044037);
            AddCraft(typeof(Mace), 1011084, 1023932, 24.0, 24.0, typeof(IronIngot), 1044036, 6, 1044037);
            AddCraft(typeof(Maul), 1011084, 1025179, 29.0, 29.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(WarMace), 1011084, 1025127, 38.0, 38.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarHammer), 1011084, 1025177, 44.0, 44.0, typeof(IronIngot), 1044036, 16, 1044037);

            #endregion

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