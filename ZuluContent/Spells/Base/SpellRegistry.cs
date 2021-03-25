using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Scripts.Zulu.Spells.Necromancy;
using Server.Engines.Magic;
using Server.SkillHandlers;
using Server.Spells.Eighth;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.Third;
using Server.Targeting;

namespace Server.Spells
{
    public static class SpellRegistry
    {
        public static readonly Dictionary<Type, Func<Mobile, Item, Spell>> SpellCreators = new();
        public static readonly Dictionary<Type, SpellInfo> SpellInfos = new();
        public static readonly Dictionary<SpellEntry, Type> SpellTypes = new();

        public static void Register<TSpell>(SpellEntry spellEntry, Func<Mobile, Item, TSpell> creator, SpellInfo info)
            where TSpell : Spell
        {
            SpellCreators.TryAdd(typeof(TSpell), creator);
            SpellTypes.TryAdd(spellEntry, typeof(TSpell));
            SpellInfos.TryAdd(typeof(TSpell), info);
        }

        public static Spell Create(SpellEntry spellId, Mobile caster, Item scroll)
        {
            return !SpellTypes.TryGetValue(spellId, out var t) ? null : Create(t, caster, scroll);
        }

        public static Spell Create(string name, Mobile caster, Item scroll)
        {
            if (Enum.TryParse(typeof(SpellEntry), name, true, out var result) && result is SpellEntry entry)
                return Create(entry, caster, scroll);

            return null;
        }

        public static Spell Create(Type t, Mobile caster, Item scroll)
        {
            return SpellCreators.TryGetValue(t, out var creator) ? creator(caster, scroll) : null;
        }

        public static T Create<T>(Mobile caster, Item scroll) where T : Spell
        {
            return (T) Create(typeof(T), caster, scroll);
        }
        
        public static SpellInfo GetInfo(SpellEntry spellEntry)
        {
            return SpellInfos[SpellTypes[spellEntry]];
        }

        static SpellRegistry()
        {
            Register(
                SpellEntry.None,
                (caster, scroll) => (Spell) null,
                new SpellInfo()
            );

            Register(
                SpellEntry.SpiritSpeak,
                (caster, _) => new SpiritSpeak.SpiritSpeakSpell(caster),
                new SpellInfo
                {
                    Name = "Spirit Speak",
                    Mantra = string.Empty
                }
            );

            Register(
                SpellEntry.EtherealMount,
                (caster, _) => (Spell) null,
                new SpellInfo
                {
                    Name = "Ethereal Mount",
                    Mantra = string.Empty
                }
            );

            Register(
                SpellEntry.Clumsy,
                (caster, scroll) => new ClumsySpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Clumsy",
                    Mantra = "Uus Jux",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.CreateFood,
                (caster, scroll) => new CreateFoodSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Create Food",
                    Mantra = "In Mani Ylem",
                    Action = 224,
                    AllowTown = true,
                    LeftHandEffect = 9011,
                    RightHandEffect = 9011,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.MandrakeRoot] = 1
                    }
                }
            );


            Register(
                SpellEntry.Feeblemind,
                (caster, scroll) => new FeeblemindSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Feeblemind",
                    Mantra = "Rel Wis",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Ginseng] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Heal,
                (caster, scroll) => new HealSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Heal",
                    Mantra = "In Mani",
                    Action = 224,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial,
                    }
                }
            );


            Register(
                SpellEntry.MagicArrow,
                (caster, scroll) => new MagicArrowSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Magic Arrow",
                    Mantra = "In Por Ylem",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9041,
                    RightHandEffect = 9041,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.Fire,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.NightSight,
                (caster, scroll) => new NightSightSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Night Sight",
                    Mantra = "In Lor",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.SulfurousAsh] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.ReactiveArmor,
                (caster, scroll) => new ReactiveArmorSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Reactive Armor",
                    Mantra = "Flam Sanct",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9011,
                    RightHandEffect = 9011,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Weaken,
                (caster, scroll) => new WeakenSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Weaken",
                    Mantra = "Des Mani",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.First,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Agility,
                (caster, scroll) => new AgilitySpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Agility",
                    Mantra = "Ex Uus",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Cunning,
                (caster, scroll) => new CunningSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Cunning",
                    Mantra = "Uus Wis",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Cure,
                (caster, scroll) => new CureSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Cure",
                    Mantra = "An Nox",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Harm,
                (caster, scroll) => new HarmSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Harm",
                    Mantra = "An Mani",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9041,
                    RightHandEffect = 9041,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Nightshade] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.MagicTrap,
                (caster, scroll) => new MagicTrapSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Magic Trap",
                    Mantra = "In Jux",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9001,
                    RightHandEffect = 9001,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.RemoveTrap,
                (caster, scroll) => new RemoveTrapSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Remove Trap",
                    Mantra = "An Jux",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9001,
                    RightHandEffect = 9001,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Protection,
                (caster, scroll) => new ProtectionSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Protection",
                    Mantra = "Uus Sanct",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9011,
                    RightHandEffect = 9011,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Strength,
                (caster, scroll) => new StrengthSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Strength",
                    Mantra = "Uus Mani",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Second,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Bless,
                (caster, scroll) => new BlessSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Bless",
                    Mantra = "Rel Sanct",
                    Action = 203,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Fireball,
                (caster, scroll) => new FireballSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Fireball",
                    Mantra = "Vas Flam",
                    Action = 203,
                    AllowTown = true,
                    LeftHandEffect = 9041,
                    RightHandEffect = 9041,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.Fire,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.MagicLock,
                (caster, scroll) => new MagicLockSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Magic Lock",
                    Mantra = "An Por",
                    Action = 215,
                    AllowTown = true,
                    LeftHandEffect = 9001,
                    RightHandEffect = 9001,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Poison,
                (caster, scroll) => new PoisonSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Poison",
                    Mantra = "In Nox",
                    Action = 203,
                    AllowTown = true,
                    LeftHandEffect = 9051,
                    RightHandEffect = 9051,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.Poison,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Telekinesis,
                (caster, scroll) => new TelekinesisSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Telekinesis",
                    Mantra = "Ort Por Ylem",
                    Action = 203,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Teleport,
                (caster, scroll) => new TeleportSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Teleport",
                    Mantra = "Rel Por",
                    Action = 215,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Unlock,
                (caster, scroll) => new UnlockSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Unlock Spell",
                    Mantra = "Ex Por",
                    Action = 215,
                    AllowTown = true,
                    LeftHandEffect = 9001,
                    RightHandEffect = 9001,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    },
                    
                }
            );


            Register(
                SpellEntry.WallOfStone,
                (caster, scroll) => new WallOfStoneSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Wall of Stone",
                    Mantra = "In Sanct Ylem",
                    Action = 227,
                    AllowTown = false,
                    LeftHandEffect = 9011,
                    RightHandEffect = 9011,
                    Circle = SpellCircle.Third,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.Garlic] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.ArchCure,
                (caster, scroll) => new ArchCureSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Arch Cure",
                    Mantra = "Vas An Nox",
                    Action = 215,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.ArchProtection,
                (caster, scroll) => new ArchProtectionSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Arch Protection",
                    Mantra = "Vas Uus Sanct",
                    Action = 215,
                    AllowTown = true,
                    LeftHandEffect = 9011,
                    RightHandEffect = 9011,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Curse,
                (caster, scroll) => new CurseSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Curse",
                    Mantra = "Des Sanct",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Nightshade] = 1,
                        [Reagent.Garlic] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.FireField,
                (caster, scroll) => new FireFieldSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Fire Field",
                    Mantra = "In Flam Grav",
                    Action = 215,
                    AllowTown = false,
                    LeftHandEffect = 9041,
                    RightHandEffect = 9041,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.Fire,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.GreaterHeal,
                (caster, scroll) => new GreaterHealSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Greater Heal",
                    Mantra = "In Vas Mani",
                    Action = 204,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Lightning,
                (caster, scroll) => new LightningSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Lightning",
                    Mantra = "Por Ort Grav",
                    Action = 239,
                    AllowTown = true,
                    LeftHandEffect = 9021,
                    RightHandEffect = 9021,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.ManaDrain,
                (caster, scroll) => new ManaDrainSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Mana Drain",
                    Mantra = "Ort Rel",
                    Action = 215,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Recall,
                (caster, scroll) => new RecallSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Recall",
                    Mantra = "Kal Ort Por",
                    Action = 239,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Fourth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.BladeSpirits,
                (caster, scroll) => new BladeSpiritsSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Blade Spirits",
                    Mantra = "In Jux Hur Ylem",
                    Action = 266,
                    AllowTown = false,
                    LeftHandEffect = 9040,
                    RightHandEffect = 9040,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.DispelField,
                (caster, scroll) => new DispelFieldSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Dispel Field",
                    Mantra = "An Grav",
                    Action = 206,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1,
                        [Reagent.Garlic] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Incognito,
                (caster, scroll) => new IncognitoSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Incognito",
                    Mantra = "Kal In Ex",
                    Action = 206,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.Garlic] = 1,
                        [Reagent.Nightshade] = 1
                    }
                }
            );


            Register(
                SpellEntry.MagicReflect,
                (caster, scroll) => new MagicReflectSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Magic Reflection",
                    Mantra = "In Jux Sanct",
                    Action = 242,
                    AllowTown = true,
                    LeftHandEffect = 9012,
                    RightHandEffect = 9012,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    }
                }
            );


            Register(
                SpellEntry.MindBlast,
                (caster, scroll) => new MindBlastSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Mind Blast",
                    Mantra = "Por Corp Wis",
                    Action = 218,
                    AllowTown = true,
                    LeftHandEffect = 9032,
                    RightHandEffect = 9032,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.Nightshade] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Paralyze,
                (caster, scroll) => new ParalyzeSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Paralyze",
                    Mantra = "An Ex Por",
                    Action = 218,
                    AllowTown = true,
                    LeftHandEffect = 9012,
                    RightHandEffect = 9012,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.PoisonField,
                (caster, scroll) => new PoisonFieldSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Poison Field",
                    Mantra = "In Nox Grav",
                    Action = 230,
                    AllowTown = false,
                    LeftHandEffect = 9052,
                    RightHandEffect = 9052,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.Poison,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Nightshade] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.SummonCreature,
                (caster, scroll) => new SummonCreatureSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Summon Creature",
                    Mantra = "Kal Xen",
                    Action = 16,
                    AllowTown = false,
                    LeftHandEffect = 0,
                    RightHandEffect = 0,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    }
                }
            );


            Register(
                SpellEntry.Dispel,
                (caster, scroll) => new DispelSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Dispel",
                    Mantra = "An Ort",
                    Action = 218,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.EnergyBolt,
                (caster, scroll) => new EnergyBoltSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Energy Bolt",
                    Mantra = "Corp Por",
                    Action = 230,
                    AllowTown = true,
                    LeftHandEffect = 9022,
                    RightHandEffect = 9022,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Explosion,
                (caster, scroll) => new ExplosionSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Explosion",
                    Mantra = "Vas Ort Flam",
                    Action = 230,
                    AllowTown = true,
                    LeftHandEffect = 9041,
                    RightHandEffect = 9041,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.Fire,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Invisibility,
                (caster, scroll) => new InvisibilitySpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Invisibility",
                    Mantra = "An Lor Xen",
                    Action = 206,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.Mark,
                (caster, scroll) => new MarkSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Mark",
                    Mantra = "Kal Por Ylem",
                    Action = 218,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.MassCurse,
                (caster, scroll) => new MassCurseSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Mass Curse",
                    Mantra = "Vas Des Sanct",
                    Action = 218,
                    AllowTown = false,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.Nightshade] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.ParalyzeField,
                (caster, scroll) => new ParalyzeFieldSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Paralyze Field",
                    Mantra = "In Ex Grav",
                    Action = 230,
                    AllowTown = false,
                    LeftHandEffect = 9012,
                    RightHandEffect = 9012,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Reveal,
                (caster, scroll) => new RevealSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Reveal",
                    Mantra = "Wis Quas",
                    Action = 206,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Sixth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.ChainLightning,
                (caster, scroll) => new ChainLightningSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Chain Lightning",
                    Mantra = "Vas Ort Grav",
                    Action = 209,
                    AllowTown = false,
                    LeftHandEffect = 9022,
                    RightHandEffect = 9022,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.EnergyField,
                (caster, scroll) => new EnergyFieldSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Energy Field",
                    Mantra = "In Sanct Grav",
                    Action = 221,
                    AllowTown = false,
                    LeftHandEffect = 9022,
                    RightHandEffect = 9022,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.FlameStrike,
                (caster, scroll) => new FlameStrikeSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Flame Strike",
                    Mantra = "Kal Vas Flam",
                    Action = 245,
                    AllowTown = true,
                    LeftHandEffect = 9042,
                    RightHandEffect = 9042,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.Fire,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.GateTravel,
                (caster, scroll) => new GateTravelSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Gate Travel",
                    Mantra = "Vas Rel Por",
                    Action = 263,
                    AllowTown = true,
                    LeftHandEffect = 9032,
                    RightHandEffect = 9032,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.ManaVampire,
                (caster, scroll) => new ManaVampireSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Mana Vampire",
                    Mantra = "Ort Sanct",
                    Action = 221,
                    AllowTown = true,
                    LeftHandEffect = 9032,
                    RightHandEffect = 9032,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BlackPearl] = 1,
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.MassDispel,
                (caster, scroll) => new MassDispelSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Mass Dispel",
                    Mantra = "Vas An Ort",
                    Action = 263,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Garlic] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.BlackPearl] = 1,
                        [Reagent.SulfurousAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.MeteorSwarm,
                (caster, scroll) => new MeteorSwarmSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Meteor Swarm",
                    Mantra = "Flam Kal Des Ylem",
                    Action = 233,
                    AllowTown = false,
                    LeftHandEffect = 9042,
                    RightHandEffect = 9042,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1,
                        [Reagent.SpidersSilk] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Polymorph,
                (caster, scroll) => new PolymorphSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Polymorph",
                    Mantra = "Vas Ylem Rel",
                    Action = 221,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Seventh,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.MandrakeRoot] = 1
                    }
                }
            );


            Register(
                SpellEntry.Earthquake,
                (caster, scroll) => new EarthquakeSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Earthquake",
                    Mantra = "In Vas Por",
                    Action = 233,
                    AllowTown = false,
                    LeftHandEffect = 9012,
                    RightHandEffect = 9012,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.Earth,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.Ginseng] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SulfurousAsh] = 1
                    }
                }
            );


            Register(
                SpellEntry.EnergyVortex,
                (caster, scroll) => new EnergyVortexSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Energy Vortex",
                    Mantra = "Vas Corp Por",
                    Action = 260,
                    AllowTown = false,
                    LeftHandEffect = 9032,
                    RightHandEffect = 9032,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.BlackPearl] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.Nightshade] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Resurrection,
                (caster, scroll) => new ResurrectionSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Resurrection",
                    Mantra = "An Corp",
                    Action = 245,
                    AllowTown = true,
                    AllowDead = true,
                    LeftHandEffect = 9062,
                    RightHandEffect = 9062,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.Garlic] = 1,
                        [Reagent.Ginseng] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.AirElemental,
                (caster, scroll) => new AirElementalSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Air Elemental",
                    Mantra = "Kal Vas Xen Hur",
                    Action = 269,
                    AllowTown = false,
                    LeftHandEffect = 9010,
                    RightHandEffect = 9010,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    }
                }
            );


            Register(
                SpellEntry.SummonDaemon,
                (caster, scroll) => new SummonDaemonSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Summon Daemon",
                    Mantra = "Kal Vas Xen Corp",
                    Action = 269,
                    AllowTown = false,
                    LeftHandEffect = 9050,
                    RightHandEffect = 9050,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    }
                }
            );


            Register(
                SpellEntry.EarthElemental,
                (caster, scroll) => new EarthElementalSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Earth Elemental",
                    Mantra = "Kal Vas Xen Ylem",
                    Action = 269,
                    AllowTown = false,
                    LeftHandEffect = 9020,
                    RightHandEffect = 9020,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    }
                }
            );


            Register(
                SpellEntry.FireElemental,
                (caster, scroll) => new FireElementalSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Fire Elemental",
                    Mantra = "Kal Vas Xen Flam",
                    Action = 269,
                    AllowTown = false,
                    LeftHandEffect = 9050,
                    RightHandEffect = 9050,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1,
                        [Reagent.SulfurousAsh] = 1
                    }
                }
            );


            Register(
                SpellEntry.WaterElemental,
                (caster, scroll) => new WaterElementalSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Water Elemental",
                    Mantra = "Kal Vas Xen An Flam",
                    Action = 269,
                    AllowTown = false,
                    LeftHandEffect = 9070,
                    RightHandEffect = 9070,
                    Circle = SpellCircle.Eighth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodmoss] = 1,
                        [Reagent.MandrakeRoot] = 1,
                        [Reagent.SpidersSilk] = 1
                    }
                }
            );


            Register(
                SpellEntry.ControlUndead,
                (caster, scroll) => new ControlUndeadSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Control Undead",
                    Mantra = "Nutu Magistri Supplicare",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bloodspawn] = 1,
                        [Reagent.Blackmoor] = 1,
                        [Reagent.Bone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Darkness,
                (caster, scroll) => new DarknessSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Darkness",
                    Mantra = "In Caligne Abditus",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Pumice] = 1,
                        [Reagent.PigIron] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.DecayingRay,
                (caster, scroll) => new DecayingRaySpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Decaying Ray",
                    Mantra = "Umbra Aufero Vita",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.VialOfBlood] = 1,
                        [Reagent.VialOfBlood] = 1,
                        [Reagent.VolcanicAsh] = 1,
                        [Reagent.DaemonBone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.SpectresTouch,
                (caster, scroll) => new SpectresTouchSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Spectres Touch",
                    Mantra = "Enevare",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.ExecutionersCap] = 1,
                        [Reagent.Brimstone] = 1,
                        [Reagent.DaemonBone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.AbyssalFlame,
                (caster, scroll) => new AbyssalFlameSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Abyssal Flame",
                    Mantra = "Orinundus Barathrum Erado Hostes Hostium",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Brimstone] = 1,
                        [Reagent.Obsidian] = 1,
                        [Reagent.VolcanicAsh] = 1,
                        [Reagent.DaemonBone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.AnimateDead,
                (caster, scroll) => new AnimateDeadSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Animate Dead",
                    Mantra = "Corpus Sine Nomine Expergefaceret",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.FertileDirt] = 1,
                        [Reagent.VialOfBlood] = 1,
                        [Reagent.Bone] = 1,
                        [Reagent.Obsidian] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Sacrifice,
                (caster, scroll) => new SacrificeSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Sacrifice",
                    Mantra = "Animus Ex Corporis Resolveretur",
                    Action = 16,
                    AllowTown = true,
                    LeftHandEffect = 0,
                    RightHandEffect = 0,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.ExecutionersCap] = 1,
                        [Reagent.Bloodspawn] = 1,
                        [Reagent.WyrmsHeart] = 1,
                        [Reagent.Bone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.WraithBreath,
                (caster, scroll) => new WraithBreathSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Wraith Breath",
                    Mantra = "Manes Sollicti Mi Compellere",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Pumice] = 1,
                        [Reagent.Obsidian] = 1,
                        [Reagent.Bone] = 1,
                        [Reagent.Blackmoor] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.SorcerersBane,
                (caster, scroll) => new SorcerersBaneSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Sorceror's Bane",
                    Mantra = "Fluctus Perturbo Magus Navitas",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.VolcanicAsh] = 1,
                        [Reagent.Pumice] = 1,
                        [Reagent.DragonsBlood] = 1,
                        [Reagent.DeadWood] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.SummonSpirit,
                (caster, scroll) => new SummonSpiritSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Summon Spirit",
                    Mantra = "Manes Turbidi Sollictique Resolverent",
                    Action = 221,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DaemonBone] = 1,
                        [Reagent.Brimstone] = 1,
                        [Reagent.DragonsBlood] = 1,
                        [Reagent.Bloodspawn] = 1
                    }
                }
            );


            Register(
                SpellEntry.WraithForm,
                (caster, scroll) => new WraithFormSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Wraith Form",
                    Mantra = "Manes Sollicti Mihi Infundite",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DaemonBone] = 1,
                        [Reagent.Brimstone] = 1,
                        [Reagent.Bloodspawn] = 1
                    }
                }
            );


            Register(
                SpellEntry.WyvernStrike,
                (caster, scroll) => new WyvernStrikeSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Wyvern Strike",
                    Mantra = "Ubrae Tenebrae Venarent",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DragonsBlood] = 1,
                        [Reagent.SerpentScale] = 1,
                        [Reagent.Blackmoor] = 1,
                        [Reagent.Bloodspawn] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Kill,
                (caster, scroll) => new KillSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Kill",
                    Mantra = "Ulties Manum Necarent",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DaemonBone] = 1,
                        [Reagent.ExecutionersCap] = 1,
                        [Reagent.VialOfBlood] = 1,
                        [Reagent.WyrmsHeart] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.LicheForm,
                (caster, scroll) => new LicheFormSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Liche Form",
                    Mantra = "Umbrae Tenebrae Miserere",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DaemonBone] = 1,
                        [Reagent.Brimstone] = 1,
                        [Reagent.DragonsBlood] = 1,
                        [Reagent.VolcanicAsh] = 1
                    }
                }
            );


            Register(
                SpellEntry.Plague,
                (caster, scroll) => new PlagueSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Plague",
                    Mantra = "Fluctus Puter Se Aresceret",
                    Action = 227,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.Necro,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.VolcanicAsh] = 1,
                        [Reagent.BatWing] = 1,
                        [Reagent.DaemonBone] = 1,
                        [Reagent.DragonsBlood] = 1,
                        [Reagent.Bloodspawn] = 1,
                        [Reagent.Pumice] = 1,
                        [Reagent.SerpentScale] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Spellbind,
                (caster, scroll) => new SpellbindSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Spellbind",
                    Mantra = "Nutu Magistri Se Compellere",
                    Action = 221,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Necro,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.EyeOfNewt] = 1,
                        [Reagent.VialOfBlood] = 1,
                        [Reagent.FertileDirt] = 1,
                        [Reagent.PigIron] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.Antidote,
                (caster, scroll) => new AntidoteSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Antidote",
                    Mantra = "Puissante Terre Traite Ce Patient",
                    Action = 212,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DeadWood] = 1,
                        [Reagent.FertileDirt] = 1,
                        [Reagent.ExecutionersCap] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.OwlSight,
                (caster, scroll) => new OwlSightSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Owl Sight",
                    Mantra = "Vista Da Noite",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.EyeOfNewt] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.ShiftingEarth,
                (caster, scroll) => new ShiftingEarthSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Shifting Earth",
                    Mantra = "Esmagamento Con Pedra",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.Earth,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.FertileDirt] = 1,
                        [Reagent.Obsidian] = 1,
                        [Reagent.DeadWood] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.SummonMammals,
                (caster, scroll) => new SummonMammalsSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Summon Mammals",
                    Mantra = "Chame O Mamifero Agora",
                    Action = 16,
                    AllowTown = false,
                    LeftHandEffect = 0,
                    RightHandEffect = 0,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.SerpentScale] = 1,
                        [Reagent.PigIron] = 1,
                        [Reagent.EyeOfNewt] = 1
                    }
                }
            );


            Register(
                SpellEntry.CallLightning,
                (caster, scroll) => new CallLightningSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Call Lightning",
                    Mantra = "Batida Do Deus",
                    Action = 236,
                    AllowTown = true,
                    LeftHandEffect = 9031,
                    RightHandEffect = 9031,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.WyrmsHeart] = 1,
                        [Reagent.PigIron] = 1,
                        [Reagent.Bone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.EarthsBlessing,
                (caster, scroll) => new EarthsBlessingSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Earths Blessing",
                    Mantra = "Foria Da Terra",
                    Action = 203,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.PigIron] = 1,
                        [Reagent.Obsidian] = 1,
                        [Reagent.VolcanicAsh] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.EarthPortal,
                (caster, scroll) => new EarthPortalSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Earth Portal",
                    Mantra = "Destraves Limites Da Natureza",
                    Action = 263,
                    AllowTown = true,
                    LeftHandEffect = 9032,
                    RightHandEffect = 9032,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Brimstone] = 1,
                        [Reagent.ExecutionersCap] = 1,
                        [Reagent.EyeOfNewt] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.NaturesTouch,
                (caster, scroll) => new NaturesTouchSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Natures Touch",
                    Mantra = "Guerissez Par Terre",
                    Action = 204,
                    AllowTown = true,
                    LeftHandEffect = 9061,
                    RightHandEffect = 9061,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Pumice] = 1,
                        [Reagent.VialOfBlood] = 1,
                        [Reagent.Obsidian] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Beneficial
                    }
                }
            );


            Register(
                SpellEntry.GustOfAir,
                (caster, scroll) => new GustOfAirSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Gust Of Air",
                    Mantra = "Gust Do Ar",
                    Action = 230,
                    AllowTown = true,
                    LeftHandEffect = 9022,
                    RightHandEffect = 9022,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.Air,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BatWing] = 1,
                        [Reagent.Brimstone] = 1,
                        [Reagent.VialOfBlood] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.RisingFire,
                (caster, scroll) => new RisingFireSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Rising Fire",
                    Mantra = "Batida Do Fogo",
                    Action = 233,
                    AllowTown = true,
                    LeftHandEffect = 9012,
                    RightHandEffect = 9012,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.Fire,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.BatWing] = 1,
                        [Reagent.Brimstone] = 1,
                        [Reagent.VialOfBlood] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = true,
                        Flags = TargetFlags.None
                    }
                }
            );


            Register(
                SpellEntry.Shapeshift,
                (caster, scroll) => new ShapeshiftSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Shapeshift",
                    Mantra = "Mude Minha Forma",
                    Action = 221,
                    AllowTown = true,
                    LeftHandEffect = 9002,
                    RightHandEffect = 9002,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.WyrmsHeart] = 1,
                        [Reagent.Blackmoor] = 1,
                        [Reagent.BatWing] = 1
                    }
                }
            );


            Register(
                SpellEntry.IceStrike,
                (caster, scroll) => new IceStrikeSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Ice Strike",
                    Mantra = "Geada Com Inverno",
                    Action = 233,
                    AllowTown = true,
                    LeftHandEffect = 9012,
                    RightHandEffect = 9012,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.Water,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.Bone] = 1,
                        [Reagent.BatWing] = 1,
                        [Reagent.Brimstone] = 1
                    },
                    TargetOptions = new TargetOptions
                    {
                        CheckLos = true,
                        Range = SpellInfo.DefaultSpellRange,
                        AllowGround = false,
                        Flags = TargetFlags.Harmful
                    }
                }
            );


            Register(
                SpellEntry.EarthSpirit,
                (caster, scroll) => new EarthSpiritSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Earth Spirit",
                    Mantra = "Chame A Terra Elemental",
                    Action = 269,
                    AllowTown = true,
                    LeftHandEffect = 9010,
                    RightHandEffect = 9010,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.DragonsBlood] = 1,
                        [Reagent.FertileDirt] = 1,
                        [Reagent.VolcanicAsh] = 1
                    }
                }
            );


            Register(
                SpellEntry.FireSpirit,
                (caster, scroll) => new FireSpiritSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Fire Spirit",
                    Mantra = "Chame O Fogo Elemental",
                    Action = 269,
                    AllowTown = true,
                    LeftHandEffect = 9010,
                    RightHandEffect = 9010,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.EyeOfNewt] = 1,
                        [Reagent.Blackmoor] = 1,
                        [Reagent.Obsidian] = 1
                    }
                }
            );


            Register(
                SpellEntry.StormSpirit,
                (caster, scroll) => new StormSpiritSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Storm Spirit",
                    Mantra = "Chame O Ar Elemental",
                    Action = 269,
                    AllowTown = true,
                    LeftHandEffect = 9010,
                    RightHandEffect = 9010,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.FertileDirt] = 1,
                        [Reagent.VolcanicAsh] = 1,
                        [Reagent.BatWing] = 1
                    }
                }
            );


            Register(
                SpellEntry.WaterSpirit,
                (caster, scroll) => new WaterSpiritSpell(caster, scroll),
                new SpellInfo
                {
                    Name = "Water Spirit",
                    Mantra = "Chame A Agua Elemental",
                    Action = 269,
                    AllowTown = true,
                    LeftHandEffect = 9010,
                    RightHandEffect = 9010,
                    Circle = SpellCircle.Earth,
                    DamageType = ElementalType.None,
                    ReagentCosts = new Dictionary<Type, int>
                    {
                        [Reagent.WyrmsHeart] = 1,
                        [Reagent.SerpentScale] = 1,
                        [Reagent.EyeOfNewt] = 1
                    }
                }
            );
        }
    }
}