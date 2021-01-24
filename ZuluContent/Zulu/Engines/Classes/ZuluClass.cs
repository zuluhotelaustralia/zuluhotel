using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Scripts.Zulu.Engines.Classes
{
    [PropertyObject]
    public class ZuluClass
    {
        //reference original ZH Canada (ZH3) release
        private const double ClassPointsPerLevel = 120;
        private const double SkillBase = 480;
        private const double PercentPerLevel = 0.08;
        private const double PercentBase = 0.52;
        private const double PerLevel = 0.1; //10% per level
        private static double[] _minSkills;
        private static double[] _maxSkills;

        private readonly IZuluClassed m_Parent; //store the parent obj

        [CommandProperty(AccessLevel.Counselor)]
        public int Level { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public ZuluClassType Type { get; set; }

        public static readonly IReadOnlyDictionary<ZuluClassType, SkillName[]> ClassSkills =
            new Dictionary<ZuluClassType, SkillName[]>
            {
                [ZuluClassType.Warrior] = new[]
                {
                    SkillName.Wrestling,
                    SkillName.Tactics,
                    SkillName.Healing,
                    SkillName.Anatomy,
                    SkillName.Swords,
                    SkillName.Macing,
                    SkillName.Fencing,
                    SkillName.Parry,
                },
                [ZuluClassType.Ranger] = new[]
                {
                    SkillName.Tracking,
                    SkillName.Archery,
                    SkillName.AnimalLore,
                    SkillName.Veterinary,
                    SkillName.AnimalTaming,
                    SkillName.Fishing,
                    SkillName.Camping,
                    SkillName.Cooking,
                    SkillName.Tactics,
                },
                [ZuluClassType.Mage] = new[]
                {
                    SkillName.Alchemy,
                    SkillName.ItemID,
                    SkillName.EvalInt,
                    SkillName.Inscribe,
                    SkillName.MagicResist,
                    SkillName.Meditation,
                    SkillName.Magery,
                    SkillName.SpiritSpeak,
                },
                [ZuluClassType.Crafter] = new[]
                {
                    SkillName.Tinkering,
                    SkillName.ArmsLore,
                    SkillName.Fletching,
                    SkillName.Tailoring,
                    SkillName.Mining,
                    SkillName.Lumberjacking,
                    SkillName.Carpentry,
                    SkillName.Blacksmith,
                },
                [ZuluClassType.Thief] = new[]
                {
                    SkillName.Hiding,
                    SkillName.Stealth,
                    SkillName.Stealing,
                    SkillName.DetectHidden,
                    SkillName.RemoveTrap,
                    SkillName.Poisoning,
                    SkillName.Lockpicking,
                    SkillName.Snooping,
                },
                [ZuluClassType.Thief] = new[]
                {
                    SkillName.Provocation,
                    SkillName.Musicianship,
                    SkillName.Herding,
                    SkillName.Discordance,
                    SkillName.TasteID,
                    SkillName.Peacemaking,
                    SkillName.Cartography,
                    SkillName.Begging,
                }
            };

        //constructor needs a reference to the parent playermobile obj.
        public ZuluClass(IZuluClassed parent)
        {
            m_Parent = parent;
            Type = ZuluClassType.None;
            Level = 0;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public double Bonus => Type == ZuluClassType.Powerplayer || Type == ZuluClassType.None
            ? 1.0
            : 1.0 + Level * PerLevel;

        public static double GetBonusFor(Mobile m, ZuluClassType name) =>
            m is PlayerMobile mobile ? mobile.ZuluClass.GetBonusFor(name) : 1.0;

        public double GetBonusFor(ZuluClassType name) => Type == name ? Bonus : 1.0;

        public static void Initialize()
        {
            CommandSystem.Register("class", AccessLevel.Player, ClassOnCommand);
            CommandSystem.Register("showclasse", AccessLevel.Player, ClassOnCommand);

            SetMaximums();
        }

        public static void ClassOnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            if (pm.AccessLevel >= AccessLevel.Counselor)
            {
                pm.Target = new InternalTarget();
                return;
            }

            pm.ZuluClass.ComputeClass();

            var message = pm.ZuluClass.Type == ZuluClassType.None
                ? "You aren't a member of any particular class."
                : $"You are a qualified level {pm.ZuluClass.Level} {pm.ZuluClass.Type.FriendlyName()}.";

            pm.SendMessage(message);
        }

        public static ZuluClass GetClass(Mobile m) => m is IZuluClassed classed ? classed.ZuluClass : null;

        public void ComputeClass()
        {
            if (m_Parent is BaseCreature)
                return;

            Type = ZuluClassType.None;
            Level = 0;

            //minimum on-class skill points per level are 600 for level 1, then += 120 pts per level
            //maximum total skill points per level are 1000 for level 1, then ( 0.52 + 0.08(level) ) * minskill
            // if you fail the maxskill check then you are still eligible for the next zuluClass down.
            // average on-class skill must be satisfied for zuluClass.

            var skills = m_Parent.Skills;
            double total = skills.Total;
            total *= 0.1;

            if (total < 600.0)
            {
                return;
            }

            if (total >= 3920.0)
            {
                Type = ZuluClassType.Powerplayer;
                Level = 1;

                if (total >= 5145)
                {
                    Level = 2;

                    if (total >= 6370)
                    {
                        Level = 3;
                    }
                }

                //we're a pp so:
                return;
            }

            int maxLevel = GetMaxLevel(total);
            if (maxLevel > 6)
            {
                maxLevel = 6;
            }

            foreach (var (spec, specSkills) in ClassSkills)
            {
                var specTotal = specSkills.Select(s => skills[s].Value).Sum();
                var level = GetSpecLevel(specTotal);

                if (level > 0)
                {
                    Type = spec;
                    Level = level;
                }
            }

            if (Level > maxLevel)
            {
                Level = maxLevel;
            }

            if (Level <= 0)
            {
                Level = 0;
                Type = ZuluClassType.None;
            }
        }

        //idx:    0    1     2     3     4     5      6
        //Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
        //Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
        private int GetSpecLevel(double totalSkills)
        {
            if (totalSkills >= _minSkills[6] && totalSkills <= _maxSkills[6])
                return 6;

            if (totalSkills >= _minSkills[5] && totalSkills <= _maxSkills[5])
                return 5;

            if (totalSkills >= _minSkills[4] && totalSkills <= _maxSkills[4])
                return 4;

            if (totalSkills >= _minSkills[3] && totalSkills <= _maxSkills[3])
                return 3;

            if (totalSkills >= _minSkills[2] && totalSkills <= _maxSkills[2])
                return 2;

            if (totalSkills >= _minSkills[1] && totalSkills <= _maxSkills[1])
                return 1;

            return 0;
        }

        private int GetMaxLevel(double total)
        {
            int num = _maxSkills.Length;
            num--;

            for (int i = num; i >= 0; i--)
            {
                if (_maxSkills[i] >= total)
                {
                    return i;
                }
            }

            return 0;
        }

        private static void SetMaximums()
        {
            // max skill = minskill * (0.52 + 0.08*level)

            _minSkills = new double[7];

            for (int i = 0; i < 7; i++)
            {
                _minSkills[i] = SkillBase + ClassPointsPerLevel * i;
            }

            _maxSkills = new double[7];

            for (int i = 0; i < 7; i++)
            {
                _maxSkills[i] = Math.Floor(_minSkills[i] / (PercentBase + PercentPerLevel * i));
            }

            // Console.WriteLine(
            //     $"Min: [ {_minSkills[0]}, {_minSkills[1]}, {_minSkills[2]}, {_minSkills[3]}, {_minSkills[4]}, {_minSkills[5]}, {_minSkills[6]} ]");
            // Console.WriteLine(
            //     $"Max: [ {_maxSkills[0]}, {_maxSkills[1]}, {_maxSkills[2]}, {_maxSkills[3]}, {_maxSkills[4]}, {_maxSkills[5]}, {_minSkills[6]} ]");
        }

        public bool IsSkillOnSpec(SkillName sn)
        {
            return ClassSkills.FirstOrDefault(kv => kv.Value.Contains(sn)).Key == Type;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(12, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (from is IZuluClassed classed)
                {
                    classed.ZuluClass.ComputeClass();
                    from.SendMessage("{0}: {1}, level {2}", from.Name, classed.ZuluClass?.Type, classed.ZuluClass?.Level);
                }
            }
        }
    }
}