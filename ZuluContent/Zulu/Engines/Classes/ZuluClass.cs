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
        private const int MaxLevel = 7;

        private static readonly double[] MinSkills =
            Enumerable
                .Range(0, MaxLevel)
                .Select(i => SkillBase + ClassPointsPerLevel * i)
                .ToArray();
        
        private static readonly double[] MaxSkills = 
            Enumerable
                .Range(0, MaxLevel)
                .Select(i =>  Math.Floor(MinSkills[i] / (PercentBase + PercentPerLevel * i)))
                .ToArray();
       
        private readonly IZuluClassed m_Parent;
        
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
                [ZuluClassType.Bard] = new[]
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

        [CommandProperty(AccessLevel.Counselor)]
        public int Level { get; set; } = 0;

        [CommandProperty(AccessLevel.Counselor)]
        public ZuluClassType Type { get; set; } = ZuluClassType.None;
        
        public ZuluClass(IZuluClassed parent)
        {
            m_Parent = parent;
            ComputeClass();
        }

        [CommandProperty(AccessLevel.Counselor)]
        public double Bonus => Type == ZuluClassType.Powerplayer || Type == ZuluClassType.None
            ? 1.0
            : 1.0 + Level * PerLevel;

        public static double GetBonusFor(Mobile m, ZuluClassType name) =>
            m is IZuluClassed classed ? classed.ZuluClass.GetBonusFor(name) : 1.0;

        public double GetBonusFor(ZuluClassType name) => Type == name ? Bonus : 1.0;

        public static void Initialize()
        {
            CommandSystem.Register("class", AccessLevel.Player, ClassOnCommand);
            CommandSystem.Register("showclasse", AccessLevel.Player, ClassOnCommand);
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

            switch (total)
            {
                case < 600.0:
                    Level = 0;
                    Type = ZuluClassType.None;
                    return;
                case >= 3920.0:
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
            }


            foreach (var (spec, specSkills) in ClassSkills)
            {
                var specTotal = specSkills.Select(s => skills[s].Value).Sum();
                var level = GetClassLevel(specTotal);

                if (level > 0)
                {
                    Type = spec;
                    Level = level;
                }
            }

            if (Level > MaxLevel) 
                Level = MaxLevel;

            if (Level <= 0)
            {
                Level = 0;
                Type = ZuluClassType.None;
            }
        }

        //idx:    0    1     2     3     4     5      6
        //Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
        //Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
        private int GetClassLevel(double totalSkills)
        {
            for (int i = 0; i < MaxLevel; i++)
            {
                if (totalSkills >= MinSkills[i] && totalSkills <= MaxSkills[i])
                    return i;
            }

            return 0;
        }
        
        public bool IsSkillInClass(SkillName sn)
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