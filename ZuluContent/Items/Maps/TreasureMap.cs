using System;
using System.IO;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class TreasureMap : MapItem
    {
        private int m_Level;
        private Mobile m_Decoder;
        private Map m_Map;
        private Point2D m_Location;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Decoder
        {
            get { return m_Decoder; }
            set { m_Decoder = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map ChestMap
        {
            get { return m_Map; }
            set { m_Map = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point2D ChestLocation
        {
            get { return m_Location; }
            set { m_Location = value; }
        }

        private static Point2D[] m_Locations;

        private static readonly string[][] SpawnTypes = {
            new[] {"Skeleton"},
            new[]
            {
                "Mongbat", "OrcWarrior", "OrcWarrior", "OrcWarrior", "RatmanMarksman",
                "RatmanMarksman", "RatmanMarksman", "HeadlessOne", "Skeleton", "Zombie"
            },
            new[] {"OrcMage", "OrcishLord", "Gazer", "Gargoyle"},
            new[]
            {
                "Liche", "BoneKnight", "AirElemental", "WaterElemental", "FireElemental",
                "EarthElemental", "OgreLord", "Troll"
            },
            new[]
            {
                "OgreLord", "Liche", "Daemon", "Drake", "TrollLord", "Vampire",
                "Vampire", "Drake", "SpectralDrake", "AirElementalLord",
                "WaterElementalLord", "EarthElementalLord", "FireElementalLord"
            },
            new[]
            {
                "BlackWisp", "Dragon", "Dragon", "DaemonLieutenant", "Dracula",
                "Wyvern", "BloodElemental", "PoisonElemental", "RockDragon", "MageHunter"
            }
        };

        public const double LootChance = 0.01; // 1% chance to appear as loot

        public static Point2D GetRandomLocation()
        {
            if (m_Locations == null)
                LoadLocations();

            if (m_Locations.Length > 0)
                return m_Locations[Utility.Random(m_Locations.Length)];

            return Point2D.Zero;
        }

        private static void LoadLocations()
        {
            string filePath = Path.Combine(Core.BaseDirectory, "Data/treasure.cfg");

            List<Point2D> list = new List<Point2D>();

            if (File.Exists(filePath))
            {
                using (StreamReader ip = new StreamReader(filePath))
                {
                    string line;

                    while ((line = ip.ReadLine()) != null)
                    {
                        try
                        {
                            string[] split = line.Split(' ');

                            int x = Convert.ToInt32(split[0]), y = Convert.ToInt32(split[1]);

                            Point2D loc = new Point2D(x, y);
                            list.Add(loc);
                        }
                        catch
                        {
                        }
                    }
                }
            }

            m_Locations = list.ToArray();
        }

        public static BaseCreature Spawn(int level, Point3D p, bool guardian)
        {
            if (level >= 0 && level < SpawnTypes.Length)
            {
                BaseCreature bc;

                try
                {
                    bc = Creatures.Create(SpawnTypes[level][Utility.Random(SpawnTypes[level].Length)]);
                }
                catch
                {
                    return null;
                }

                bc.Home = p;
                bc.RangeHome = 5;
                bc.Summoned = true;

                return bc;
            }

            return null;
        }

        public static BaseCreature Spawn(int level, Point3D p, Map map, Mobile target, bool guardian)
        {
            if (map == null)
                return null;

            BaseCreature c = Spawn(level, p, guardian);

            if (c != null)
            {
                bool spawned = false;

                for (int i = 0; !spawned && i < 10; ++i)
                {
                    int x = p.X - 3 + Utility.Random(7);
                    int y = p.Y - 3 + Utility.Random(7);

                    if (map.CanSpawnMobile(x, y, p.Z))
                    {
                        c.MoveToWorld(new Point3D(x, y, p.Z), map);
                        spawned = true;
                    }
                    else
                    {
                        int z = map.GetAverageZ(x, y);

                        if (map.CanSpawnMobile(x, y, z))
                        {
                            c.MoveToWorld(new Point3D(x, y, z), map);
                            spawned = true;
                        }
                    }
                }

                if (!spawned)
                {
                    c.Delete();
                    return null;
                }

                if (target != null)
                    c.Combatant = target;

                return c;
            }

            return null;
        }


        [Constructible]
        public TreasureMap(int level, Map map)
        {
            m_Level = level;
            m_Map = map;

            m_Location = GetRandomLocation();

            Width = 300;
            Height = 300;

            ItemID = 0x14ED;

            int width = 600;
            int height = 600;

            int x1 = m_Location.X - Utility.RandomMinMax(width / 4, width / 4 * 3);
            int y1 = m_Location.Y - Utility.RandomMinMax(height / 4, height / 4 * 3);

            if (x1 < 0)
                x1 = 0;

            if (y1 < 0)
                y1 = 0;

            int x2 = x1 + width;
            int y2 = y1 + height;

            if (x2 >= 5120)
                x2 = 5119;

            if (y2 >= 4096)
                y2 = 4095;

            x1 = x2 - width;
            y1 = y2 - height;

            Bounds = new Rectangle2D(x1, y1, width, height);
            Protected = true;
        }

        [Constructible]
        public TreasureMap(Serial serial) : base(serial)
        {
        }

        public static bool HasDiggingTool(Mobile m)
        {
            if (m.Backpack == null)
                return false;

            List<BaseHarvestTool> items = m.Backpack.FindItemsByType<BaseHarvestTool>();

            foreach (BaseHarvestTool tool in items)
            {
                if (tool.HarvestSystem == Engines.Harvest.Mining.System)
                    return true;
            }

            return false;
        }

        public void OnBeginDig(Mobile from)
        {
            if (m_Decoder != from && !HasRequiredSkill(from))
            {
                from.SendLocalizedMessage(
                    503031); // You did not decode this map and have no clue where to look for the treasure.
            }
            else if (!from.CanBeginAction(typeof(TreasureMap)))
            {
                from.SendLocalizedMessage(503020); // You are already digging treasure.
            }
            else if (from.Map != m_Map)
            {
                from.SendLocalizedMessage(1010479); // You seem to be in the right place, but may be on the wrong facet!
            }
            else
            {
                from.SendLocalizedMessage(503033); // Where do you wish to dig?
                from.Target = new DigTarget(this);
            }
        }

        private class DigTarget : Target
        {
            private TreasureMap m_Map;

            public DigTarget(TreasureMap map) : base(6, true, TargetFlags.None)
            {
                m_Map = map;
            }

            protected override async void OnTarget(Mobile from, object targeted)
            {
                if (m_Map.Deleted)
                    return;

                Map map = m_Map.m_Map;

                if (m_Map.m_Decoder != from && !m_Map.HasRequiredSkill(from))
                {
                    from.SendLocalizedMessage(
                        503031); // You did not decode this map and have no clue where to look for the treasure.
                    return;
                }
                else if (!from.CanBeginAction(typeof(TreasureMap)))
                {
                    from.SendLocalizedMessage(503020); // You are already digging treasure.
                }
                else if (!HasDiggingTool(from))
                {
                    from.SendMessage("You must have a digging tool to dig for treasure.");
                }
                else if (from.Map != map)
                {
                    from.SendLocalizedMessage(
                        1010479); // You seem to be in the right place, but may be on the wrong facet!
                }
                else
                {
                    IPoint3D p = targeted as IPoint3D;

                    Point3D targ3D;
                    if (p is Item)
                        targ3D = ((Item) p).GetWorldLocation();
                    else
                        targ3D = new Point3D(p);

                    from.PlaySound(899);
                    await Timer.Pause(600);
                    from.PlaySound(901);
                    await Timer.Pause(600);

                    int maxRange;
                    double skillValue = from.Skills[SkillName.Mining].Value;

                    // TODO: Add Ranger classe bonus

                    if (skillValue >= 140.0)
                        maxRange = 8;
                    else if (skillValue >= 120.0)
                        maxRange = 7;
                    else if (skillValue >= 100.0)
                        maxRange = 6;
                    else if (skillValue >= 80.0)
                        maxRange = 5;
                    else if (skillValue >= 60.0)
                        maxRange = 4;
                    else if (skillValue >= 40.0)
                        maxRange = 3;
                    else if (skillValue >= 20.0)
                        maxRange = 2;
                    else
                        maxRange = 1;

                    Point2D loc = m_Map.m_Location;
                    int x = loc.X, y = loc.Y;

                    Point3D chest3D0 = new Point3D(loc, 0);

                    if (!Utility.InRange(targ3D, chest3D0, maxRange))
                    {
                        Direction dir = Utility.GetDirection(targ3D, chest3D0);

                        string sDir;
                        switch (dir)
                        {
                            case Direction.North:
                                sDir = "north";
                                break;
                            case Direction.Right:
                                sDir = "northeast";
                                break;
                            case Direction.East:
                                sDir = "east";
                                break;
                            case Direction.Down:
                                sDir = "southeast";
                                break;
                            case Direction.South:
                                sDir = "south";
                                break;
                            case Direction.Left:
                                sDir = "southwest";
                                break;
                            case Direction.West:
                                sDir = "west";
                                break;
                            default:
                                sDir = "northwest";
                                break;
                        }

                        from.SendAsciiMessage(0x44, $"You dig for awhile and have a feeling to travel to the {sDir}.");
                    }
                    else
                    {
                        int z = map.GetAverageZ(x, y);
                        new DigTimer(from, m_Map, new Point3D(x, y, z), map).Start();
                    }
                }
            }
        }

        private class DigTimer : Timer
        {
            private Mobile m_From;
            private TreasureMap m_TreasureMap;

            private Point3D m_Location;
            private Map m_Map;

            private TreasureChestDirt m_Dirt1;
            private TreasureChestDirt m_Dirt2;
            private TreasureMapChest m_Chest;

            private int m_Count;

            public DigTimer(Mobile from, TreasureMap treasureMap, Point3D location, Map map) : base(TimeSpan.Zero,
                TimeSpan.FromMilliseconds(500))
            {
                m_From = from;
                m_TreasureMap = treasureMap;

                m_Location = location;
                m_Map = map;

                Priority = TimerPriority.TenMS;
            }

            protected override void OnTick()
            {
                m_Count++;

                m_From.RevealingAction();
                m_From.Direction = m_From.GetDirectionTo(m_Location);

                if (m_Count > 1 && m_Dirt1 == null)
                {
                    m_Dirt1 = new TreasureChestDirt();
                    m_Dirt1.MoveToWorld(m_Location, m_Map);

                    m_Dirt2 = new TreasureChestDirt();
                    m_Dirt2.MoveToWorld(new Point3D(m_Location.X, m_Location.Y - 1, m_Location.Z), m_Map);
                }

                if (m_Count == 3)
                {
                    m_Dirt1.Turn1();
                }
                else if (m_Count == 6)
                {
                    m_Dirt1.Turn2();
                    m_Dirt2.Turn2();
                }
                else if (m_Count > 6)
                {
                    if (m_Chest == null)
                    {
                        m_Chest = new TreasureMapChest(m_From, m_TreasureMap.Level, true);
                        m_Chest.MoveToWorld(new Point3D(m_Location.X, m_Location.Y, m_Location.Z - 15), m_Map);
                    }
                    else
                    {
                        m_Chest.Z++;
                    }

                    Effects.PlaySound(m_Chest, 0x33B);
                }

                if (m_Chest != null && m_Chest.Location.Z >= m_Location.Z)
                {
                    Stop();

                    m_Chest.Temporary = false;

                    int spawns;
                    switch (m_TreasureMap.Level)
                    {
                        case 0:
                            spawns = 3;
                            break;
                        case 1:
                            spawns = 0;
                            break;
                        default:
                            spawns = 4;
                            break;
                    }

                    m_From.SendAsciiMessage(0x44, "You unleash the treasure's guardians!");
                    m_From.SendAsciiMessage(0x44, "The chest will unlock when all guardians are destroyed.");

                    for (int i = 0; i < spawns; ++i)
                    {
                        BaseCreature bc = Spawn(m_TreasureMap.Level, m_Chest.Location, m_Chest.Map, null, true);

                        if (bc != null)
                            m_Chest.Guardians.Add(bc);
                    }

                    m_TreasureMap.Delete();
                }
                else
                {
                    new SoundTimer(m_From, 0x125 + m_Count % 2).Start();
                }
            }

            private class SoundTimer : Timer
            {
                private Mobile m_From;
                private int m_SoundID;

                public SoundTimer(Mobile from, int soundID) : base(TimeSpan.FromSeconds(0.9))
                {
                    m_From = from;
                    m_SoundID = soundID;

                    Priority = TimerPriority.TenMS;
                }

                protected override void OnTick()
                {
                    m_From.PlaySound(m_SoundID);
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }

            if (m_Decoder == null)
                Decode(from);
            else
                DisplayTo(from);
        }

        private double GetMinSkillLevel()
        {
            switch (m_Level)
            {
                case 1: return 20.0;
                case 2: return 40.0;
                case 3: return 60.0;
                case 4: return 80.0;
                case 5: return 100.0;

                default: return 0.0;
            }
        }

        private bool HasRequiredSkill(Mobile from)
        {
            return @from.Skills[SkillName.Cartography].Value >= GetMinSkillLevel();
        }

        public void Decode(Mobile from)
        {
            if (m_Decoder != null)
                return;

            double minSkill = GetMinSkillLevel();

            if (from.Skills[SkillName.Cartography].Value < minSkill)
                from.SendLocalizedMessage(503013); // The map is too difficult to attempt to decode.

            double maxSkill = minSkill + 60.0;

            if (!from.CheckSkill(SkillName.Cartography, minSkill, maxSkill))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2,
                    503018); // You fail to make anything of the map.
                return;
            }

            from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 503019); // You successfully decode a treasure map!
            Decoder = from;
            ItemID = 0x14EC;

            DisplayTo(from);
        }

        public override void DisplayTo(Mobile from)
        {
            if (m_Decoder != from && !HasRequiredSkill(from))
            {
                from.SendLocalizedMessage(
                    503031); // You did not decode this map and have no clue where to look for the treasure.
                return;
            }

            from.PlaySound(0x249);
            base.DisplayTo(from);
        }

        public override int LabelNumber
        {
            get
            {
                if (m_Decoder != null)
                {
                    if (m_Level == 6)
                        return 1063453;
                    else
                        return 1041516 + m_Level;
                }
                else if (m_Level == 6)
                    return 1063452;
                else
                    return 1041510 + m_Level;
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            if (m_Decoder != null)
            {
                LabelTo(from, $"a treasure map lvl{m_Level}");
            }
            else
            {
                LabelTo(from, "a tattered map");
            }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0);

            writer.Write(m_Level);
            writer.Write(m_Decoder);
            writer.Write(m_Map);
            writer.Write(m_Location);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Level = (int) reader.ReadInt();
            m_Decoder = reader.ReadEntity<Mobile>();
            m_Map = reader.ReadMap();
            m_Location = reader.ReadPoint2D();
        }
    }

    public class TreasureChestDirt : Item
    {
        public TreasureChestDirt() : base(0x912)
        {
            Movable = false;

            Timer.DelayCall(TimeSpan.FromMinutes(2.0), Delete);
        }

        public TreasureChestDirt(Serial serial) : base(serial)
        {
        }

        public void Turn1()
        {
            ItemID = 0x913;
        }

        public void Turn2()
        {
            ItemID = 0x914;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            Delete();
        }
    }
}