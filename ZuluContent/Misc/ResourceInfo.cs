using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Server.Configurations.ResourceConfiguration;

namespace Server.Items
{
    public enum CraftResource
    {
        None = 0,
        Iron = 1,
        Spike = 2,
        Fruity = 3,
        IceRock = 4,
        BlackDwarf = 5,
        Bronze = 6,
        DarkPagan = 7,
        SilverRock = 8,
        Platinum = 9,
        DullCopper = 10,
        Mystic = 11,
        Copper = 12,
        Spectral = 13,
        Onyx = 14,
        OldBritain = 15,
        RedElven = 16,
        Undead = 17,
        Pyrite = 18,
        Virginity = 19,
        Malachite = 20,
        Lavarock = 21,
        Azurite = 22,
        Dripstone = 23,
        Executor = 24,
        Peachblue = 25,
        Destruction = 26,
        Anra = 27,
        Crystal = 28,
        Doom = 29,
        Goddess = 30,
        NewZulu = 31,
        EbonTwilightSapphire = 32,
        DarkSableRuby = 33,
        RadiantNimbusDiamond = 34,

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
        GoldenReflections = 306,
        Jadewood = 307,
        Darkwood = 308,
        Stonewood = 309,
        Sunwood = 310,
        Swampwood = 311,
        Stardust = 312,
        Silverleaf = 313,
        Stormteal = 314,
        Emeraldwood = 315,
        Bloodwood = 316,
        Hardranger = 317,
        Crystalwood = 318,
        Gauntlet = 319,
        Doomwood = 320,
        Bloodhorse = 321,
        Zulu = 322,
        Darkness = 323,
        Elven = 324,
        YoungOak = 325,
    }

    public enum CraftResourceType
    {
        None,
        Metal,
        Leather,
        Wood
    }

    public class CraftResourceInfo
    {
        public int Hue { get; private set; }

        public string Name { get; private set; }

        public CraftResource Resource { get; private set; }

        public Type[] ResourceTypes { get; private set; }

        public double Quality { get; private set; }

        public double CraftSkillRequired { get; private set; }

        public Dictionary<Type, int> Enchantments { get; private set; }

        public CraftResourceInfo(int hue, string name,
            CraftResource resource, Dictionary<Type, int> enchantments, double quality, double craftSkillRequired,
            params Type[] resourceTypes)
        {
            Hue = hue;
            Name = name;
            Resource = resource;
            Enchantments = enchantments;
            Quality = quality;
            CraftSkillRequired = craftSkillRequired;
            ResourceTypes = resourceTypes;

            foreach (var t in resourceTypes)
                CraftResources.RegisterType(t, resource);
        }
    }

    public class CraftResources
    {
        public static readonly CraftResourceInfo[] MetalInfo = OreConfiguration.Entries.Select((e, i) =>
            new CraftResourceInfo(e.Hue, e.Name, (CraftResource) (i + 1),
                e.Enchantments.ToDictionary(x => x.EnchantmentType, y => y.EnchantmentValue),
                e.Quality, e.CraftSkillRequired, e.SmeltType, e.ResourceType)).ToArray();

        public static readonly CraftResourceInfo[] LeatherInfo =
        {
            new CraftResourceInfo(0x000, "Normal", CraftResource.RegularLeather, null, 1.0, 0.0,
                typeof(Leather), typeof(Hides)),
        };

        public static readonly CraftResourceInfo[] WoodInfo = LogConfiguration.Entries.Select((e, i) =>
            new CraftResourceInfo(e.Hue, e.Name, (CraftResource) (i + 301),
                null,
                e.Quality, e.CraftSkillRequired, e.ResourceType)).ToArray();

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
            m_TypeTable ??= new Hashtable();

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
            CraftResourceInfo[] list = GetType(resource) switch
            {
                CraftResourceType.Metal => MetalInfo,
                CraftResourceType.Leather => LeatherInfo,
                CraftResourceType.Wood => WoodInfo,
                _ => null
            };

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

            if (resource >= CraftResource.RegularWood && resource <= CraftResource.YoungOak)
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

        public static Dictionary<Type, int> GetEnchantments(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return info == null ? null : info.Enchantments;
        }

        public static double GetQuality(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return info == null ? 1.0 : info.Quality;
        }

        public static double GetCraftSkillRequired(CraftResource resource)
        {
            CraftResourceInfo info = GetInfo(resource);

            return info == null ? 0.0 : info.CraftSkillRequired;
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