using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using static Server.Configurations.MessageHueConfiguration;

namespace Server.Engines.Harvest
{
    public class Mining : HarvestSystem
    {
        private static Mining m_System;

        public static Mining System
        {
            get
            {
                if (m_System == null)
                    m_System = new Mining();

                return m_System;
            }
        }

        private Mining()
        {
            HarvestResource[] res;
            HarvestVein[] veins;

            #region Mining for ore and stone

            HarvestDefinition oreAndStone = new HarvestDefinition();

            // Resource banks are every 1x1 tiles
            oreAndStone.BankWidth = 1;
            oreAndStone.BankHeight = 1;

            // Every bank holds from 45 to 90 ore
            oreAndStone.MinTotal = 45;
            oreAndStone.MaxTotal = 90;

            // A resource bank will respawn its content every 10 to 20 minutes
            oreAndStone.MinRespawn = TimeSpan.FromMinutes(10.0);
            oreAndStone.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the Mining skill
            oreAndStone.Skill = SkillName.Mining;

            // Set the list of harvestable tiles
            oreAndStone.Tiles = m_MountainAndCaveTiles;

            // Players must be within 2 tiles to harvest
            oreAndStone.MaxRange = 2;

            // One ore per harvest action
            oreAndStone.ConsumedPerHarvest = skillValue => (int) (skillValue / 15) + 1;

            // The digging effect
            oreAndStone.EffectActions = new[] {11};
            oreAndStone.EffectSounds = new[] {0x042};
            oreAndStone.EffectCounts = new[] {4};
            oreAndStone.EffectDelay = TimeSpan.FromSeconds(1.6);
            oreAndStone.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            oreAndStone.NoResourcesMessage = 503040; // There is no metal here to mine.
            oreAndStone.DoubleHarvestMessage = 503042; // Someone has gotten to the metal before you.
            oreAndStone.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
            oreAndStone.OutOfRangeMessage = 500446; // That is too far away.
            oreAndStone.FailMessage = 503043; // You loosen some rocks but fail to find any useable ore.
            oreAndStone.PackFullMessage = 1010481; // Your backpack is full, so the ore you mined is lost.
            oreAndStone.ToolBrokeMessage = 1044038; // You have worn out your tool!

            // "You dig some ~ore~ and put in your backpack."

            res = new[]
            {
                new HarvestResource(5.0, "You dig some Spike ore and put it in your backpack.",
                    typeof(SpikeOre)),
                new HarvestResource(10.0, "You dig some Fruity ore and put it in your backpack.",
                    typeof(FruityOre)),
                new HarvestResource(20.0, "You dig some Ice Rock ore and put it in your backpack.",
                    typeof(IceRockOre)),
                new HarvestResource(25.0, "You dig some Black Dwarf ore and put it in your backpack.",
                    typeof(BlackDwarfOre)),
                new HarvestResource(15.0, "You dig some Bronze ore and put it in your backpack.",
                    typeof(BronzeOre)),
                new HarvestResource(45.0, "You dig some Dark Pagan ore and put it in your backpack.",
                    typeof(DarkPaganOre)),
                new HarvestResource(40.0, "You dig some Silver Rock ore and put it in your backpack.",
                    typeof(SilverRockOre)),
                new HarvestResource(35.0, "You dig some Platinum ore and put it in your backpack.",
                    typeof(PlatinumOre)),
                new HarvestResource(30.0, "You dig some Dull Copper ore and put it in your backpack.",
                    typeof(DullCopperOre)),
                new HarvestResource(55.0, "You dig some Mystic ore and put it in your backpack.",
                    typeof(MysticOre)),
                new HarvestResource(50.0, "You dig some Copper ore and put it in your backpack.",
                    typeof(CopperOre)),
                new HarvestResource(60.0, "You dig some Spectral ore and put it in your backpack.",
                    typeof(SpectralOre)),
                new HarvestResource(65.0, "You dig some Old Britain ore and put it in your backpack.",
                    typeof(OldBritainOre)),
                new HarvestResource(70.0, "You dig some Onyx ore and put it in your backpack.",
                    typeof(OnyxOre)),
                new HarvestResource(75.0, "You dig some Red Elven ore and put it in your backpack.",
                    typeof(RedElvenOre)),
                new HarvestResource(80.0, "You dig some Undead ore and put it in your backpack.",
                    typeof(UndeadOre)),
                new HarvestResource(85.0, "You dig some Pyrite ore and put it in your backpack.",
                    typeof(PyriteOre)),
                new HarvestResource(90.0, "You dig some Virginity ore and put it in your backpack.",
                    typeof(VirginityOre)),
                new HarvestResource(95.0, "You dig some Malachite ore and put it in your backpack.",
                    typeof(MalachiteOre)),
                new HarvestResource(97.0, "You dig some Lavarock ore and put it in your backpack.",
                    typeof(LavarockOre)),
                new HarvestResource(98.0, "You dig some Azurite ore and put it in your backpack.",
                    typeof(AzuriteOre)),
                new HarvestResource(100.0, "You dig some Dripstone ore and put it in your backpack.",
                    typeof(DripstoneOre)),
                new HarvestResource(104.0, "You dig some Executor ore and put it in your backpack.",
                    typeof(ExecutorOre)),
                new HarvestResource(108.0, "You dig some Peachblue ore and put it in your backpack.",
                    typeof(PeachblueOre)),
                new HarvestResource(112.0, "You dig some Destruction ore and put it in your backpack.",
                    typeof(DestructionOre)),
                new HarvestResource(116.0, "You dig some Anra ore and put it in your backpack.",
                    typeof(AnraOre)),
                new HarvestResource(119.0, "You dig some Crystal ore and put it in your backpack.",
                    typeof(CrystalOre)),
                new HarvestResource(122.0, "You dig some Doom ore and put it in your backpack.",
                    typeof(DoomOre)),
                new HarvestResource(125.0, "You dig some Goddess ore and put it in your backpack.",
                    typeof(GoddessOre)),
                new HarvestResource(129.0, "You dig some New Zulu ore and put it in your backpack.",
                    typeof(NewZuluOre)),
                new HarvestResource(130.0,
                    "You dig some Dark Sable Ruby ore and put it in your backpack.", typeof(DarkSableRubyOre)),
                new HarvestResource(130.0,
                    "You dig some Ebon Twilight Sapphire ore and put it in your backpack.",
                    typeof(EbonTwilightSapphireOre)),
                new HarvestResource(140.0,
                    "You dig some Radiant Nimbus Diamond ore and put it in your backpack.",
                    typeof(RadiantNimbusDiamondOre)),
            };


            veins = new[]
            {
                new HarvestVein(2.0, res[28]), // Goddess
                new HarvestVein(5.0, res[27]), // Doom
                new HarvestVein(8.0, res[26]), // Crystal
                new HarvestVein(10.0, res[25]), // Anra
                new HarvestVein(14.0, res[24]), // Destruction
                new HarvestVein(17.0, res[23]), // Peachblue
                new HarvestVein(20.0, res[22]), // Executor
                new HarvestVein(23.0, res[21]), // Dripstone
                new HarvestVein(26.0, res[20]), // Azurite
                new HarvestVein(33.0, res[19]), // Lavarock
                new HarvestVein(38.0, res[18]), // Malachite
                new HarvestVein(42.0, res[17]), // Virginity
                new HarvestVein(46.0, res[16]), // Pyrite
                new HarvestVein(51.0, res[15]), // Undead
                new HarvestVein(56.0, res[14]), // RedElven
                new HarvestVein(66.0, res[13]), // Onyx
                new HarvestVein(61.0, res[12]), // OldBritain
                new HarvestVein(71.0, res[11]), // Spectral
                new HarvestVein(74.0, res[10]), // Copper
                new HarvestVein(80.0, res[9]), // Mystic
                new HarvestVein(85.0, res[8]), // DullCopper
                new HarvestVein(90.0, res[7]), // Platinum
                new HarvestVein(100.0, res[6]), // SilverRock
                new HarvestVein(110.0, res[5]), // DarkPagan
                new HarvestVein(120.0, res[4]), // Bronze
                new HarvestVein(125.0, res[3]), // BlackDwarf
                new HarvestVein(130.0, res[2]), // IceRock
                new HarvestVein(140.0, res[1]), // Fruity
                new HarvestVein(155.0, res[0]) // Spike
            };

            oreAndStone.Resources = res;
            oreAndStone.Veins = veins;

            oreAndStone.DefaultVein = new HarvestVein(0.0, new HarvestResource(0.0,
                "You dig some Iron ore and put it in your backpack.",
                typeof(IronOre)));

            Definitions.Add(oreAndStone);

            #endregion
        }

        public override bool CheckHarvest(Mobile from, Item tool)
        {
            if (!base.CheckHarvest(from, tool))
                return false;

            if (from.IsBodyMod && !from.Body.IsHuman)
            {
                from.SendLocalizedMessage(501865); // You can't mine while polymorphed.
                return false;
            }

            return true;
        }

        public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            if (!base.CheckHarvest(from, tool, def, toHarvest))
                return false;


            if (from.IsBodyMod && !from.Body.IsHuman)
            {
                from.SendLocalizedMessage(501865); // You can't mine while polymorphed.
                return false;
            }

            return true;
        }

        private static int[] m_Offsets = new[]
        {
            -1, -1,
            -1, 0,
            -1, 1,
            0, -1,
            0, 1,
            1, -1,
            1, 0,
            1, 1
        };

        public override bool BeginHarvesting(Mobile from, Item tool)
        {
            if (!base.BeginHarvesting(from, tool))
                return false;

            from.SendLocalizedMessage(503033); // Where do you wish to dig?
            return true;
        }

        public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            from.SendAsciiMessage(MessageSuccessHue, "You start mining...");
        }

        public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
            if (toHarvest is LandTarget)
                from.SendLocalizedMessage(501862); // You can't mine there.
            else
                from.SendLocalizedMessage(501863); // You can't mine that.
        }

        #region Tile lists

        private static int[] m_MountainAndCaveTiles = new[]
        {
            220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
            230, 231, 236, 237, 238, 239, 240, 241, 242, 243,
            244, 245, 246, 247, 252, 253, 254, 255, 256, 257,
            258, 259, 260, 261, 262, 263, 268, 269, 270, 271,
            272, 273, 274, 275, 276, 277, 278, 279, 286, 287,
            288, 289, 290, 291, 292, 293, 294, 296, 296, 297,
            321, 322, 323, 324, 467, 468, 469, 470, 471, 472,
            473, 474, 476, 477, 478, 479, 480, 481, 482, 483,
            484, 485, 486, 487, 492, 493, 494, 495, 543, 544,
            545, 546, 547, 548, 549, 550, 551, 552, 553, 554,
            555, 556, 557, 558, 559, 560, 561, 562, 563, 564,
            565, 566, 567, 568, 569, 570, 571, 572, 573, 574,
            575, 576, 577, 578, 579, 581, 582, 583, 584, 585,
            586, 587, 588, 589, 590, 591, 592, 593, 594, 595,
            596, 597, 598, 599, 600, 601, 610, 611, 612, 613,

            1010, 1741, 1742, 1743, 1744, 1745, 1746, 1747, 1748, 1749,
            1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1771, 1772,
            1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1781, 1782,
            1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1801, 1802,
            1803, 1804, 1805, 1806, 1807, 1808, 1809, 1811, 1812, 1813,
            1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823,
            1824, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839,
            1840, 1841, 1842, 1843, 1844, 1845, 1846, 1847, 1848, 1849,
            1850, 1851, 1852, 1853, 1854, 1861, 1862, 1863, 1864, 1865,
            1866, 1867, 1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875,
            1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1981,
            1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991,
            1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001,
            2002, 2003, 2004, 2028, 2029, 2030, 2031, 2032, 2033, 2100,
            2101, 2102, 2103, 2104, 2105,

            0x453B, 0x453C, 0x453D, 0x453E, 0x453F, 0x4540, 0x4541,
            0x4542, 0x4543, 0x4544, 0x4545, 0x4546, 0x4547, 0x4548,
            0x4549, 0x454A, 0x454B, 0x454C, 0x454D, 0x454E, 0x454F
        };

        private static int[] m_SandTiles = new[]
        {
            22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
            32, 33, 34, 35, 36, 37, 38, 39, 40, 41,
            42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
            52, 53, 54, 55, 56, 57, 58, 59, 60, 61,
            62, 68, 69, 70, 71, 72, 73, 74, 75,

            286, 287, 288, 289, 290, 291, 292, 293, 294, 295,
            296, 297, 298, 299, 300, 301, 402, 424, 425, 426,
            427, 441, 442, 443, 444, 445, 446, 447, 448, 449,
            450, 451, 452, 453, 454, 455, 456, 457, 458, 459,
            460, 461, 462, 463, 464, 465, 642, 643, 644, 645,
            650, 651, 652, 653, 654, 655, 656, 657, 821, 822,
            823, 824, 825, 826, 827, 828, 833, 834, 835, 836,
            845, 846, 847, 848, 849, 850, 851, 852, 857, 858,
            859, 860, 951, 952, 953, 954, 955, 956, 957, 958,
            967, 968, 969, 970,

            1447, 1448, 1449, 1450, 1451, 1452, 1453, 1454, 1455,
            1456, 1457, 1458, 1611, 1612, 1613, 1614, 1615, 1616,
            1617, 1618, 1623, 1624, 1625, 1626, 1635, 1636, 1637,
            1638, 1639, 1640, 1641, 1642, 1647, 1648, 1649, 1650
        };

        #endregion
    }
}