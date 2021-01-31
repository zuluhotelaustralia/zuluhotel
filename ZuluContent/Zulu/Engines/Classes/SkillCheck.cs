using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Scripts.Zulu.Engines.Classes
{
    public static class SkillCheck
    {
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

        public static Dictionary<SkillName, SkillConfig> Configs { get; set; } =
            new Dictionary<SkillName, SkillConfig>
            {
                [SkillName.Alchemy] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 100, PointsAmount = 4, PointsSides = 5, PointsBonus = 10},
                    DefaultPoints = 200
                },
                [SkillName.Anatomy] = new SkillConfig
                {
                    Delay = TimeSpan.FromSeconds(10.0),
                    IntAdvancement = new StatAdvancement
                        {Chance = 80, PointsAmount = 4, PointsSides = 4, PointsBonus = 5},
                    DefaultPoints = 200
                }
            };

        public static bool SkillAsPercentSkillCheck(Mobile from, SkillName skillName, int points)
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
            if (skill.Lock == SkillLock.Locked || points == 0 || from is BaseCreature)
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
                if (current > 60000)
                    current = 60000;

                // TODO: Add cmd level check
                if (current > 1350)
                    current = 1350;

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
                ++current;
                toIncrease = GetNeededRawPointsToIncrease(current);
            }

            if (Utility.Random((int) toIncrease) < rawPoints)
                ++current;

            if (current != initial)
            {
                if (current > 60000)
                    current = 60000;

                // TODO: Add cmd level check
                if (current > 1350)
                    current = 1350;

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