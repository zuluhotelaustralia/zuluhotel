using System;
using Server.Mobiles;

namespace Server.Items
{
    public class SlayerGroup
    {
        private static SlayerEntry[] m_TotalEntries;
        private static SlayerGroup[] m_Groups;

        public static SlayerEntry[] TotalEntries
        {
            get { return m_TotalEntries; }
        }

        public static SlayerGroup[] Groups
        {
            get { return m_Groups; }
        }

        public static SlayerEntry GetEntryByName(SlayerName name)
        {
            int v = (int) name;

            if (v >= 0 && v < m_TotalEntries.Length)
                return m_TotalEntries[v];

            return null;
        }

        public static SlayerName GetLootSlayerType(Type type)
        {
            for (int i = 0; i < m_Groups.Length; ++i)
            {
                SlayerGroup group = m_Groups[i];
                Type[] foundOn = group.FoundOn;

                bool inGroup = false;

                for (int j = 0; foundOn != null && !inGroup && j < foundOn.Length; ++j)
                    inGroup = foundOn[j] == type;

                if (inGroup)
                {
                    int index = Utility.Random(1 + group.Entries.Length);

                    if (index == 0)
                        return group.m_Super.Name;

                    return group.Entries[index - 1].Name;
                }
            }

            return SlayerName.Silver;
        }

        static SlayerGroup()
        {
            SlayerGroup humanoid = new SlayerGroup();
            SlayerGroup undead = new SlayerGroup();
            SlayerGroup elemental = new SlayerGroup();
            SlayerGroup abyss = new SlayerGroup();
            SlayerGroup arachnid = new SlayerGroup();
            SlayerGroup reptilian = new SlayerGroup();
            SlayerGroup fey = new SlayerGroup();

            humanoid.Opposition = new[] {undead};
            humanoid.FoundOn = new[] {typeof(BoneKnight), typeof(Liche), typeof(LicheLord)};
            humanoid.Super = new SlayerEntry(SlayerName.Repond, typeof(Cyclops), typeof(Ettin), typeof(EvilMage),
                typeof(Troll), typeof(OgreLord), typeof(OgreLord), typeof(OrcCaptain), typeof(OrcCaptain),
                typeof(OrcishLord),
                typeof(OrcMage), typeof(RatmanMarksman), typeof(Titan),
                typeof(Troll));
            humanoid.Entries = new[]
            {
                new SlayerEntry(SlayerName.OgreTrashing, typeof(OgreLord), typeof(OgreLord)),
                new SlayerEntry(SlayerName.OrcSlaying, typeof(OrcCaptain), typeof(OrcCaptain), typeof(OrcishLord),
                    typeof(OrcMage)),
                new SlayerEntry(SlayerName.TrollSlaughter, typeof(Troll), typeof(TrollLord))
            };

            undead.Opposition = new[] {humanoid};
            undead.Super = new SlayerEntry(SlayerName.Silver, typeof(BoneKnight), typeof(BoneMagician), typeof(Ghost),
                typeof(Liche), typeof(LicheLord), typeof(Mummy), typeof(Revenant), typeof(Shade),
                typeof(SkeletalAssassin), typeof(SkeletalWarrior), typeof(Skeleton), typeof(Spectre), typeof(Wraith),
                typeof(Zombie));
            undead.Entries = new SlayerEntry[0];

            fey.Opposition = new[] {abyss};
            fey.Super = new SlayerEntry(SlayerName.Fey, typeof(Wisp));
            fey.Entries = new SlayerEntry[0];

            elemental.Opposition = new[] {abyss};
            elemental.FoundOn = new[] {typeof(Balron), typeof(Daemon)};
            elemental.Super = new SlayerEntry(SlayerName.ElementalBan, typeof(AirElemental), typeof(BloodElemental),
                typeof(EarthElemental), typeof(FireElemental), typeof(IceElemental),
                typeof(PoisonElemental), typeof(WaterElemental));
            elemental.Entries = new[]
            {
                new SlayerEntry(SlayerName.BloodDrinking, typeof(BloodElemental)),
                new SlayerEntry(SlayerName.EarthShatter, typeof(EarthElemental)),
                new SlayerEntry(SlayerName.ElementalHealth, typeof(PoisonElemental)),
                new SlayerEntry(SlayerName.FlameDousing, typeof(FireElemental)),
                new SlayerEntry(SlayerName.SummerWind, typeof(IceElemental)),
                new SlayerEntry(SlayerName.Vacuum, typeof(AirElemental)),
                new SlayerEntry(SlayerName.WaterDissipation, typeof(WaterElemental))
            };

            abyss.Opposition = new[] {elemental, fey};
            abyss.FoundOn = new[] {typeof(BloodElemental)};

            abyss.Super = new SlayerEntry(SlayerName.Exorcism, typeof(Balron), typeof(Daemon), typeof(Gargoyle),
                typeof(IceFiend), typeof(Imp), typeof(StoneGargoyle));

            abyss.Entries = new[]
            {
                new SlayerEntry(SlayerName.DaemonDismissal, typeof(Balron), typeof(Daemon), typeof(IceFiend),
                    typeof(Imp)),
                new SlayerEntry(SlayerName.GargoylesFoe, typeof(Gargoyle), typeof(StoneGargoyle)),
                new SlayerEntry(SlayerName.BalronDamnation, typeof(Balron))
            };

            arachnid.Opposition = new[] {reptilian};
            arachnid.FoundOn = new[] {typeof(AncientDracoliche), typeof(Dragon), typeof(OphidianAvenger)};
            arachnid.Super = new SlayerEntry(SlayerName.ArachnidDoom, typeof(GiantFrostSpider), typeof(GiantRockSpider),
                typeof(GiantScorpion), typeof(TerathanAvenger), typeof(TerathanDrone), typeof(TerathanMatriarch),
                typeof(TerathanWarrior));
            arachnid.Entries = new[]
            {
                new SlayerEntry(SlayerName.ScorpionsBane, typeof(GiantScorpion)),
                new SlayerEntry(SlayerName.SpidersDeath, typeof(GiantFrostSpider), typeof(GiantRockSpider)),
                new SlayerEntry(SlayerName.Terathan, typeof(TerathanAvenger), typeof(TerathanDrone),
                    typeof(TerathanMatriarch), typeof(TerathanWarrior))
            };

            reptilian.Opposition = new[] {arachnid};
            reptilian.FoundOn = new[] {typeof(TerathanAvenger), typeof(TerathanMatriarch)};
            reptilian.Super = new SlayerEntry(SlayerName.ReptilianDeath, typeof(AncientDracoliche),
                typeof(DeepSeaSerpent),
                typeof(Dragon), typeof(Drake), typeof(IceSerpent), typeof(GiantSerpent), typeof(IceSnake),
                typeof(LavaSerpent), typeof(LavaSnake), typeof(LizardShaman), typeof(OphidianAvenger)
                , typeof(OphidianWarrior),
                typeof(SeaSerpent), typeof(SilverSerpent), typeof(Snake), typeof(PoisonWyrm), typeof(Wyvern));
            reptilian.Entries = new[]
            {
                new SlayerEntry(SlayerName.DragonSlaying, typeof(AncientDracoliche), typeof(Dragon), typeof(Drake),
                    typeof(PoisonWyrm), typeof(Wyvern)),
                new SlayerEntry(SlayerName.LizardmanSlaughter, typeof(LizardShaman)),
                new SlayerEntry(SlayerName.Ophidian, typeof(OphidianAvenger), typeof(OphidianWarrior)),
                new SlayerEntry(SlayerName.SnakesBane, typeof(DeepSeaSerpent), typeof(GiantSerpent), typeof(IceSerpent),
                    typeof(IceSnake), typeof(LavaSerpent), typeof(LavaSnake), typeof(SeaSerpent), typeof(SilverSerpent),
                    typeof(Snake))
            };

            m_Groups = new[]
            {
                humanoid,
                undead,
                elemental,
                abyss,
                arachnid,
                reptilian,
                fey
            };

            m_TotalEntries = CompileEntries(m_Groups);
        }

        private static SlayerEntry[] CompileEntries(SlayerGroup[] groups)
        {
            SlayerEntry[] entries = new SlayerEntry[28];

            for (int i = 0; i < groups.Length; ++i)
            {
                SlayerGroup g = groups[i];

                g.Super.Group = g;

                entries[(int) g.Super.Name] = g.Super;

                for (int j = 0; j < g.Entries.Length; ++j)
                {
                    g.Entries[j].Group = g;
                    entries[(int) g.Entries[j].Name] = g.Entries[j];
                }
            }

            return entries;
        }

        private SlayerGroup[] m_Opposition;
        private SlayerEntry m_Super;
        private SlayerEntry[] m_Entries;
        private Type[] m_FoundOn;

        public SlayerGroup[] Opposition
        {
            get { return m_Opposition; }
            set { m_Opposition = value; }
        }

        public SlayerEntry Super
        {
            get { return m_Super; }
            set { m_Super = value; }
        }

        public SlayerEntry[] Entries
        {
            get { return m_Entries; }
            set { m_Entries = value; }
        }

        public Type[] FoundOn
        {
            get { return m_FoundOn; }
            set { m_FoundOn = value; }
        }

        public bool OppositionSuperSlays(Mobile m)
        {
            for (int i = 0; i < Opposition.Length; i++)
            {
                if (Opposition[i].Super.Slays(m))
                    return true;
            }

            return false;
        }

        public SlayerGroup()
        {
        }
    }
}