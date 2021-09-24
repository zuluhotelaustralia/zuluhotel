using System;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class DeceitBrazier : Item
    {
        public static readonly string[] CreatureTemplates =
        {
            #region Undead

            "Skeleton", "Mummy",
            "BoneKnight", "Liche", "LicheLord",
            "Wraith", "Shade", "Spectre", "Zombie",

            #endregion

            #region Demons

            "Balron", "Daemon", "Imp",
            "Mongbat", "IceFiend", "Gargoyle", "StoneGargoyle",

            #endregion

            #region Gazers

            "Gazer",

            #endregion

            #region Uncategorized

            "Harpy", "HeadlessOne", "HellHound",
            "HellCat", "Phoenix", "LavaLizard",
            "PredatorHellCat", "Wisp",

            #endregion

            #region Arachnid

            "PhaseSpider", "GiantFrostSpider", "GiantScorpion",

            #endregion

            #region Repond

            "Cyclops", "Ettin", "EvilMage",
            "TrollLord", "OgreLord", "OrcCaptain",
            "OrcishLord", "OrcMasterMage", "Ratlord",
            "RatmanMarksman", "OrcCaptain", "Troll", "Titan",
            "EvilMage",

            #endregion

            #region Reptilian

            "Dragon", "Drake", "Snake",
            "IceSerpent", "GiantSerpent", "IceSnake", "LavaSerpent",
            "LizardmanKing", "Wyvern", "PoisonWyrm",
            "SilverSerpent", "LavaSnake",

            #endregion

            #region Elementals

            "EarthElemental", "PoisonElemental", "FireElemental",
            "IceElemental", "WaterElemental",
            "AirElemental",

            #endregion

            #region Random Critters

            "Sewerrat", "GiantRat", "DireWolf", "TimberWolf",
            "Cougar", "Alligator"

            #endregion
        };

        private Timer m_Timer;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextSpawn { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SpawnRange { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextSpawnDelay { get; set; }

        public override int LabelNumber => 1023633; // Brazier


        [Constructible]
        public DeceitBrazier() : base(0xE31)
        {
            Movable = false;
            Light = LightType.Circle225;
            NextSpawn = DateTime.Now;
            NextSpawnDelay = TimeSpan.FromMinutes(15.0);
            SpawnRange = 5;
        }

        [Constructible]
        public DeceitBrazier(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write((int) SpawnRange);
            writer.Write(NextSpawnDelay);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            if (version >= 0)
            {
                SpawnRange = reader.ReadInt();
                NextSpawnDelay = reader.ReadTimeSpan();
            }

            NextSpawn = DateTime.Now;
        }

        public virtual void HeedWarning()
        {
            PublicOverheadMessage(MessageType.Regular, 0x3B2,
                500761); // Heed this warning well, and use this brazier at your own peril.
        }

        public override bool HandlesOnMovement
        {
            get { return true; }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (NextSpawn < DateTime.Now) // means we haven't spawned anything if the next spawn is below
            {
                if (Utility.InRange(m.Location, Location, 1) && !Utility.InRange(oldLocation, Location, 1) &&
                    m.Player && !(m.AccessLevel > AccessLevel.Player || m.Hidden))
                {
                    if (m_Timer == null || !m_Timer.Running)
                        m_Timer = Timer.DelayCall(TimeSpan.FromSeconds(2), HeedWarning);
                }
            }

            base.OnMovement(m, oldLocation);
        }

        public Point3D GetSpawnPosition()
        {
            var map = Map;

            if (map == null)
                return Location;

            // Try 10 times to find a Spawnable location.
            for (var i = 0; i < 10; i++)
            {
                var x = Location.X + (Utility.Random(SpawnRange * 2 + 1) - SpawnRange);
                var y = Location.Y + (Utility.Random(SpawnRange * 2 + 1) - SpawnRange);
                var z = Map.GetAverageZ(x, y);

                if (Map.CanSpawnMobile(new Point2D(x, y), Z))
                    return new Point3D(x, y, Z);
                else if (Map.CanSpawnMobile(new Point2D(x, y), z))
                    return new Point3D(x, y, z);
            }

            return Location;
        }

        public virtual void DoEffect(Point3D loc, Map map)
        {
            Effects.SendLocationParticles(EffectItem.Create(loc, map, EffectItem.DefaultDuration), 0x3709, 10, 30,
                5052);
            Effects.PlaySound(loc, map, 0x225);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Utility.InRange(from.Location, Location, 2))
            {
                try
                {
                    if (NextSpawn < DateTime.Now)
                    {
                        var map = Map;
                        BaseCreature bc = Creatures.Create(CreatureTemplates[Utility.Random(CreatureTemplates.Length)]);

                        if (bc != null)
                        {
                            var spawnLoc = GetSpawnPosition();

                            DoEffect(spawnLoc, map);

                            void Callback()
                            {
                                bc.Home = Location;
                                bc.RangeHome = SpawnRange;
                                bc.FightMode = FightMode.Closest;

                                bc.MoveToWorld(spawnLoc, map);

                                DoEffect(spawnLoc, map);

                                bc.ForceReacquire();
                            }

                            Timer.DelayCall(TimeSpan.FromSeconds(1), Callback);

                            NextSpawn = DateTime.Now + NextSpawnDelay;
                        }
                    }
                    else
                    {
                        // The brazier fizzes and pops, but nothing seems to happen.
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, 500760); 
                    }
                }
                catch
                {
                }
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away.
            }
        }
    }
}