using System;
using System.Linq.Expressions;

namespace Server.Spells
{
    public enum SpellType
    {
        None = -1,
        Clumsy = 00,
        CreateFood = 01,
        Feeblemind = 02,
        Heal = 03,
        MagicArrow = 04,
        NightSight = 05,
        ReactiveArmor = 06,
        Weaken = 07,

        // Second circle
        Agility = 08,
        Cunning = 09,
        Cure = 10,
        Harm = 11,
        MagicTrap = 12,
        RemoveTrap = 13,
        Protection = 14,
        Strength = 15,

        // Third circle
        Bless = 16,
        Fireball = 17,
        MagicLock = 18,
        Poison = 19,
        Telekinesis = 20,
        Teleport = 21,
        Unlock = 22,
        WallOfStone = 23,

        // Fourth circle
        ArchCure = 24,
        ArchProtection = 25,
        Curse = 26,
        FireField = 27,
        GreaterHeal = 28,
        Lightning = 29,
        ManaDrain = 30,
        Recall = 31,

        // Fifth circle
        BladeSpirits = 32,
        DispelField = 33,
        Incognito = 34,
        MagicReflect = 35,
        MindBlast = 36,
        Paralyze = 37,
        PoisonField = 38,
        SummonCreature = 39,

        // Sixth circle
        Dispel = 40,
        EnergyBolt = 41,
        Explosion = 42,
        Invisibility = 43,
        Mark = 44,
        MassCurse = 45,
        ParalyzeField = 46,
        Reveal = 47,

        // Seventh circle
        ChainLightning = 48,
        EnergyField = 49,
        FlameStrike = 50,
        GateTravel = 51,
        ManaVampire = 52,
        MassDispel = 53,
        MeteorSwarm = 54,
        Polymorph = 55,

        // Eighth circle
        Earthquake = 56,
        EnergyVortex = 57,
        Resurrection = 58,
        AirElemental = 59,
        SummonDaemon = 60,
        EarthElemental = 61,
        FireElemental = 62,
        WaterElemental = 63,

        ControlUndead = 100,
        Darkness = 101,
        DecayingRay = 102,
        SpectresTouch = 103,
        AbyssalFlame = 104,
        AnimateDead = 105,
        Sacrifice = 106,
        WraithBreath = 107,
        SorcerorsBane = 108,
        SummonSpirit = 109,
        WraithForm = 110,
        WyvernStrike = 111,
        Kill = 112,
        LicheForm = 113,
        Plague = 114,
        Spellbind = 115,

        Antidote = 600,
        OwlSight = 601,
        ShiftingEarth = 602,
        SummonMammals = 603,
        CallLightning = 604,
        EarthsBlessing = 605,
        EarthPortal = 606,
        NaturesTouch = 607,
        GustOfAir = 608,
        RisingFire = 609,
        Shapeshift = 610,
        IceStrike = 611,
        EarthSpirit = 612,
        FireSpirit = 613,
        StormSpirit = 614,
        WaterSpirit = 615,
    }
}