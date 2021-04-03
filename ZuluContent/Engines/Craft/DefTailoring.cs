using System;
using System.Linq;
using Server.Items;
using static Server.Configurations.ResourceConfiguration;

namespace Server.Engines.Craft
{
    public class DefTailoring : CraftSystem
    {
        public override SkillName MainSkill => SkillName.Tailoring;

        public override int GumpTitleNumber => 1044005; // <CENTER>TAILORING MENU</CENTER>

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefTailoring();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.5; // 50%
        }

        private DefTailoring() : base(2, 2, 1)
        {
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

            return 0;
        }

        public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (type != typeof(Cloth) && type != typeof(UncutCloth))
                return false;

            return true;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x248);
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

            #region Hats

            AddCraft(typeof(SkullCap), 1011375, 1025444, 2.0, 2.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft(typeof(Bandana), 1011375, 1025440, 1.0, 1.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft(typeof(FloppyHat), 1011375, 1025907, 16.0, 16.0, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(Cap), 1011375, 1025909, 16.0, 16.0, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(WideBrimHat), 1011375, 1025908, 16.0, 16.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(StrawHat), 1011375, 1025911, 16.0, 16.0, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(TallStrawHat), 1011375, 1025910, 16.0, 16.0, typeof(Cloth), 1044286, 13, 1044287);
            AddCraft(typeof(WizardsHat), 1011375, 1025912, 17.0, 17.0, typeof(Cloth), 1044286, 15, 1044287);
            AddCraft(typeof(Bonnet), 1011375, 1025913, 16.0, 16.0, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(FeatheredHat), 1011375, 1025914, 16.0, 16.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(TricorneHat), 1011375, 1025915, 16.0, 16.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(JesterHat), 1011375, 1025916, 17.0, 17.0, typeof(Cloth), 1044286, 15, 1044287);

            #endregion

            #region Shirts

            AddCraft(typeof(Doublet), 1015269, 1028059, 12.0, 12.0, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Shirt), 1015269, 1025399, 30.0, 30.0, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(FancyShirt), 1015269, 1027933, 35.0, 35.0, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Tunic), 1015269, 1028097, 8.0, 8.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(Surcoat), 1015269, 1028189, 5.0, 5.0, typeof(Cloth), 1044286, 14, 1044287);
            AddCraft(typeof(PlainDress), 1015269, 1027937, 22.0, 22.0, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(FancyDress), 1015269, 1027935, 43.0, 43.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(Cloak), 1015269, 1025397, 51.0, 51.0, typeof(Cloth), 1044286, 14, 1044287);
            AddCraft(typeof(Robe), 1015269, 1027939, 64.0, 64.0, typeof(Cloth), 1044286, 16, 1044287);
            AddCraft(typeof(JesterSuit), 1015269, 1028095, 18.0, 18.0, typeof(Cloth), 1044286, 14, 1044287);

            #endregion

            #region Pants

            AddCraft(typeof(ShortPants), 1015279, 1025422, 20.0, 20.0, typeof(Cloth), 1044286, 6, 1044287);
            AddCraft(typeof(LongPants), 1015279, 1025433, 35.0, 35.0, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Kilt), 1015279, 1025431, 30.0, 30.0, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Skirt), 1015279, 1025398, 39.0, 39.0, typeof(Cloth), 1044286, 10, 1044287);

            #endregion

            #region Misc

            AddCraft(typeof(Bandage), 1015283, 1023617, 5.0, 5.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft(typeof(HalfApron), 1015283, 1025435, 30.0, 30.0, typeof(Cloth), 1044286, 6, 1044287);
            AddCraft(typeof(FullApron), 1015283, 1025437, 39.0, 39.0, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(BodySash), 1015283, 1025441, 12.0, 12.0, typeof(Cloth), 1044286, 4, 1044287);
            AddCraft(typeof(OrcMask), 1015283, 1025147, 75.0, 75.0, typeof(Hide), 1044462, 5, 1044463);
            AddCraft(typeof(BearMask), 1015283, 1025445, 85.0, 85.0, typeof(Hide), 1044462, 10, 1044463);
            AddCraft(typeof(DeerMask), 1015283, 1025447, 80.0, 80.0, typeof(Hide), 1044462, 10, 1044463);
            AddCraft(typeof(TribalMask), 1015283, 1025449, 50.0, 50.0, typeof(Hide), 1044462, 5, 1044463);
            AddCraft(typeof(VoodooMask), 1015283, 1025449, 90.0, 90.0, typeof(Hide), 1044462, 8, 1044463);

            #endregion

            #region Leather Armor

            AddCraft(typeof(LeatherGorget), 1015293, 1025063, 64.0, 64.0, typeof(Hide), 1044462, 4, 1044463);
            AddCraft(typeof(LeatherCap), 1015293, 1027609, 17.0, 17.0, typeof(Hide), 1044462, 2, 1044463);
            AddCraft(typeof(LeatherGloves), 1015293, 1025062, 62.0, 62.0, typeof(Hide), 1044462, 3, 1044463);
            AddCraft(typeof(LeatherArms), 1015293, 1025061, 64.0, 64.0, typeof(Hide), 1044462, 4, 1044463);
            AddCraft(typeof(LeatherLegs), 1015293, 1025067, 76.0, 76.0, typeof(Hide), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherChest), 1015293, 1025068, 80.0, 80.0, typeof(Hide), 1044462, 12, 1044463);

            #endregion

            #region Studded Armor

            AddCraft(typeof(StuddedGorget), 1015300, 1025078, 89.0, 89.0, typeof(Hide), 1044462, 6, 1044463);
            AddCraft(typeof(StuddedGloves), 1015300, 1025077, 93.0, 93.0, typeof(Hide), 1044462, 8, 1044463);
            AddCraft(typeof(StuddedArms), 1015300, 1025076, 97.0, 97.0, typeof(Hide), 1044462, 10, 1044463);
            AddCraft(typeof(StuddedLegs), 1015300, 1025082, 101.0, 101.0, typeof(Hide), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedChest), 1015300, 1025083, 105.0, 105.0, typeof(Hide), 1044462, 14, 1044463);

            #endregion

            #region Female Armor

            AddCraft(typeof(LeatherShorts), 1015306, 1027168, 72.0, 72.0, typeof(Hide), 1044462, 8, 1044463);
            AddCraft(typeof(LeatherSkirt), 1015306, 1027176, 68.0, 68.0, typeof(Hide), 1044462, 6, 1044463);
            AddCraft(typeof(LeatherBustierArms), 1015306, 1027178, 68.0, 68.0, typeof(Hide), 1044462, 6, 1044463);
            AddCraft(typeof(StuddedBustierArms), 1015306, 1027180, 93.0, 93.0, typeof(Hide), 1044462, 8, 1044463);
            AddCraft(typeof(FemaleLeatherChest), 1015306, 1027174, 72.0, 72.0, typeof(Hide), 1044462, 8, 1044463);
            AddCraft(typeof(FemaleStuddedChest), 1015306, 1027170, 97.0, 97.0, typeof(Hide), 1044462, 10, 1044463);

            #endregion

            #region Footwear

            AddCraft(typeof(Sandals), 1015288, 1025901, 22.0, 22.0, typeof(Hide), 1044462, 4, 1044463);
            AddCraft(typeof(Shoes), 1015288, 1025904, 26.0, 26.0, typeof(Hide), 1044462, 6, 1044463);
            AddCraft(typeof(Boots), 1015288, 1025899, 43.0, 43.0, typeof(Hide), 1044462, 8, 1044463);
            AddCraft(typeof(ThighBoots), 1015288, 1025906, 51.0, 51.0, typeof(Hide), 1044462, 10, 1044463);

            #endregion

            #region Containers

            AddCraft(typeof(Backpack), 1015091, 1022482, 40.0, 40.0, typeof(Hide), 1044462, 4, 1044463);
            AddCraft(typeof(Bag), 1015091, 1023702, 8.0, 8.0, typeof(Hide), 1044462, 2, 1044463);
            AddCraft(typeof(Pouch), 1015091, 1023705, 15.0, 15.0, typeof(Hide), 1044462, 4, 1044463);
            AddCraft(typeof(Bridle), 1015091, 1024980, 100.0, 100.0, typeof(Hide), 1044462, 15, 1044463);

            #endregion

            // Set the overridable material
            SetSubRes(typeof(Hide), 1049150);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            ZhConfig.Resources.Hides.Entries.ToList()
                .ForEach(e => AddSubRes(e.ResourceType, e.Name, e.CraftSkillRequired, 1044462, e.Name));

            MarkOption = true;
            Repair = true;
            Fortify = true;
            CanEnhance = false;
        }
    }
}