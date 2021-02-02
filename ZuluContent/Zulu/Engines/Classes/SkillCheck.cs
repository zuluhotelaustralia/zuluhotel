using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Scripts.Zulu.Engines.Classes
{
    public static class SkillCheck
    {
        public const int MaxStatCap = 60000;
        public const int StatCap = 1350;

        public record StatAdvancement
        {
            public int Chance { get; init; }
            public uint PointsAmount { get; init; }
            public uint PointsSides { get; init; }
            public int PointsBonus { get; init; }
        }

        public record SkillConfig
        {
            public TimeSpan Delay { get; init; }
            public StatAdvancement StrAdvancement { get; init; }
            public StatAdvancement DexAdvancement { get; init; }
            public StatAdvancement IntAdvancement { get; init; }
            public int DefaultPoints { get; init; }
        }

        public static readonly IReadOnlyDictionary<SkillName, SkillConfig> Configs =
            new Dictionary<SkillName, SkillConfig>
            {
                // Warrior
                [SkillName.Anatomy] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 200
                },
                [SkillName.Fencing] = new SkillConfig
                {
                    StrAdvancement = new StatAdvancement
                        {Chance = 40, PointsAmount = 4, PointsSides = 4, PointsBonus = 0},
                    DexAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 4, PointsSides = 6, PointsBonus = 0},
                    DefaultPoints = 20
                },
                [SkillName.Healing] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 4, PointsSides = 4, PointsBonus = 10},
                    DexAdvancement = new StatAdvancement
                        {Chance = 30, PointsAmount = 3, PointsSides = 4, PointsBonus = 0},
                    DefaultPoints = 100
                },
                [SkillName.Macing] = new SkillConfig
                {
                    StrAdvancement = new StatAdvancement
                        {Chance = 65, PointsAmount = 4, PointsSides = 7, PointsBonus = 3},
                    DefaultPoints = 20
                },
                [SkillName.Parry] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 1, PointsSides = 4, PointsBonus = 0},
                    DexAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 2, PointsSides = 3, PointsBonus = 0},
                    DefaultPoints = 20
                },
                [SkillName.Swords] = new SkillConfig
                {
                    StrAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 4, PointsSides = 6, PointsBonus = 0},
                    DexAdvancement = new StatAdvancement
                        {Chance = 40, PointsAmount = 4, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 20
                },
                [SkillName.Tactics] = new SkillConfig
                {
                    DexAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 2, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 15
                },
                [SkillName.Wrestling] = new SkillConfig
                {
                    StrAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 4, PointsSides = 7, PointsBonus = 4},
                    DexAdvancement = new StatAdvancement
                        {Chance = 30, PointsAmount = 7, PointsSides = 8, PointsBonus = 0},
                    DefaultPoints = 20
                },

                // Mage
                [SkillName.Alchemy] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 100, PointsAmount = 4, PointsSides = 5, PointsBonus = 10},
                    DefaultPoints = 200
                },
                [SkillName.EvalInt] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 90, PointsAmount = 5, PointsSides = 6, PointsBonus = 20},
                    DefaultPoints = 200
                },
                [SkillName.Inscribe] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 4, PointsSides = 6, PointsBonus = 10},
                    DefaultPoints = 600
                },
                [SkillName.ItemID] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(4.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 5, PointsSides = 5, PointsBonus = 15},
                    DefaultPoints = 200
                },
                [SkillName.Magery] = new SkillConfig
                {
                    IntAdvancement = new StatAdvancement
                        {Chance = 100, PointsAmount = 5, PointsSides = 8, PointsBonus = 18},
                    DefaultPoints = 300
                },
                [SkillName.MagicResist] = new SkillConfig
                {
                    IntAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 3, PointsSides = 4, PointsBonus = 15},
                    DefaultPoints = 50
                },
                [SkillName.Meditation] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 5, PointsSides = 5, PointsBonus = 15},
                    DefaultPoints = 250
                },
                [SkillName.SpiritSpeak] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 100, PointsAmount = 5, PointsSides = 8, PointsBonus = 35},
                    DefaultPoints = 20
                },

                // Ranger
                [SkillName.AnimalLore] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(5.0),
                    DefaultPoints = 200
                },
                [SkillName.AnimalTaming] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 4, PointsBonus = 10},
                    DefaultPoints = 25
                },
                [SkillName.Archery] = new SkillConfig
                {
                    StrAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 4, PointsSides = 4, PointsBonus = 0},
                    DexAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 4, PointsSides = 6, PointsBonus = 8},
                    DefaultPoints = 20
                },
                [SkillName.Camping] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 15, PointsAmount = 1, PointsSides = 8, PointsBonus = 0},
                    IntAdvancement = new StatAdvancement
                        {Chance = 10, PointsAmount = 1, PointsSides = 6, PointsBonus = 0},
                    DefaultPoints = 200
                },
                [SkillName.Cooking] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 3, PointsSides = 4, PointsBonus = 0},
                    IntAdvancement = new StatAdvancement
                        {Chance = 30, PointsAmount = 3, PointsSides = 4, PointsBonus = 0},
                    DefaultPoints = 300
                },
                [SkillName.Fishing] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(5.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 4, PointsBonus = 8},
                    DefaultPoints = 200
                },
                [SkillName.Tracking] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 4, PointsSides = 4, PointsBonus = 15},
                    IntAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 4, PointsSides = 2, PointsBonus = 5},
                    DefaultPoints = 50
                },
                [SkillName.Veterinary] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 20, PointsAmount = 3, PointsSides = 4, PointsBonus = 0},
                    DefaultPoints = 100
                },

                // Crafter
                [SkillName.ArmsLore] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 4, PointsSides = 4, PointsBonus = 5},
                    StrAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 3, PointsSides = 4, PointsBonus = 0},
                    DefaultPoints = 200
                },
                [SkillName.Blacksmith] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 6, PointsBonus = 10},
                    DefaultPoints = 30
                },
                [SkillName.Carpentry] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 3, PointsSides = 6, PointsBonus = 5},
                    DefaultPoints = 100
                },
                [SkillName.Fletching] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 3, PointsSides = 4, PointsBonus = 8},
                    DexAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 3, PointsSides = 6, PointsBonus = 8},
                    DefaultPoints = 100
                },
                [SkillName.Lumberjacking] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 6, PointsBonus = 0},
                    DefaultPoints = 100
                },
                [SkillName.Mining] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 3, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 25
                },
                [SkillName.Tailoring] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 3, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 50
                },
                [SkillName.Tinkering] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    StrAdvancement = new StatAdvancement
                        {Chance = 10, PointsAmount = 2, PointsSides = 4, PointsBonus = 10},
                    DexAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 4, PointsSides = 6, PointsBonus = 10},
                    DefaultPoints = 100
                },

                // Bard
                [SkillName.Begging] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(20.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 200
                },
                [SkillName.Cartography] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 3, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 200
                },
                [SkillName.Discordance] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DefaultPoints = 200
                },
                [SkillName.Herding] = new SkillConfig
                {
                    DefaultPoints = 100
                },
                [SkillName.Musicianship] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(15.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 3, PointsSides = 4, PointsBonus = 0},
                    DexAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 3, PointsSides = 5, PointsBonus = 8},
                    DefaultPoints = 20
                },
                [SkillName.Peacemaking] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 15, PointsAmount = 1, PointsSides = 8, PointsBonus = 0},
                    DefaultPoints = 200
                },
                [SkillName.Provocation] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 50, PointsAmount = 4, PointsSides = 10, PointsBonus = 0},
                    DefaultPoints = 20
                },
                [SkillName.TasteID] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 85, PointsAmount = 4, PointsSides = 4, PointsBonus = 15},
                    DefaultPoints = 200
                },

                // Thief
                [SkillName.Hiding] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 70, PointsAmount = 4, PointsSides = 6, PointsBonus = 10},
                    DefaultPoints = 200
                },
                [SkillName.Lockpicking] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 3, PointsSides = 4, PointsBonus = 12},
                    DefaultPoints = 100
                },
                [SkillName.Poisoning] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 5, PointsSides = 6, PointsBonus = 40},
                    IntAdvancement = new StatAdvancement
                        {Chance = 60, PointsAmount = 5, PointsSides = 4, PointsBonus = 30},
                    DefaultPoints = 200
                },
                [SkillName.RemoveTrap] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 90, PointsAmount = 5, PointsSides = 8, PointsBonus = 15},
                    DefaultPoints = 200
                },
                [SkillName.Snooping] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(5.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 6, PointsBonus = 10},
                    DefaultPoints = 200
                },
                [SkillName.Stealing] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(15.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 90, PointsAmount = 3, PointsSides = 4, PointsBonus = 9},
                    DefaultPoints = 200
                },
                [SkillName.Stealth] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 100, PointsAmount = 6, PointsSides = 6, PointsBonus = 45},
                    DefaultPoints = 200
                },
                [SkillName.DetectHidden] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    DexAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 3, PointsSides = 5, PointsBonus = 10},
                    IntAdvancement = new StatAdvancement
                        {Chance = 40, PointsAmount = 3, PointsSides = 6, PointsBonus = 0},
                    DefaultPoints = 200
                },

                // Misc
                [SkillName.Forensics] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(15.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 90, PointsAmount = 4, PointsSides = 4, PointsBonus = 12},
                    DefaultPoints = 200
                },
            };

        public static bool DifficultySkillCheck(Mobile from, SkillName skillName, int difficulty, int points)
        {
            Skill skill = from.Skills[skillName];

            var skillValue = (int) skill.Value;

            if (skillValue == 0 && from is BaseCreature)
                return false;

            var chance = skillValue - difficulty + 20;

            // In case skill arrow is down
            if (chance < 0)
            {
                AwardSkillPoints(from, skillName, 0);
                return false;
            }

            if (chance == 0)
                chance = 2;
            else
            {
                chance = (int) (chance * 2.5);
                if (chance > 98)
                    chance = 98;
            }

            var check = Utility.Random(100);
            if (check >= chance)
            {
                if ((from as IZuluClassed)?.ZuluClass.IsSkillInClass(skillName) ?? false)
                {
                    check = Utility.Random(100);
                    // In case they have the skill arrow down
                    if (check >= chance)
                    {
                        AwardSkillPoints(from, skillName, 0);
                        return false;
                    }

                    points = 0;
                }
                else
                {
                    // In case they have the skill arrow down
                    AwardSkillPoints(from, skillName, 0);
                    return false;
                }
            }

            if (from is PlayerMobile)
            {
                points = (int) (points * (1.0 - ((chance) / 100.0)));
                AwardSkillPoints(from, skillName, points);
            }

            return true;
        }

        public static bool PercentSkillCheck(Mobile from, SkillName skillName, int points)
        {
            Skill skill = from.Skills[skillName];

            var chance = (int) skill.Value;

            if (chance == 0 && from is BaseCreature)
                return false;

            if (chance < 2)
                chance = 2;
            else if (chance > 98)
                chance = 98;

            var check = Utility.Random(100);
            if (check >= chance)
            {
                if ((from as IZuluClassed)?.ZuluClass.IsSkillInClass(skillName) ?? false)
                {
                    check = Utility.Random(100);
                    if (check >= chance)
                    {
                        if (chance < 10)
                        {
                            AwardSkillPoints(from, skillName, points / 2);
                        }

                        return false;
                    }

                    points = 0;
                }
                else
                {
                    if (chance < 10)
                    {
                        AwardSkillPoints(from, skillName, points / 2);
                    }

                    return false;
                }
            }

            if (from is PlayerMobile)
            {
                AwardSkillPoints(from, skillName, points);
            }

            return true;
        }

        public static void AwardSkillPoints(Mobile from, SkillName skillName, int points)
        {
            Skill skill = from.Skills[skillName];

            // TODO: Add cmd level check
            if (skill.Lock == SkillLock.Locked || from is BaseCreature)
                return;

            if (skill.Lock == SkillLock.Down)
            {
                var baseSkill = (int) (skill.Base);
                var amount = 1;
                if (amount > baseSkill)
                {
                    amount = baseSkill;
                    skill.SetLockNoRelay(SkillLock.Locked);
                    skill.Update();
                }

                skill.BaseFixedPoint -= amount * 10;
                return;
            }

            if (points == 0)
                return;

            points = (int) (points * GetSkillPointsMultiplier(from as PlayerMobile, skillName));

            // TODO: Should we implement global multipliers?

            AwardPoints(from as PlayerMobile, skillName, points);
        }

        public static double GetSkillPointsMultiplier(PlayerMobile from, SkillName skillName)
        {
            if (from.ZuluClass.IsSkillInClass(skillName))
                return from.ZuluClass.Bonus;
            return 1.0;
        }

        public static void AwardPoints(PlayerMobile from, SkillName skillName, int points)
        {
            var config = Configs[skillName];

            Skill skill = from.Skills[skillName];

            AwardRawAttributePoints(from, skill, points);

            // Check the strength advancement
            if (config.StrAdvancement != null && Utility.Random(1000) < config.StrAdvancement.Chance * 10)
            {
                AwardRawAttributePoints(from, StatType.Str,
                    Utility.Dice(config.StrAdvancement.PointsAmount, config.StrAdvancement.PointsSides,
                        config.StrAdvancement.PointsBonus));
            }

            // Check the dexterity advancement
            if (config.DexAdvancement != null && Utility.Random(1000) < config.DexAdvancement.Chance * 10)
            {
                AwardRawAttributePoints(from, StatType.Dex,
                    Utility.Dice(config.DexAdvancement.PointsAmount, config.DexAdvancement.PointsSides,
                        config.DexAdvancement.PointsBonus));
            }

            // Check intelligence advancement
            if (config.IntAdvancement != null && Utility.Random(1000) < config.IntAdvancement.Chance * 10)
            {
                AwardRawAttributePoints(from, StatType.Int,
                    Utility.Dice(config.IntAdvancement.PointsAmount, config.IntAdvancement.PointsSides,
                        config.IntAdvancement.PointsBonus));
            }
        }

        public static void AwardRawAttributePoints(PlayerMobile from, Skill skill, double rawPoints)
        {
            if (rawPoints == 0.0)
                return;

            var initial = (int) (skill.Base * 10.0);
            var current = initial;

            var toIncrease = GetNeededRawPointsToIncrease(current);

            while (rawPoints >= toIncrease)
            {
                rawPoints -= toIncrease;
                ++current;
                toIncrease = GetNeededRawPointsToIncrease(current);
            }

            if (Utility.Random((int) toIncrease) < rawPoints)
                ++current;

            if (current != initial)
            {
                if (current > MaxStatCap)
                    current = MaxStatCap;

                // TODO: Add cmd level check
                if (current > StatCap)
                    current = StatCap;

                skill.BaseFixedPoint = current;
            }
        }

        public static void AwardRawAttributePoints(PlayerMobile from, StatType stat, double rawPoints)
        {
            if (rawPoints == 0.0)
                return;

            var initial = 0;
            if (stat == StatType.Str)
                initial = from.RawStr * 10;
            else if (stat == StatType.Dex)
                initial = from.RawDex * 10;
            else if (stat == StatType.Int)
                initial = from.RawInt * 10;
            var current = initial;

            var toIncrease = GetNeededRawPointsToIncrease(current);

            while (rawPoints >= toIncrease)
            {
                rawPoints -= toIncrease;
                current += 10;
                toIncrease = GetNeededRawPointsToIncrease(current);
            }

            if (Utility.Random((int) toIncrease) < rawPoints)
                current += 10;

            if (current != initial)
            {
                if (current > MaxStatCap)
                    current = MaxStatCap;

                // TODO: Add cmd level check
                if (current > StatCap)
                    current = StatCap;

                if (stat == StatType.Str)
                    from.RawStr = current / 10;
                else if (stat == StatType.Dex)
                    from.RawDex = current / 10;
                else if (stat == StatType.Int)
                    from.RawInt = current / 10;
            }
        }

        public static double GetNeededRawPointsToIncrease(double baseValue)
        {
            if (baseValue < 200.0)
                return 20.48;

            if (baseValue >= 2800)
                return 0x7fffffff;

            return Math.Pow(2.0, (int) baseValue / 100) * 10.24;
        }
    }
}