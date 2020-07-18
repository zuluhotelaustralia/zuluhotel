using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;
using Server.Accounting;
using Server.Targeting;
using Server.Commands;
using Server.Mobiles;
using Server.Spells;
using Skills = Server.Skills;

namespace RunZH.Scripts.Zulu.Engines.Classes
{
    [PropertyObject]
    public class Spec
    {
        //reference original ZH Canada (ZH3) release
        private const double ClassPointsPerLevel = 120;
        private const double SkillBase = 480;
        private const double PercentPerLevel = 0.08;
        private const double PercentBase = 0.52;
        private const double PerLevel = 0.1; //10% per level
        private static double[] _minSkills;
        private static double[] _maxSkills;
        private readonly Mobile m_Parent; //store the parent obj

        public static readonly IReadOnlyDictionary<SpecName, SkillName[]> SpecSkills =
            new Dictionary<SpecName, SkillName[]>
            {
                {
                    SpecName.Warrior, new[]
                    {
                        SkillName.Wrestling,
                        SkillName.Tactics,
                        SkillName.Healing,
                        SkillName.Anatomy,
                        SkillName.Swords,
                        SkillName.Macing,
                        SkillName.Fencing,
                        SkillName.Parry,
                    }
                },
                {
                    SpecName.Ranger, new[]
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
                    }
                },
                {
                    SpecName.Mage, new[]
                    {
                        SkillName.Alchemy,
                        SkillName.ItemID,
                        SkillName.EvalInt,
                        SkillName.Inscribe,
                        SkillName.MagicResist,
                        SkillName.Meditation,
                        SkillName.Magery,
                        SkillName.SpiritSpeak,
                    }
                },
                {
                    SpecName.Crafter, new[]
                    {
                        SkillName.Tinkering,
                        SkillName.ArmsLore,
                        SkillName.Fletching,
                        SkillName.Tailoring,
                        SkillName.Mining,
                        SkillName.Lumberjacking,
                        SkillName.Carpentry,
                        SkillName.Blacksmith,
                    }
                },
                {
                    SpecName.Thief, new[]
                    {
                        SkillName.Hiding,
                        SkillName.Stealth,
                        SkillName.Stealing,
                        SkillName.DetectHidden,
                        SkillName.RemoveTrap,
                        SkillName.Poisoning,
                        SkillName.Lockpicking,
                        SkillName.Snooping,
                    }
                },
                {
                    SpecName.Bard, new[]
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
                }
            };

        //constructor needs a reference to the parent playermobile obj.
        public Spec(Mobile parent)
        {
            m_Parent = parent;
            SpecName = SpecName.None;
            SpecLevel = 0;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public int SpecLevel { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public SpecName SpecName { get; set; }

        //ZHS had this set to 20% per level, let's go with 10 or 5% for better balance
        [CommandProperty(AccessLevel.Counselor)]
        public double Bonus => SpecName == SpecName.Powerplayer || SpecName == SpecName.None
            ? 1.0
            : 1.0 + SpecLevel * PerLevel;

        public static double GetBonusFor(Mobile m, SpecName name) =>
            m is PlayerMobile mobile ? mobile.Spec.GetBonusFor(name) : 1.0;

        public double GetBonusFor(SpecName name) => SpecName == name ? Bonus : 1.0;

        public static void Initialize()
        {
            CommandSystem.Register("spec", AccessLevel.Player, Spec_OnCommand);
            CommandSystem.Register("showclasse", AccessLevel.Player, Spec_OnCommand);

            SetMaximums();
        }

        public static void Spec_OnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            if (pm.AccessLevel >= AccessLevel.Counselor)
            {
                pm.Target = new InternalTarget();
                return;
            }

            pm.Spec.ComputeSpec();

            var message = pm.Spec.SpecName == SpecName.None
                ? "You aren't a member of any particular Specialization."
                : $"You are a qualified Spec {pm.Spec.SpecLevel} {pm.Spec.SpecName.FriendlyName()}.";

            pm.SendMessage(message);
        }

        public void ComputeSpec()
        {
            if (m_Parent is BaseCreature)
                return;

            SpecName = SpecName.None;
            SpecLevel = 0;

            //minimum on-class skill points per level are 600 for level 1, then += 120 pts per level
            //maximum total skill points per level are 1000 for level 1, then ( 0.52 + 0.08(level) ) * minskill
            // if you fail the maxskill check then you are still eligible for the next spec down.
            // average on-class skill must be satisfied for spec.

            Skills skills = m_Parent.Skills;
            double total = skills.Total;
            total *= 0.1;

            if (total < 600.0)
            {
                return;
            }

            if (total >= 3920.0)
            {
                SpecName = SpecName.Powerplayer;
                SpecLevel = 1;

                if (total >= 5145)
                {
                    SpecLevel = 2;

                    if (total >= 6370)
                    {
                        SpecLevel = 3;
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

            foreach (var (spec, specSkills) in SpecSkills)
            {
                var specTotal = specSkills.Select(s => skills[s].Value).Sum();
                var level = GetSpecLevel(specTotal, SpecName.Thief);

                if (level > 0)
                {
                    SpecName = spec;
                    SpecLevel = level;
                }
            }

            if (SpecLevel > maxLevel)
            {
                SpecLevel = maxLevel;
            }

            if (SpecLevel <= 0)
            {
                SpecLevel = 0;
                SpecName = SpecName.None;
            }
        }

        private double AvgSkill(double onSpec, SpecName sn)
        {
            if (sn == SpecName.Ranger)
            {
                return onSpec * 8 / 9;
            }
            else
            {
                return onSpec;
            }
        }

        //idx:    0    1     2     3     4     5      6
        //Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
        //Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
        private int GetSpecLevel(double onSpec, SpecName sn)
        {
            double averaged = AvgSkill(onSpec, sn);

            if (averaged >= _minSkills[6] && averaged <= _maxSkills[6])
            {
                return 6;
            }

            if (averaged >= _minSkills[5] && averaged <= _maxSkills[5])
            {
                return 5;
            }

            if (averaged >= _minSkills[4] && averaged <= _maxSkills[4])
            {
                return 4;
            }

            if (averaged >= _minSkills[3] && averaged <= _maxSkills[3])
            {
                return 3;
            }

            if (averaged >= _minSkills[2] && averaged <= _maxSkills[2])
            {
                return 2;
            }

            if (averaged >= _minSkills[1] && averaged <= _maxSkills[1])
            {
                return 1;
            }

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
                _minSkills[i] = SkillBase + ClassPointsPerLevel * (double) i;
            }

            _maxSkills = new double[7];

            for (int i = 0; i < 7; i++)
            {
                _maxSkills[i] = Math.Floor(_minSkills[i] / (PercentBase + PercentPerLevel * (double) i));
            }

            Console.WriteLine("Min: [ {0}, {1}, {2}, {3}, {4}, {5}, {6} ]", _minSkills[0], _minSkills[1],
                _minSkills[2], _minSkills[3], _minSkills[4], _minSkills[5], _minSkills[6]);
            Console.WriteLine("Max: [ {0}, {1}, {2}, {3}, {4}, {5}, {6} ]", _maxSkills[0], _maxSkills[1],
                _maxSkills[2], _maxSkills[3], _maxSkills[4], _maxSkills[5], _minSkills[6]);
        }

        public bool IsSkillOnSpec(SkillName sn)
        {
            return SpecSkills.FirstOrDefault(kv => kv.Value.Contains(sn)).Key == SpecName;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(12, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                from.Spec.ComputeSpec();
                from.SendMessage("{0}: {1}, level {2}", from.Name, from.Spec.SpecName, from.Spec.SpecLevel);
            }
        }
    }
}