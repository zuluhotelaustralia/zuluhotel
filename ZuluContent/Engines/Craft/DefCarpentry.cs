using System;
using System.Linq;
using Server.Items;
using static Server.Configurations.ResourceConfiguration;

namespace Server.Engines.Craft
{
    public class DefCarpentry : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Carpentry; }
        }

        public override int GumpTitleNumber
        {
            get { return 1044004; } // <CENTER>CARPENTRY MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefCarpentry();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0;
        }

        private DefCarpentry() : base(3, 3, 2)
        {
        }

        public override int GetCraftSkillRequired(int itemSkillRequired, Type craftResourceType)
        {
            var resource = CraftResources.GetFromType(craftResourceType);
            return itemSkillRequired + (int) (CraftResources.GetCraftSkillRequired(resource) / 4);
        }

        public override int GetCraftPoints(int itemSkillRequired, int materialAmount)
        {
            return (itemSkillRequired + materialAmount) * 10;
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
            from.PlaySound(0x23D);
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
            var index = -1;

            // Chairs
            AddCraft(typeof(FootStool), 1015076, 1022910, 28.0, 28.0, typeof(Log), 1044041, 10, 1044351);
            AddCraft(typeof(Stool), 1015076, 1022602, 68.0, 68.0, typeof(Log), 1044041, 21, 1044351);
            AddCraft(typeof(WoodenThrone), 1015076, 1044304, 15.0, 15.0, typeof(Log), 1044041, 12, 1044351);
            AddCraft(typeof(Throne), 1015076, 1044305, 84.0, 84.0, typeof(Log), 1044041, 19, 1044351);
            AddCraft(typeof(FancyWoodenChairCushion), 1015076, 1044302, 52.0, 52.0, typeof(Log), 1044041, 15, 1044351);
            AddCraft(typeof(WoodenChairCushion), 1015076, 1044303, 52.0, 52.0, typeof(Log), 1044041, 15, 1044351);
            AddCraft(typeof(WoodenChair), 1015076, 1044301, 31.0, 31.0, typeof(Log), 1044041, 13, 1044351);
            AddCraft(typeof(BambooChair), 1015076, 1044300, 31.0, 31.0, typeof(Log), 1044041, 13, 1044351);
            AddCraft(typeof(WoodenBench), 1015076, 1022860, 10.0, 10.0, typeof(Log), 1044041, 10, 1044351);
            AddCraft(typeof(WoodenBench), 1015076, 1024169, 10.0, 10.0, typeof(Log), 1044041, 10,
                1044351); // loom bench
            AddCraft(typeof(WoodenBench), 1015076, 1022860, 63.0, 63.0, typeof(Log), 1044041, 17, 1044351); // bench 1
            AddCraft(typeof(WoodenBench), 1015076, 1022860, 83.0, 83.0, typeof(Log), 1044041, 25, 1044351); // bench 2
            AddCraft(typeof(WoodenBench), 1015076, 1022860, 95.0, 95.0, typeof(Log), 1044041, 35, 1044351); // bench 3
            index = AddCraft(typeof(WoodenBench), 1015076, 1024620, 50.0, 50.0, typeof(Log), 1044041, 15,
                1044351); // stone bench
            AddSkill(index, SkillName.Tinkering, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 70, 1044037);
            index = AddCraft(typeof(WoodenBench), 1015076, 1024635, 40.0, 40.0, typeof(Log), 1044041, 10,
                1044351); // stone chair
            AddSkill(index, SkillName.Tinkering, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 70, 1044037);

            // Containers
            AddCraft(typeof(WoodenBox), 1044292, 1023709, 44.0, 44.0, typeof(Log), 1044041, 10, 1044351);
            AddCraft(typeof(SmallCrate), 1044292, 1044309, 20.0, 20.0, typeof(Log), 1044041, 8, 1044351);
            AddCraft(typeof(MediumCrate), 1044292, 1044310, 41.0, 41.0, typeof(Log), 1044041, 15, 1044351);
            AddCraft(typeof(LargeCrate), 1044292, 1044311, 57.0, 57.0, typeof(Log), 1044041, 18, 1044351);
            AddCraft(typeof(WoodenChest), 1044292, 1023650, 84.0, 84.0, typeof(Log), 1044041, 20, 1044351);
            AddCraft(typeof(EmptyBookcase), 1044292, 1022718, 42.0, 42.0, typeof(Log), 1044041, 25, 1044351);
            AddCraft(typeof(FullBookcase), 1044292, 1022718, 42.0, 42.0, typeof(Log), 1044041, 25, 1044351);
            AddCraft(typeof(FancyArmoire), 1044292, 1044312, 94.0, 94.0, typeof(Log), 1044041, 35, 1044351);
            AddCraft(typeof(Armoire), 1044292, 1022643, 94.0, 94.0, typeof(Log), 1044041, 35, 1044351);
            AddCraft(typeof(PicnicBasket), 1044292, 1023706, 15.0, 15.0, typeof(Log), 1044041, 3, 1044351);

            // Tables
            AddCraft(typeof(Nightstand), 1015086, 1022620, 100.0, 100.0, typeof(Log), 1044041, 50, 1044351); // dresser
            AddCraft(typeof(Nightstand), 1015086, 1044306, 52.0, 52.0, typeof(Log), 1044041, 17, 1044351);
            AddCraft(typeof(WritingTable), 1015086, 1022890, 73.0, 73.0, typeof(Log), 1044041, 17, 1044351);
            AddCraft(typeof(YewWoodTable), 1015086, 1044307, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351);
            AddCraft(typeof(LargeTable), 1015086, 1044308, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351);
            AddCraft(typeof(LargeTable), 1015086, 1022868, 75.0, 75.0, typeof(Log), 1044041, 25, 1044351); // table 1
            AddCraft(typeof(LargeTable), 1015086, 1022868, 75.0, 75.0, typeof(Log), 1044041, 25, 1044351); // table 2
            AddCraft(typeof(LargeTable), 1015086, 1022868, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351); // table 3
            AddCraft(typeof(LargeTable), 1015086, 1022868, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351); // table 4
            AddCraft(typeof(LargeTable), 1015086, 1022868, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351); // table 5
            AddCraft(typeof(LargeTable), 1015086, 1022868, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351); // table 6
            AddCraft(typeof(LargeTable), 1015086, 1022868, 85.0, 85.0, typeof(Log), 1044041, 50, 1044351); // table 7
            index = AddCraft(typeof(LargeTable), 1015086, 1025460, 60.0, 60.0, typeof(Log), 1044041, 20,
                1044351); // stone table 8
            AddSkill(index, SkillName.Tinkering, 95.0, 95.0);
            AddRes(index, typeof(IronIngot), 1044036, 85, 1044037);

            // Staves and Shields
            AddCraft(typeof(BlackStaff), 1044295, 1023568, 94.0, 94.0, typeof(Log), 1044041, 10, 1044351);
            AddCraft(typeof(ShepherdsCrook), 1044295, 1023713, 89.0, 89.0, typeof(Log), 1044041, 7, 1044351);
            AddCraft(typeof(QuarterStaff), 1044295, 1023721, 84.0, 84.0, typeof(Log), 1044041, 6, 1044351);
            AddCraft(typeof(GnarledStaff), 1044295, 1025112, 89.0, 89.0, typeof(Log), 1044041, 7, 1044351);
            AddCraft(typeof(WoodenKiteShield), 1044295, 1027032, 62.0, 62.0, typeof(Log), 1044041, 9, 1044351);
            AddCraft(typeof(WoodenShield), 1044295, 1027034, 62.0, 62.0, typeof(Log), 1044041, 9, 1044351);

            // Other
            AddCraft(typeof(BarrelStaves), 1044294, 1027857, 2.0, 2.0, typeof(Log), 1044041, 5, 1044351);
            AddCraft(typeof(BarrelLid), 1044294, 1027608, 4.0, 4.0, typeof(Log), 1044041, 4, 1044351);
            AddCraft(typeof(TallMusicStand), 1044294, 1044315, 91.0, 91.0, typeof(Log), 1044041, 20, 1044351);
            index = AddCraft(typeof(StoneFireplaceEastDeed), 1044294, 1061848, 75.0, 75.0, typeof(Log), 1044041, 85,
                1044351);
            AddSkill(index, SkillName.Tinkering, 60.0, 60.0);
            AddRes(index, typeof(IronIngot), 1044036, 125, 1044037);
            index = AddCraft(typeof(StoneFireplaceSouthDeed), 1044294, 1061849, 75.0, 75.0, typeof(Log), 1044041, 85,
                1044351);
            AddSkill(index, SkillName.Tinkering, 60.0, 60.0);
            AddRes(index, typeof(IronIngot), 1044036, 125, 1044037);
            index = AddCraft(typeof(WaterVatEast), 1044294, 1025460, 80.0, 80.0, typeof(Log), 1044041, 75,
                1044351); //  vat
            AddSkill(index, SkillName.Tinkering, 50.0, 50.0);
            AddRes(index, typeof(IronIngot), 1044036, 15, 1044037);

            // Misc
            index = AddCraft(typeof(SmallBedSouthDeed), 1044290, 1044321, 105.0, 105.0, typeof(Log), 1044041, 100,
                1044351);
            AddSkill(index, SkillName.Tailoring, 85.0, 85.0);
            AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
            index = AddCraft(typeof(SmallBedEastDeed), 1044290, 1044322, 105.0, 105.0, typeof(Log), 1044041, 100,
                1044351);
            AddSkill(index, SkillName.Tailoring, 85.0, 85.0);
            AddRes(index, typeof(Cloth), 1044286, 100, 1044287);
            index = AddCraft(typeof(LargeBedSouthDeed), 1044290, 1044323, 105.0, 105.0, typeof(Log), 1044041, 150,
                1044351);
            AddSkill(index, SkillName.Tailoring, 85.0, 85.0);
            AddRes(index, typeof(Cloth), 1044286, 150, 1044287);
            index = AddCraft(typeof(LargeBedEastDeed), 1044290, 1044324, 105.0, 105.0, typeof(Log), 1044041, 150,
                1044351);
            AddSkill(index, SkillName.Tailoring, 85.0, 85.0);
            AddRes(index, typeof(Cloth), 1044286, 150, 1044287);
            index = AddCraft(typeof(LargePainting), 1044290, 1023748, 50.0, 50.0, typeof(Log), 1044041, 5,
                1044351); // painting 1
            AddSkill(index, SkillName.Tailoring, 30.0, 30.0);
            AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
            index = AddCraft(typeof(LargePainting), 1044290, 1023748, 50.0, 50.0, typeof(Log), 1044041, 5,
                1044351); // painting 2
            AddSkill(index, SkillName.Tailoring, 30.0, 30.0);
            AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
            AddCraft(typeof(DartBoardSouthDeed), 1044290, 1044325, 26.0, 26.0, typeof(Log), 1044041, 5, 1044351);
            AddCraft(typeof(DartBoardEastDeed), 1044290, 1044326, 26.0, 26.0, typeof(Log), 1044041, 5, 1044351);
            index = AddCraft(typeof(PentagramDeed), 1044290, 1044328, 110.0, 110.0, typeof(Log), 1044041, 100, 1044351);
            AddSkill(index, SkillName.Magery, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 40, 1044037);
            index = AddCraft(typeof(AltarDeed), 1044290, 1024628, 110.0, 110.0, typeof(Log), 1044041, 100, 1044351);
            AddSkill(index, SkillName.Magery, 60.0, 60.0);
            AddRes(index, typeof(IronIngot), 1044036, 40, 1044037);
            index = AddCraft(typeof(StatueEast), 1044290, 1030912, 95.0, 95.0, typeof(Log), 1044041, 10,
                1044351); //  statue 1
            AddSkill(index, SkillName.Tinkering, 95.0, 95.0);
            AddRes(index, typeof(IronIngot), 1044036, 125, 1044037);
            index = AddCraft(typeof(StatueEast), 1044290, 1030912, 95.0, 95.0, typeof(Log), 1044041, 10,
                1044351); //  statue 2
            AddSkill(index, SkillName.Tinkering, 95.0, 95.0);
            AddRes(index, typeof(IronIngot), 1044036, 125, 1044037);
            index = AddCraft(typeof(StatueEast), 1044290, 1030912, 95.0, 95.0, typeof(Log), 1044041, 10,
                1044351); //  statue 3
            AddSkill(index, SkillName.Tinkering, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);

            // Blacksmithy
            index = AddCraft(typeof(SmallForgeDeed), 1044296, 1044330, 84.0, 84.0, typeof(Log), 1044041, 5, 1044351);
            AddSkill(index, SkillName.Blacksmith, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 75, 1044037);
            index = AddCraft(typeof(LargeForgeEastDeed), 1044296, 1044331, 84.0, 84.0, typeof(Log), 1044041, 5,
                1044351);
            AddSkill(index, SkillName.Blacksmith, 90.0, 90.0);
            AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);
            index = AddCraft(typeof(LargeForgeSouthDeed), 1044296, 1044332, 84.0, 84.0, typeof(Log), 1044041, 5,
                1044351);
            AddSkill(index, SkillName.Blacksmith, 90.0, 90.0);
            AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);
            index = AddCraft(typeof(AnvilEastDeed), 1044296, 1044333, 84.0, 84.0, typeof(Log), 1044041, 5, 1044351);
            AddSkill(index, SkillName.Blacksmith, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 150, 1044037);
            index = AddCraft(typeof(AnvilSouthDeed), 1044296, 1044334, 84.0, 84.0, typeof(Log), 1044041, 5, 1044351);
            AddSkill(index, SkillName.Blacksmith, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 150, 1044037);

            // Training
            index = AddCraft(typeof(FishingPole), 1044297, 1023519, 78.0, 78.0, typeof(Log), 1044041, 5, 1044351);
            AddSkill(index, SkillName.Tailoring, 50.0, 50.0);
            AddRes(index, typeof(Cloth), 1044286, 5, 1044287);
            index = AddCraft(typeof(TrainingDummyEastDeed), 1044297, 1044335, 78.0, 78.0, typeof(Log), 1044041, 55,
                1044351);
            AddSkill(index, SkillName.Tailoring, 60.0, 60.0);
            AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
            index = AddCraft(typeof(TrainingDummySouthDeed), 1044297, 1044336, 78.0, 78.0, typeof(Log), 1044041, 55,
                1044351);
            AddSkill(index, SkillName.Tailoring, 60.0, 60.0);
            AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
            index = AddCraft(typeof(PickpocketDipEastDeed), 1044297, 1044337, 84.0, 84.0, typeof(Log), 1044041, 65,
                1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 60, 1044287);
            index = AddCraft(typeof(PickpocketDipSouthDeed), 1044297, 1044338, 84.0, 84.0, typeof(Log), 1044041, 65,
                1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 60, 1044287);

            // Tailoring
            index = AddCraft(typeof(Dressform), 1044298, 1044339, 73.0, 73.0, typeof(Log), 1044041, 25, 1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
            index = AddCraft(typeof(SpinningwheelEastDeed), 1044298, 1044341, 84.0, 84.0, typeof(Log), 1044041, 75,
                1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
            index = AddCraft(typeof(SpinningwheelSouthDeed), 1044298, 1044342, 84.0, 84.0, typeof(Log), 1044041, 75,
                1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
            index = AddCraft(typeof(LoomEastDeed), 1044298, 1044343, 94.0, 94.0, typeof(Log), 1044041, 85, 1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 25, 1044287);
            index = AddCraft(typeof(LoomSouthDeed), 1044298, 1044344, 94.0, 94.0, typeof(Log), 1044041, 85, 1044351);
            AddSkill(index, SkillName.Tailoring, 75.0, 75.0);
            AddRes(index, typeof(Cloth), 1044286, 25, 1044287);

            // Instruments
            index = AddCraft(typeof(Drums), 1044293, 1023740, 68.0, 68.0, typeof(Log), 1044041, 20, 1044351);
            AddSkill(index, SkillName.Tailoring, 55.0, 55.0);
            AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
            index = AddCraft(typeof(Tambourine), 1044293, 1023742, 68.0, 68.0, typeof(Log), 1044041, 15, 1044351);
            AddSkill(index, SkillName.Tailoring, 55.0, 55.0);
            AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
            index = AddCraft(typeof(Harp), 1044293, 1023761, 89.0, 89.0, typeof(Log), 1044041, 35, 1044351);
            AddSkill(index, SkillName.Tailoring, 55.0, 55.0);
            AddRes(index, typeof(Cloth), 1044286, 15, 1044287);
            index = AddCraft(typeof(LapHarp), 1044293, 1023762, 73.0, 73.0, typeof(Log), 1044041, 20, 1044351);
            AddSkill(index, SkillName.Tailoring, 55.0, 55.0);
            AddRes(index, typeof(Cloth), 1044286, 10, 1044287);
            index = AddCraft(typeof(Lute), 1044293, 1023763, 78.0, 78.0, typeof(Log), 1044041, 25, 1044351);
            AddSkill(index, SkillName.Tailoring, 55.0, 55.0);
            AddRes(index, typeof(Cloth), 1044286, 10, 1044287);

            // Cooking
            index = AddCraft(typeof(StoneOvenEastDeed), 1044299, 1044345, 78.0, 78.0, typeof(Log), 1044041, 85,
                1044351);
            AddSkill(index, SkillName.Tinkering, 60.0, 60.0);
            AddRes(index, typeof(IronIngot), 1044036, 125, 1044037);
            index = AddCraft(typeof(StoneOvenSouthDeed), 1044299, 1044346, 78.0, 78.0, typeof(Log), 1044041, 85,
                1044351);
            AddSkill(index, SkillName.Tinkering, 60.0, 60.0);
            AddRes(index, typeof(IronIngot), 1044036, 125, 1044037);
            index = AddCraft(typeof(FlourMillEastDeed), 1044299, 1044347, 105.0, 105.0, typeof(Log), 1044041, 100,
                1044351);
            AddSkill(index, SkillName.Tinkering, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 50, 1044037);
            index = AddCraft(typeof(FlourMillSouthDeed), 1044299, 1044348, 105.0, 105.0, typeof(Log), 1044041, 100,
                1044351);
            AddSkill(index, SkillName.Tinkering, 85.0, 85.0);
            AddRes(index, typeof(IronIngot), 1044036, 50, 1044037);
            AddCraft(typeof(WaterTroughEastDeed), 1044299, 1044349, 105.0, 105.0, typeof(Log), 1044041, 150, 1044351);
            AddCraft(typeof(WaterTroughSouthDeed), 1044299, 1044350, 105.0, 105.0, typeof(Log), 1044041, 150, 1044351);

            MarkOption = true;
            Repair = false;

            SetSubRes(typeof(Log), 1027136);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material	TODO: Verify the required skill amount
            LogConfiguration.Entries.ToList()
                .ForEach(e => AddSubRes(e.ResourceType, e.Name.Length > 0 ? e.Name : "Log", e.CraftSkillRequired,
                    1044022, e.Name));
        }
    }
}