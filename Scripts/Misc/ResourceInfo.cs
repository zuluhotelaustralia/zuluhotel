using System;
using System.Collections;

namespace Server.Items
{
    public enum CraftResource
    {
        None = 0,
        Iron = 1,
        Gold = 2,
        Spike = 3,
        Fruity = 4,
        Bronze = 5,
        IceRock = 6,
        BlackDwarf = 7,
        DullCopper = 8,
        Platinum = 9,
        SilverRock = 10,
        DarkPagan = 11,
        Copper = 12,
        Mystic = 13,
        Spectral = 14,
        OldBritain = 15,
        Onyx = 16,
        RedElven = 17,
        Undead = 18,
        Pyrite = 19,
        Virginity = 20,
        Malachite = 21,
        Lavarock = 22,
        Azurite = 23,
        Dripstone = 24,
        Executor = 25,
        Peachblue = 26,
        Destruction = 27,
        Anra = 28,
        Crystal = 29,
        Doom = 30,
        Goddess = 31,
        NewZulu = 32,
        DarkSableRuby = 33, 
        EbonTwilightSapphire = 34,
        RadiantNimbusDiamond = 35,

        RegularLeather = 101,
        RatLeather = 102,
        WolfLeather = 103,
        BearLeather = 104,
        SerpentLeather = 105,
        LizardLeather = 106,
        TrollLeather = 107,
        OstardLeather = 108,
        NecromancerLeather = 109,
        LavaLeather = 110,
        LicheLeather = 111,
        IceCrystalLeather = 112,
        DragonLeather = 113,
        WyrmLeather = 114,
        BalronLeather = 115,
        GoldenDragonLeather = 116,

        RegularWood = 301,
        Pinetree = 302,
        Cherry = 303,
        Oak = 304,
        PurplePassion = 305,
        GoldenReflection = 306,
        Hardranger = 307,
        Jadewood = 308,
        Darkwood = 309,
        Stonewood = 310,
        Sunwood = 311,
        Gauntlet = 312, //312
        Swampwood = 313,
        Stardust = 314,
        Silverleaf = 315,
        Stormteal = 316,
        Emeraldwood = 317,
        Bloodwood = 318,
        Crystalwood = 319,
        Bloodhorse = 320,
        Doomwood = 321,
        Zulu = 322,
        Darkness = 323,
        Elven = 324,
    }

    public enum CraftResourceType
    {
        None,
        Metal,
        Leather,
        Wood
    }

    public class CraftAttributeInfo
    {
        private int m_WeaponDurability;
        private int m_WeaponLowerRequirements;

        private int m_ArmorDurability;
        private int m_ArmorLowerRequirements;

        private int m_RunicMinAttributes;
        private int m_RunicMaxAttributes;
        private int m_RunicMinIntensity;
        private int m_RunicMaxIntensity;

        public int WeaponDurability
        {
            get { return m_WeaponDurability; }
            set { m_WeaponDurability = value; }
        }

        public int WeaponLowerRequirements
        {
            get { return m_WeaponLowerRequirements; }
            set { m_WeaponLowerRequirements = value; }
        }

        public int ArmorDurability
        {
            get { return m_ArmorDurability; }
            set { m_ArmorDurability = value; }
        }

        public int ArmorLowerRequirements
        {
            get { return m_ArmorLowerRequirements; }
            set { m_ArmorLowerRequirements = value; }
        }

        public int RunicMinAttributes
        {
            get { return m_RunicMinAttributes; }
            set { m_RunicMinAttributes = value; }
        }

        public int RunicMaxAttributes
        {
            get { return m_RunicMaxAttributes; }
            set { m_RunicMaxAttributes = value; }
        }

        public int RunicMinIntensity
        {
            get { return m_RunicMinIntensity; }
            set { m_RunicMinIntensity = value; }
        }

        public int RunicMaxIntensity
        {
            get { return m_RunicMaxIntensity; }
            set { m_RunicMaxIntensity = value; }
        }

        public int ArmorEnergyResist { get; set; }

        public int ArmorPoisonResist { get; set; }

        public int ArmorColdResist { get; set; }

        public int ArmorFireResist { get; set; }

        public CraftAttributeInfo()
        {
        }

        public static readonly CraftAttributeInfo Blank;

        public static readonly CraftAttributeInfo
            Gold,
            Spike,
            Fruity,
            Bronze,
            IceRock,
            BlackDwarf,
            DullCopper,
            Platinum,
            SilverRock,
            DarkPagan,
            Copper,
            Mystic,
            Spectral,
            OldBritain,
            Onyx,
            RedElven,
            Undead,
            Pyrite,
            Virginity,
            Malachite,
            Lavarock,
            Azurite,
            Dripstone,
            Executor,
            Peachblue,
            Destruction,
            Anra,
            Crystal,
            Doom,
            Goddess,
            NewZulu,
            DarkSableRuby,
            EbonTwilightSapphire,
            RadiantNimbusDiamond;

        public static readonly CraftAttributeInfo
            Rat,
            Wolf,
            Bear,
            Serpent,
            Lizard,
            Troll,
            Ostard,
            Necromancer,
            Lava,
            Liche,
            IceCrystal,
            Dragon,
            Wyrm,
            Balron,
            GoldenDragon;

        public static readonly CraftAttributeInfo
            Pinetree,
            Cherry,
            Oak,
            PurplePassion,
            GoldenReflection,
            Hardranger,
            Jadewood,
            Darkwood,
            Stonewood,
            Sunwood,
            Gauntlet,
            Swampwood,
            Stardust,
            Silverleaf,
            Stormteal,
            Emeraldwood,
            Bloodwood,
            Crystalwood,
            Bloodhorse,
            Doomwood,
            Zulu,
            Darkness,
            Elven;

        static CraftAttributeInfo()
        {
            Blank = new CraftAttributeInfo();

            Gold = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Spike = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Fruity = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Bronze = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            IceRock = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            BlackDwarf = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            DullCopper = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Platinum = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            SilverRock = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            DarkPagan = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Copper = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Mystic = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Spectral = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            OldBritain = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Onyx = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            RedElven = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Undead = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Pyrite = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Virginity = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Malachite = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Lavarock = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Azurite = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Dripstone = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Executor = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Peachblue = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Destruction = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Anra = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Crystal = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Doom = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Goddess = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            NewZulu = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            DarkSableRuby = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            EbonTwilightSapphire = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            RadiantNimbusDiamond = new CraftAttributeInfo
            {
                ArmorDurability = 50,
                ArmorLowerRequirements = 20,
                WeaponDurability = 100,
                WeaponLowerRequirements = 50,
                RunicMinAttributes = 1,
                RunicMaxAttributes = 2,
                RunicMinIntensity = 10,
                RunicMaxIntensity = 35,
            };

            Rat = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
                RunicMinIntensity = 40,
                RunicMaxIntensity = 100
            };


            Wolf = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Bear = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Serpent = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Lizard = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Troll = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Ostard = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Necromancer = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Lava = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Liche = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            IceCrystal = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Dragon = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Wyrm = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            Balron = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };
            GoldenDragon = new CraftAttributeInfo
            {
                ArmorFireResist = 1,
                ArmorColdResist = 2,
                ArmorPoisonResist = 3,
                ArmorEnergyResist = 4,
                RunicMinAttributes = 4,
                RunicMaxAttributes = 5,
            };

            Pinetree = new CraftAttributeInfo();
            Cherry = new CraftAttributeInfo();
            Oak = new CraftAttributeInfo();
            PurplePassion = new CraftAttributeInfo();
            GoldenReflection = new CraftAttributeInfo();
            Hardranger = new CraftAttributeInfo();
            Jadewood = new CraftAttributeInfo();
            Darkwood = new CraftAttributeInfo();
            Stonewood = new CraftAttributeInfo();
            Sunwood = new CraftAttributeInfo();
            Gauntlet = new CraftAttributeInfo();
            Swampwood = new CraftAttributeInfo();
            Stardust = new CraftAttributeInfo();
            Silverleaf = new CraftAttributeInfo();
            Stormteal = new CraftAttributeInfo();
            Emeraldwood = new CraftAttributeInfo();
            Bloodwood = new CraftAttributeInfo();
            Crystalwood = new CraftAttributeInfo();
            Bloodhorse = new CraftAttributeInfo();
            Doomwood = new CraftAttributeInfo();
            Zulu = new CraftAttributeInfo();
            Darkness = new CraftAttributeInfo();
            Elven = new CraftAttributeInfo();
        }
    }

    public class CraftResourceInfo
    {
        private int m_Hue;
        private int m_Number;
        private string m_Name;
        private CraftAttributeInfo m_AttributeInfo;
        private CraftResource m_Resource;
        private Type[] m_ResourceTypes;

        public int Hue
        {
            get { return m_Hue; }
        }

        public int Number
        {
            get { return m_Number; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public CraftAttributeInfo AttributeInfo
        {
            get { return m_AttributeInfo; }
        }

        public CraftResource Resource
        {
            get { return m_Resource; }
        }

        public Type[] ResourceTypes
        {
            get { return m_ResourceTypes; }
        }

        public CraftResourceInfo(int hue, int number, string name, CraftAttributeInfo attributeInfo,
            CraftResource resource, params Type[] resourceTypes)
        {
            m_Hue = hue;
            m_Number = number;
            m_Name = name;
            m_AttributeInfo = attributeInfo;
            m_Resource = resource;
            m_ResourceTypes = resourceTypes;

            for (int i = 0; i < resourceTypes.Length; ++i)
                CraftResources.RegisterType(resourceTypes[i], resource);
        }
    }

    public class CraftResources
    {
        private static CraftResourceInfo[] m_MetalInfo = new[]
        {
            new CraftResourceInfo(0x0, 1053109, "Iron", CraftAttributeInfo.Blank, CraftResource.Iron, typeof(IronIngot),
                typeof(IronOre)),
            new CraftResourceInfo(2793, 1160000, "Gold", CraftAttributeInfo.Gold, CraftResource.Gold, typeof(GoldIngot),
                typeof(GoldOre)),
            new CraftResourceInfo(0x4c7, 1160001, "Spike", CraftAttributeInfo.Spike, CraftResource.Spike,
                typeof(SpikeIngot), typeof(SpikeOre)),
            new CraftResourceInfo(0x46e, 1160002, "Fruity", CraftAttributeInfo.Fruity, CraftResource.Fruity,
                typeof(FruityIngot), typeof(FruityOre)),
            new CraftResourceInfo(0x45e, 1160003, "Bronze", CraftAttributeInfo.Bronze, CraftResource.Bronze,
                typeof(BronzeIngot), typeof(BronzeOre)),
            new CraftResourceInfo(0x480, 1160004, "Ice rock", CraftAttributeInfo.IceRock, CraftResource.IceRock,
                typeof(IceRockIngot), typeof(IceRockOre)),
            new CraftResourceInfo(0x451, 1160005, "Black dwarf", CraftAttributeInfo.BlackDwarf,
                CraftResource.BlackDwarf, typeof(BlackDwarfIngot), typeof(BlackDwarfOre)),
            new CraftResourceInfo(0x3ea, 1160006, "Dull copper", CraftAttributeInfo.DullCopper,
                CraftResource.DullCopper, typeof(DullCopperIngot), typeof(DullCopperOre)),
            new CraftResourceInfo(0x457, 1160007, "Platinum", CraftAttributeInfo.Platinum, CraftResource.Platinum,
                typeof(PlatinumIngot), typeof(PlatinumOre)),
            new CraftResourceInfo(0x3e9, 1160008, "Silver rock", CraftAttributeInfo.SilverRock,
                CraftResource.SilverRock, typeof(SilverRockIngot), typeof(SilverRockOre)),
            new CraftResourceInfo(0x46b, 1160009, "Dark pagan", CraftAttributeInfo.DarkPagan, CraftResource.DarkPagan,
                typeof(DarkPaganIngot), typeof(DarkPaganOre)),
            new CraftResourceInfo(0x602, 1160010, "Copper", CraftAttributeInfo.Copper, CraftResource.Copper,
                typeof(CopperIngot), typeof(CopperOre)),
            new CraftResourceInfo(0x17f, 1160011, "Mystic", CraftAttributeInfo.Mystic, CraftResource.Mystic,
                typeof(MysticIngot), typeof(MysticOre)),
            new CraftResourceInfo(2744, 1160012, "Spectral", CraftAttributeInfo.Spectral, CraftResource.Spectral,
                typeof(SpectralIngot), typeof(SpectralOre)),
            new CraftResourceInfo(0x852, 1160013, "Old britain", CraftAttributeInfo.OldBritain,
                CraftResource.OldBritain, typeof(OldBritainIngot), typeof(OldBritainOre)),
            new CraftResourceInfo(0x455, 1160014, "Onyx", CraftAttributeInfo.Onyx, CraftResource.Onyx,
                typeof(OnyxIngot), typeof(OnyxOre)),
            new CraftResourceInfo(0x4b9, 1160015, "Red elven", CraftAttributeInfo.RedElven, CraftResource.RedElven,
                typeof(RedElvenIngot), typeof(RedElvenOre)),
            new CraftResourceInfo(0x279, 1160016, "Undead", CraftAttributeInfo.Undead, CraftResource.Undead,
                typeof(UndeadIngot), typeof(UndeadOre)),
            new CraftResourceInfo(0x6b8, 1160017, "Pyrite", CraftAttributeInfo.Pyrite, CraftResource.Pyrite,
                typeof(PyriteIngot), typeof(PyriteOre)),
            new CraftResourceInfo(0x482, 1160018, "Virginity", CraftAttributeInfo.Virginity, CraftResource.Virginity,
                typeof(VirginityIngot), typeof(VirginityOre)),
            new CraftResourceInfo(2748, 1160019, "Malachite", CraftAttributeInfo.Malachite, CraftResource.Malachite,
                typeof(MalachiteIngot), typeof(MalachiteOre)),
            new CraftResourceInfo(2747, 1160020, "Lavarock", CraftAttributeInfo.Lavarock, CraftResource.Lavarock,
                typeof(LavarockIngot), typeof(LavarockOre)),
            new CraftResourceInfo(0x4df, 1160021, "Azurite", CraftAttributeInfo.Azurite, CraftResource.Azurite,
                typeof(AzuriteIngot), typeof(AzuriteOre)),
            new CraftResourceInfo(2771, 1160022, "Dripstone", CraftAttributeInfo.Dripstone, CraftResource.Dripstone,
                typeof(DripstoneIngot), typeof(DripstoneOre)),
            new CraftResourceInfo(2766, 1160023, "Executor", CraftAttributeInfo.Executor, CraftResource.Executor,
                typeof(ExecutorIngot), typeof(ExecutorOre)),
            new CraftResourceInfo(2769, 1160024, "Peachblue", CraftAttributeInfo.Peachblue, CraftResource.Peachblue,
                typeof(PeachblueIngot), typeof(PeachblueOre)),
            new CraftResourceInfo(2773, 1160025, "Destruction", CraftAttributeInfo.Destruction,
                CraftResource.Destruction, typeof(DestructionIngot), typeof(DestructionOre)),
            new CraftResourceInfo(0x48b, 1160026, "Anra", CraftAttributeInfo.Anra, CraftResource.Anra,
                typeof(AnraIngot), typeof(AnraOre)),
            new CraftResourceInfo(2759, 1160027, "Crystal", CraftAttributeInfo.Crystal, CraftResource.Crystal,
                typeof(CrystalIngot), typeof(CrystalOre)),
            new CraftResourceInfo(2772, 1160028, "Doom", CraftAttributeInfo.Doom, CraftResource.Doom, typeof(DoomIngot),
                typeof(DoomOre)),
            new CraftResourceInfo(2774, 1160029, "Goddess", CraftAttributeInfo.Goddess, CraftResource.Goddess,
                typeof(GoddessIngot), typeof(GoddessOre)),
            new CraftResourceInfo(2749, 1160030, "New zulu", CraftAttributeInfo.NewZulu, CraftResource.NewZulu,
                typeof(NewZuluIngot), typeof(NewZuluOre)),
            new CraftResourceInfo(2761, 1160032, "Dark sable ruby", CraftAttributeInfo.DarkSableRuby,
                CraftResource.DarkSableRuby, typeof(DarkSableRubyIngot), typeof(DarkSableRubyOre)
            ),
            new CraftResourceInfo(2760, 1160031, "Ebon twilight sapphire", CraftAttributeInfo.EbonTwilightSapphire,
                CraftResource.EbonTwilightSapphire, typeof(EbonTwilightSapphireIngot), typeof(EbonTwilightSapphireOre)
            ),
            new CraftResourceInfo(2765, 1160033, "Radiant nimbus diamond", CraftAttributeInfo.RadiantNimbusDiamond,
                CraftResource.RadiantNimbusDiamond, typeof(RadiantNimbusDiamondIngot), typeof(RadiantNimbusDiamondOre)
            ),
        };

        private static CraftResourceInfo[] m_LeatherInfo = new[]
        {
            new CraftResourceInfo(0x000, 1049353, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularLeather,
                typeof(Leather), typeof(Hides)),
            new CraftResourceInfo(0x7e2, 1160415, "Rat", CraftAttributeInfo.Rat, CraftResource.RatLeather,
                typeof(RatLeather), typeof(RatHides)),
            new CraftResourceInfo(1102, 1160416, "Wolf", CraftAttributeInfo.Wolf, CraftResource.WolfLeather,
                typeof(WolfLeather), typeof(WolfHides)),
            new CraftResourceInfo(44, 1160417, "Bear", CraftAttributeInfo.Bear, CraftResource.BearLeather,
                typeof(BearLeather), typeof(BearHides)),
            new CraftResourceInfo(0x8fd, 1160418, "Serpent", CraftAttributeInfo.Serpent, CraftResource.SerpentLeather,
                typeof(SerpentLeather), typeof(SerpentHides)),
            new CraftResourceInfo(0x852, 1160419, "Lizard", CraftAttributeInfo.Lizard, CraftResource.LizardLeather,
                typeof(LizardLeather), typeof(LizardHides)),
            new CraftResourceInfo(0x54a, 1160420, "Troll", CraftAttributeInfo.Troll, CraftResource.TrollLeather,
                typeof(TrollLeather), typeof(TrollHides)),
            new CraftResourceInfo(0x415, 1160421, "Ostard", CraftAttributeInfo.Ostard, CraftResource.OstardLeather,
                typeof(OstardLeather), typeof(OstardHides)),
            new CraftResourceInfo(84, 1160422, "Necromancer", CraftAttributeInfo.Necromancer,
                CraftResource.NecromancerLeather, typeof(NecromancerLeather), typeof(NecromancerHides)),
            new CraftResourceInfo(2747, 1160423, "Lava", CraftAttributeInfo.Lava, CraftResource.LavaLeather,
                typeof(LavaLeather), typeof(LavaHides)),
            new CraftResourceInfo(2763, 1160424, "Liche", CraftAttributeInfo.Liche, CraftResource.LicheLeather,
                typeof(LicheLeather), typeof(LicheHides)),
            new CraftResourceInfo(2759, 1160425, "Ice Crystal", CraftAttributeInfo.IceCrystal,
                CraftResource.IceCrystalLeather, typeof(IceCrystalLeather), typeof(IceCrystalHides)),
            new CraftResourceInfo(2761, 1160426, "Dragon", CraftAttributeInfo.Dragon, CraftResource.DragonLeather,
                typeof(DragonLeather), typeof(DragonHides)),
            new CraftResourceInfo(2747, 1160427, "Wyrm", CraftAttributeInfo.Wyrm, CraftResource.WyrmLeather,
                typeof(WyrmLeather), typeof(WyrmHides)),
            new CraftResourceInfo(1175, 1160428, "Balron", CraftAttributeInfo.Balron, CraftResource.BalronLeather,
                typeof(BalronLeather), typeof(BalronHides)),
            new CraftResourceInfo(48, 1160429, "Golden Dragon", CraftAttributeInfo.GoldenDragon,
                CraftResource.GoldenDragonLeather, typeof(GoldenDragonLeather), typeof(GoldenDragonHides))
        };

        private static CraftResourceInfo[] m_WoodInfo = new[]
        {
            new CraftResourceInfo(0x000, 1011542, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularWood,
                typeof(Log), typeof(Board)),
            new CraftResourceInfo(1132, 1160034, "Pinetree", CraftAttributeInfo.Pinetree, CraftResource.Pinetree,
                typeof(PinetreeLog), typeof(PinetreeBoard)),
            new CraftResourceInfo(2206, 1160035, "Cherry", CraftAttributeInfo.Cherry, CraftResource.Cherry,
                typeof(CherryLog), typeof(CherryBoard)),
            new CraftResourceInfo(1045, 1160036, "Oak", CraftAttributeInfo.Oak, CraftResource.Oak, typeof(OakLog),
                typeof(OakBoard)),
            new CraftResourceInfo(515, 1160037, "Purple Passion", CraftAttributeInfo.PurplePassion,
                CraftResource.PurplePassion, typeof(PurplePassionLog), typeof(PurplePassionBoard)),
            new CraftResourceInfo(48, 1160038, "Golden Reflection", CraftAttributeInfo.GoldenReflection,
                CraftResource.GoldenReflection, typeof(GoldenReflectionLog), typeof(GoldenReflectionBoard)),
            new CraftResourceInfo(2778, 1160039, "Hardranger", CraftAttributeInfo.Hardranger, CraftResource.Hardranger,
                typeof(HardrangerLog), typeof(HardrangerBoard)),
            new CraftResourceInfo(1162, 1160040, "Jadewood", CraftAttributeInfo.Jadewood, CraftResource.Jadewood,
                typeof(JadewoodLog), typeof(JadewoodBoard)),
            new CraftResourceInfo(1109, 1160041, "Darkwood", CraftAttributeInfo.Darkwood, CraftResource.Darkwood,
                typeof(DarkwoodLog), typeof(DarkwoodBoard)),
            new CraftResourceInfo(1154, 1160042, "Stonewood", CraftAttributeInfo.Stonewood, CraftResource.Stonewood,
                typeof(StonewoodLog), typeof(StonewoodBoard)),
            new CraftResourceInfo(2766, 1160043, "Sunwood", CraftAttributeInfo.Sunwood, CraftResource.Sunwood,
                typeof(SunwoodLog), typeof(SunwoodBoard)),
            new CraftResourceInfo(2777, 1160044, "Gauntlet", CraftAttributeInfo.Gauntlet, CraftResource.Gauntlet,
                typeof(GauntletLog), typeof(GauntletBoard)),
            new CraftResourceInfo(2767, 1160045, "Swampwood", CraftAttributeInfo.Swampwood, CraftResource.Swampwood,
                typeof(SwampwoodLog), typeof(SwampwoodBoard)),
            new CraftResourceInfo(2751, 1160046, "Stardust", CraftAttributeInfo.Stardust, CraftResource.Stardust,
                typeof(StardustLog), typeof(StardustBoard)),
            new CraftResourceInfo(2301, 1160047, "Silver leaf", CraftAttributeInfo.Silverleaf, CraftResource.Silverleaf,
                typeof(SilverleafLog), typeof(SilverleafBoard)),
            new CraftResourceInfo(1346, 1160048, "Stormteal", CraftAttributeInfo.Stormteal, CraftResource.Stormteal,
                typeof(StormtealLog), typeof(StormtealBoard)),
            new CraftResourceInfo(2748, 1160049, "Emerald wood", CraftAttributeInfo.Emeraldwood,
                CraftResource.Emeraldwood, typeof(EmeraldwoodLog), typeof(EmeraldwoodBoard)),
            new CraftResourceInfo(1645, 1160050, "Bloodwood", CraftAttributeInfo.Bloodwood, CraftResource.Bloodwood,
                typeof(BloodwoodLog), typeof(BloodwoodBoard)),
            new CraftResourceInfo(2759, 1160051, "Crystal wood", CraftAttributeInfo.Crystalwood,
                CraftResource.Crystalwood, typeof(CrystalwoodLog), typeof(CrystalwoodBoard)),
            new CraftResourceInfo(2780, 1160052, "Bloodhorse", CraftAttributeInfo.Bloodhorse, CraftResource.Bloodhorse,
                typeof(BloodhorseLog), typeof(BloodhorseBoard)),
            new CraftResourceInfo(2772, 1160053, "Doom wood", CraftAttributeInfo.Doomwood, CraftResource.Doomwood,
                typeof(DoomwoodLog), typeof(DoomwoodBoard)),
            new CraftResourceInfo(2749, 1160054, "Zulu", CraftAttributeInfo.Zulu, CraftResource.Zulu, typeof(ZuluLog),
                typeof(ZuluBoard)),
            new CraftResourceInfo(1175, 1160055, "Darkness", CraftAttributeInfo.Darkness, CraftResource.Darkness,
                typeof(DarknessLog), typeof(DarknessBoard)),
            new CraftResourceInfo(1165, 1160056, "Elven", CraftAttributeInfo.Elven, CraftResource.Elven,
                typeof(ElvenLog), typeof(ElvenBoard)),
        };

        /// <summary>
        /// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or RegularWood. False if otherwise.
        /// </summary>
        public static bool IsStandard(CraftResource resource)
        {
            return resource == CraftResource.None || resource == CraftResource.Iron ||
                   resource == CraftResource.RegularLeather || resource == CraftResource.RegularWood;
        }

        private static Hashtable m_TypeTable;

        /// <summary>
        /// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
        /// </summary>
        public static void RegisterType(Type resourceType, CraftResource resource)
        {
            if (m_TypeTable == null)
                m_TypeTable = new Hashtable();

            m_TypeTable[resourceType] = resource;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
        /// </summary>
        public static CraftResource GetFromType(Type resourceType)
        {
            if (m_TypeTable == null)
                return CraftResource.None;

            object obj = m_TypeTable[resourceType];

            if (!(obj is CraftResource))
                return CraftResource.None;

            return (CraftResource) obj;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
        /// </summary>
        public static CraftResourceInfo GetInfo(CraftResource resource)
        {
            CraftResourceInfo[] list = null;

            switch (GetType(resource))
            {
                case CraftResourceType.Metal:
                    list = m_MetalInfo;
                    break;
                case CraftResourceType.Leather:
                    list = m_LeatherInfo;
                    break;
                case CraftResourceType.Wood:
                    list = m_WoodInfo;
                    break;
            }

            if (list != null)
            {
                int index = GetIndex(resource);

                if (index >= 0 && index < list.Length)
                    return list[index];
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
        /// </summary>
        public static CraftResourceType GetType(CraftResource resource)
        {
            if (resource >= CraftResource.Iron && resource <= CraftResource.RadiantNimbusDiamond)
                return CraftResourceType.Metal;

            if (resource >= CraftResource.RegularLeather && resource <= CraftResource.GoldenDragonLeather)
                return CraftResourceType.Leather;

            if (resource >= CraftResource.RegularWood && resource <= CraftResource.Elven)
                return CraftResourceType.Wood;

            return CraftResourceType.None;
        }

        /// <summary>
        /// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
        /// </summary>
        public static CraftResource GetStart(CraftResource resource)
        {
            return GetType(resource) switch
            {
                CraftResourceType.Metal => CraftResource.Iron,
                CraftResourceType.Leather => CraftResource.RegularLeather,
                CraftResourceType.Wood => CraftResource.RegularWood,
                _ => CraftResource.None
            };
        }

        /// <summary>
        /// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
        /// </summary>
        public static int GetIndex(CraftResource resource)
        {
            CraftResource start = GetStart(resource);

            if (start == CraftResource.None)
                return 0;

            return (int) (resource - start);
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetLocalizationNumber(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return info == null ? 0 : info.Number;
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetHue(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return info == null ? 0 : info.Hue;
        }

        /// <summary>
        /// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
        /// </summary>
        public static string GetName(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return info == null ? String.Empty : info.Name;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info)
        {
            return (CraftResource) info.Level;
        }

        /// <summary>
        /// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
        /// </summary>
        public static CraftResource GetFromOreInfo(OreInfo info, ArmorMaterialType material)
        {
            return GetFromOreInfo(info);
        }
    }

    // NOTE: This class is only for compatability with very old RunUO versions.
    // No changes to it should be required for custom resources.
    public class OreInfo
    {
        public static readonly OreInfo Iron = new OreInfo(0, 0x000, "Iron");
        public static readonly OreInfo DullCopper = new OreInfo(1, 0x973, "Dull Copper");
        public static readonly OreInfo ShadowIron = new OreInfo(2, 0x966, "Shadow Iron");
        public static readonly OreInfo Copper = new OreInfo(3, 0x96D, "Copper");
        public static readonly OreInfo Bronze = new OreInfo(4, 0x972, "Bronze");
        public static readonly OreInfo Gold = new OreInfo(5, 0x8A5, "Gold");
        public static readonly OreInfo Agapite = new OreInfo(6, 0x979, "Agapite");
        public static readonly OreInfo Verite = new OreInfo(7, 0x89F, "Verite");
        public static readonly OreInfo Valorite = new OreInfo(8, 0x8AB, "Valorite");

        private int m_Level;
        private int m_Hue;
        private string m_Name;

        public OreInfo(int level, int hue, string name)
        {
            m_Level = level;
            m_Hue = hue;
            m_Name = name;
        }

        public int Level
        {
            get { return m_Level; }
        }

        public int Hue
        {
            get { return m_Hue; }
        }

        public string Name
        {
            get { return m_Name; }
        }
    }
}