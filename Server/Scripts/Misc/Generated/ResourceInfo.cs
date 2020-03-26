using System;
using System.Collections.Generic;

namespace Server.Items
{
    public enum CraftResource
    {
	None = 0,

	Iron,
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
	RadiantNimbusDiamond,

	RegularLeather = 101,
	SpinedLeather,
	HornedLeather,
	BarbedLeather,
	RatLeather,
	WolfLeather,
	BearLeather,
	SerpentLeather,
	LizardLeather,
	TrollLeather,
	OstardLeather,
	NecromancerLeather,
	LavaLeather,
	LicheLeather,
	IceCrystalLeather,
	DragonLeather,
	WyrmLeather,
	BalronLeather,
	GoldenDragonLeather,

	RedScales = 201,
	YellowScales,
	BlackScales,
	GreenScales,
	WhiteScales,
	BlueScales,

	RegularWood = 301,
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
	Gauntlet, //312
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
	Elven
    }

    public enum CraftResourceType
    {
	None,
	Metal,
	Leather,
	Scales,
	Wood
    }

    public class CraftAttributeInfo
    {
	private int m_WeaponFireDamage;
	private int m_WeaponColdDamage;
	private int m_WeaponPoisonDamage;
	private int m_WeaponEnergyDamage;
	private int m_WeaponChaosDamage;
	private int m_WeaponDirectDamage;
	private int m_WeaponDurability;
	private int m_WeaponLuck;
	private int m_WeaponGoldIncrease;
	private int m_WeaponLowerRequirements;

	private int m_ArmorPhysicalResist;
	private int m_ArmorFireResist;
	private int m_ArmorColdResist;
	private int m_ArmorPoisonResist;
	private int m_ArmorEnergyResist;
	private int m_ArmorDurability;
	private int m_ArmorLuck;
	private int m_ArmorGoldIncrease;
	private int m_ArmorLowerRequirements;

	private int m_RunicMinAttributes;
	private int m_RunicMaxAttributes;
	private int m_RunicMinIntensity;
	private int m_RunicMaxIntensity;

	public int WeaponFireDamage{ get{ return m_WeaponFireDamage; } set{ m_WeaponFireDamage = value; } }
	public int WeaponColdDamage{ get{ return m_WeaponColdDamage; } set{ m_WeaponColdDamage = value; } }
	public int WeaponPoisonDamage{ get{ return m_WeaponPoisonDamage; } set{ m_WeaponPoisonDamage = value; } }
	public int WeaponEnergyDamage{ get{ return m_WeaponEnergyDamage; } set{ m_WeaponEnergyDamage = value; } }
	public int WeaponChaosDamage{ get{ return m_WeaponChaosDamage; } set{ m_WeaponChaosDamage = value; } }
	public int WeaponDirectDamage{ get{ return m_WeaponDirectDamage; } set{ m_WeaponDirectDamage = value; } }
	public int WeaponDurability{ get{ return m_WeaponDurability; } set{ m_WeaponDurability = value; } }
	public int WeaponLuck{ get{ return m_WeaponLuck; } set{ m_WeaponLuck = value; } }
	public int WeaponGoldIncrease{ get{ return m_WeaponGoldIncrease; } set{ m_WeaponGoldIncrease = value; } }
	public int WeaponLowerRequirements{ get{ return m_WeaponLowerRequirements; } set{ m_WeaponLowerRequirements = value; } }

	public int ArmorPhysicalResist{ get{ return m_ArmorPhysicalResist; } set{ m_ArmorPhysicalResist = value; } }
	public int ArmorFireResist{ get{ return m_ArmorFireResist; } set{ m_ArmorFireResist = value; } }
	public int ArmorColdResist{ get{ return m_ArmorColdResist; } set{ m_ArmorColdResist = value; } }
	public int ArmorPoisonResist{ get{ return m_ArmorPoisonResist; } set{ m_ArmorPoisonResist = value; } }
	public int ArmorEnergyResist{ get{ return m_ArmorEnergyResist; } set{ m_ArmorEnergyResist = value; } }
	public int ArmorDurability{ get{ return m_ArmorDurability; } set{ m_ArmorDurability = value; } }
	public int ArmorLuck{ get{ return m_ArmorLuck; } set{ m_ArmorLuck = value; } }
	public int ArmorGoldIncrease{ get{ return m_ArmorGoldIncrease; } set{ m_ArmorGoldIncrease = value; } }
	public int ArmorLowerRequirements{ get{ return m_ArmorLowerRequirements; } set{ m_ArmorLowerRequirements = value; } }

	public int RunicMinAttributes{ get{ return m_RunicMinAttributes; } set{ m_RunicMinAttributes = value; } }
	public int RunicMaxAttributes{ get{ return m_RunicMaxAttributes; } set{ m_RunicMaxAttributes = value; } }
	public int RunicMinIntensity{ get{ return m_RunicMinIntensity; } set{ m_RunicMinIntensity = value; } }
	public int RunicMaxIntensity{ get{ return m_RunicMaxIntensity; } set{ m_RunicMaxIntensity = value; } }

	public CraftAttributeInfo()
	{
	}

	public static readonly CraftAttributeInfo Blank;
	public static readonly CraftAttributeInfo Gold, Spike, Fruity, Bronze, IceRock, BlackDwarf, DullCopper, Platinum, SilverRock, DarkPagan, Copper, Mystic, Spectral, OldBritain, Onyx, RedElven, Undead, Pyrite, Virginity, Malachite, Lavarock, Azurite, Dripstone, Executor, Peachblue, Destruction, Anra, Crystal, Doom, Goddess, NewZulu, DarkSableRuby, EbonTwilightSapphire, RadiantNimbusDiamond;

	public static readonly CraftAttributeInfo Spined, Horned, Barbed, Rat, Wolf, Bear, Serpent, Lizard, Troll, Ostard, Necromancer, Lava, Liche, IceCrystal, Dragon, Wyrm, Balron, GoldenDragon;
	public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales;
	public static readonly CraftAttributeInfo Pinetree, Cherry, Oak, PurplePassion, GoldenReflection, Hardranger, Jadewood, Darkwood, Stonewood, Sunwood, Gauntlet, Swampwood, Stardust, Silverleaf, Stormteal, Emeraldwood, Bloodwood, Crystalwood, Bloodhorse, Doomwood, Zulu, Darkness, Elven;

	static CraftAttributeInfo()
	{
	    Blank = new CraftAttributeInfo();

	    CraftAttributeInfo info;

	    info = Gold = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Spike = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Fruity = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Bronze = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = IceRock = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = BlackDwarf = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = DullCopper = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Platinum = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = SilverRock = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = DarkPagan = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Copper = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Mystic = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Spectral = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = OldBritain = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Onyx = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = RedElven = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Undead = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Pyrite = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Virginity = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Malachite = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Lavarock = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Azurite = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Dripstone = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Executor = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Peachblue = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Destruction = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Anra = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Crystal = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Doom = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = Goddess = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = NewZulu = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = DarkSableRuby = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = EbonTwilightSapphire = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }

	    info = RadiantNimbusDiamond = new CraftAttributeInfo();

	    info.ArmorPhysicalResist = 6;
	    info.ArmorDurability = 50;
	    info.ArmorLowerRequirements = 20;
	    info.WeaponDurability = 100;
	    info.WeaponLowerRequirements = 50;
	    info.RunicMinAttributes = 1;
	    info.RunicMaxAttributes = 2;
	    if ( Core.ML )
	    {
		info.RunicMinIntensity = 40;
		info.RunicMaxIntensity = 100;
	    }
	    else
	    {
		info.RunicMinIntensity = 10;
		info.RunicMaxIntensity = 35;
	    }
                        

	    CraftAttributeInfo spined = Spined = new CraftAttributeInfo();

	    spined.ArmorPhysicalResist = 5;
	    spined.ArmorLuck = 40;
	    spined.RunicMinAttributes = 1;
	    spined.RunicMaxAttributes = 3;
	    if ( Core.ML )
	    {
		spined.RunicMinIntensity = 40;
		spined.RunicMaxIntensity = 100;
	    }
	    else
	    {
		spined.RunicMinIntensity = 20;
		spined.RunicMaxIntensity = 40;
	    }

	    CraftAttributeInfo horned = Horned = new CraftAttributeInfo();

	    horned.ArmorPhysicalResist = 2;
	    horned.ArmorFireResist = 3;
	    horned.ArmorColdResist = 2;
	    horned.ArmorPoisonResist = 2;
	    horned.ArmorEnergyResist = 2;
	    horned.RunicMinAttributes = 3;
	    horned.RunicMaxAttributes = 4;
	    if ( Core.ML )
	    {
		horned.RunicMinIntensity = 45;
		horned.RunicMaxIntensity = 100;
	    }
	    else
	    {
		horned.RunicMinIntensity = 30;
		horned.RunicMaxIntensity = 70;
	    }

	    CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();

	    barbed.ArmorPhysicalResist = 2;
	    barbed.ArmorFireResist = 1;
	    barbed.ArmorColdResist = 2;
	    barbed.ArmorPoisonResist = 3;
	    barbed.ArmorEnergyResist = 4;
	    barbed.RunicMinAttributes = 4;
	    barbed.RunicMaxAttributes = 5;
	    if ( Core.ML )
	    {
		barbed.RunicMinIntensity = 50;
		barbed.RunicMaxIntensity = 100;
	    }
	    else
	    {
		barbed.RunicMinIntensity = 40;
		barbed.RunicMaxIntensity = 100;
	    }

	    CraftAttributeInfo rat = Rat = new CraftAttributeInfo();

	    rat.ArmorPhysicalResist = 2;
	    rat.ArmorFireResist = 1;
	    rat.ArmorColdResist = 2;
	    rat.ArmorPoisonResist = 3;
	    rat.ArmorEnergyResist = 4;
	    rat.RunicMinAttributes = 4;
	    rat.RunicMaxAttributes = 5;
	    if ( Core.ML )
	    {
		rat.RunicMinIntensity = 50;
		rat.RunicMaxIntensity = 100;
	    }
	    else
	    {
		rat.RunicMinIntensity = 40;
		rat.RunicMaxIntensity = 100;
	    }

	    CraftAttributeInfo wolf = Wolf = new CraftAttributeInfo();

	    wolf.ArmorPhysicalResist = 2;
	    wolf.ArmorFireResist = 1;
	    wolf.ArmorColdResist = 2;
	    wolf.ArmorPoisonResist = 3;
	    wolf.ArmorEnergyResist = 4;
	    wolf.RunicMinAttributes = 4;
	    wolf.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo bear = Bear = new CraftAttributeInfo();

	    bear.ArmorPhysicalResist = 2;
	    bear.ArmorFireResist = 1;
	    bear.ArmorColdResist = 2;
	    bear.ArmorPoisonResist = 3;
	    bear.ArmorEnergyResist = 4;
	    bear.RunicMinAttributes = 4;
	    bear.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo serpent = Serpent = new CraftAttributeInfo();

	    serpent.ArmorPhysicalResist = 2;
	    serpent.ArmorFireResist = 1;
	    serpent.ArmorColdResist = 2;
	    serpent.ArmorPoisonResist = 3;
	    serpent.ArmorEnergyResist = 4;
	    serpent.RunicMinAttributes = 4;
	    serpent.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo lizard = Lizard = new CraftAttributeInfo();

	    lizard.ArmorPhysicalResist = 2;
	    lizard.ArmorFireResist = 1;
	    lizard.ArmorColdResist = 2;
	    lizard.ArmorPoisonResist = 3;
	    lizard.ArmorEnergyResist = 4;
	    lizard.RunicMinAttributes = 4;
	    lizard.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo troll = Troll = new CraftAttributeInfo();

	    troll.ArmorPhysicalResist = 2;
	    troll.ArmorFireResist = 1;
	    troll.ArmorColdResist = 2;
	    troll.ArmorPoisonResist = 3;
	    troll.ArmorEnergyResist = 4;
	    troll.RunicMinAttributes = 4;
	    troll.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo ostard = Ostard = new CraftAttributeInfo();

	    ostard.ArmorPhysicalResist = 2;
	    ostard.ArmorFireResist = 1;
	    ostard.ArmorColdResist = 2;
	    ostard.ArmorPoisonResist = 3;
	    ostard.ArmorEnergyResist = 4;
	    ostard.RunicMinAttributes = 4;
	    ostard.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo necromancer = Necromancer = new CraftAttributeInfo();

	    necromancer.ArmorPhysicalResist = 2;
	    necromancer.ArmorFireResist = 1;
	    necromancer.ArmorColdResist = 2;
	    necromancer.ArmorPoisonResist = 3;
	    necromancer.ArmorEnergyResist = 4;
	    necromancer.RunicMinAttributes = 4;
	    necromancer.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo lava = Lava = new CraftAttributeInfo();

	    lava.ArmorPhysicalResist = 2;
	    lava.ArmorFireResist = 1;
	    lava.ArmorColdResist = 2;
	    lava.ArmorPoisonResist = 3;
	    lava.ArmorEnergyResist = 4;
	    lava.RunicMinAttributes = 4;
	    lava.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo liche = Liche = new CraftAttributeInfo();

	    liche.ArmorPhysicalResist = 2;
	    liche.ArmorFireResist = 1;
	    liche.ArmorColdResist = 2;
	    liche.ArmorPoisonResist = 3;
	    liche.ArmorEnergyResist = 4;
	    liche.RunicMinAttributes = 4;
	    liche.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo icecrystal = IceCrystal = new CraftAttributeInfo();

	    icecrystal.ArmorPhysicalResist = 2;
	    icecrystal.ArmorFireResist = 1;
	    icecrystal.ArmorColdResist = 2;
	    icecrystal.ArmorPoisonResist = 3;
	    icecrystal.ArmorEnergyResist = 4;
	    icecrystal.RunicMinAttributes = 4;
	    icecrystal.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo dragon = Dragon = new CraftAttributeInfo();

	    dragon.ArmorPhysicalResist = 2;
	    dragon.ArmorFireResist = 1;
	    dragon.ArmorColdResist = 2;
	    dragon.ArmorPoisonResist = 3;
	    dragon.ArmorEnergyResist = 4;
	    dragon.RunicMinAttributes = 4;
	    dragon.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo wyrm = Wyrm = new CraftAttributeInfo();

	    wyrm.ArmorPhysicalResist = 2;
	    wyrm.ArmorFireResist = 1;
	    wyrm.ArmorColdResist = 2;
	    wyrm.ArmorPoisonResist = 3;
	    wyrm.ArmorEnergyResist = 4;
	    wyrm.RunicMinAttributes = 4;
	    wyrm.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo balron = Balron = new CraftAttributeInfo();

	    balron.ArmorPhysicalResist = 2;
	    balron.ArmorFireResist = 1;
	    balron.ArmorColdResist = 2;
	    balron.ArmorPoisonResist = 3;
	    balron.ArmorEnergyResist = 4;
	    balron.RunicMinAttributes = 4;
	    balron.RunicMaxAttributes = 5;
	    
	    CraftAttributeInfo goldendragon = GoldenDragon = new CraftAttributeInfo();

	    goldendragon.ArmorPhysicalResist = 2;
	    goldendragon.ArmorFireResist = 1;
	    goldendragon.ArmorColdResist = 2;
	    goldendragon.ArmorPoisonResist = 3;
	    goldendragon.ArmorEnergyResist = 4;
	    goldendragon.RunicMinAttributes = 4;
	    goldendragon.RunicMaxAttributes = 5;

	    CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

	    red.ArmorFireResist = 10;
	    red.ArmorColdResist = -3;

	    CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

	    yellow.ArmorPhysicalResist = -3;
	    yellow.ArmorLuck = 20;

	    CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

	    black.ArmorPhysicalResist = 10;
	    black.ArmorEnergyResist = -3;

	    CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

	    green.ArmorFireResist = -3;
	    green.ArmorPoisonResist = 10;

	    CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

	    white.ArmorPhysicalResist = -3;
	    white.ArmorColdResist = 10;

	    CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

	    blue.ArmorPoisonResist = -3;
	    blue.ArmorEnergyResist = 10;

	    CraftAttributeInfo PinetreeInfo = Pinetree = new CraftAttributeInfo();
	    PinetreeInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo CherryInfo = Cherry = new CraftAttributeInfo();
	    CherryInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo OakInfo = Oak = new CraftAttributeInfo();
	    OakInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo PurplePassionInfo = PurplePassion = new CraftAttributeInfo();
	    PurplePassionInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo GoldenReflectionInfo = GoldenReflection = new CraftAttributeInfo();
	    GoldenReflectionInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo HardrangerInfo = Hardranger = new CraftAttributeInfo();
	    HardrangerInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo JadewoodInfo = Jadewood = new CraftAttributeInfo();
	    JadewoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo DarkwoodInfo = Darkwood = new CraftAttributeInfo();
	    DarkwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo StonewoodInfo = Stonewood = new CraftAttributeInfo();
	    StonewoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo SunwoodInfo = Sunwood = new CraftAttributeInfo();
	    SunwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo GauntletInfo = Gauntlet = new CraftAttributeInfo();
	    GauntletInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo SwampwoodInfo = Swampwood = new CraftAttributeInfo();
	    SwampwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo StardustInfo = Stardust = new CraftAttributeInfo();
	    StardustInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo SilverleafInfo = Silverleaf = new CraftAttributeInfo();
	    SilverleafInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo StormtealInfo = Stormteal = new CraftAttributeInfo();
	    StormtealInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo EmeraldwoodInfo = Emeraldwood = new CraftAttributeInfo();
	    EmeraldwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo BloodwoodInfo = Bloodwood = new CraftAttributeInfo();
	    BloodwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo CrystalwoodInfo = Crystalwood = new CraftAttributeInfo();
	    CrystalwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo BloodhorseInfo = Bloodhorse = new CraftAttributeInfo();
	    BloodhorseInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo DoomwoodInfo = Doomwood = new CraftAttributeInfo();
	    DoomwoodInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo ZuluInfo = Zulu = new CraftAttributeInfo();
	    ZuluInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo DarknessInfo = Darkness = new CraftAttributeInfo();
	    DarknessInfo.ArmorPhysicalResist = 6;
	    CraftAttributeInfo ElvenInfo = Elven = new CraftAttributeInfo();
	    ElvenInfo.ArmorPhysicalResist = 6;
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

	public int Hue{ get{ return m_Hue; } }
	public int Number{ get{ return m_Number; } }
	public string Name{ get{ return m_Name; } }
	public CraftAttributeInfo AttributeInfo{ get{ return m_AttributeInfo; } }
	public CraftResource Resource{ get{ return m_Resource; } }
	public Type[] ResourceTypes{ get{ return m_ResourceTypes; } }

	public CraftResourceInfo( int hue, int number, string name, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes )
	{
	    m_Hue = hue;
	    m_Number = number;
	    m_Name = name;
	    m_AttributeInfo = attributeInfo;
	    m_Resource = resource;
	    m_ResourceTypes = resourceTypes;

	    for ( int i = 0; i < resourceTypes.Length; ++i )
		CraftResources.RegisterType( resourceTypes[i], resource );
	}
    }

    public class CraftResources
    {
	private static CraftResourceInfo[] m_MetalInfo = new CraftResourceInfo[]
	    {
		new CraftResourceInfo( 0x0, 1053109, "Iron", CraftAttributeInfo.Blank, CraftResource.Iron, typeof( IronIngot ), typeof( IronOre), typeof( Granite ) ),
		new CraftResourceInfo( 2793, 1160000, "Gold", CraftAttributeInfo.Gold, CraftResource.Gold, typeof( GoldIngot ), typeof( GoldOre), typeof( GoldGranite ) ),
		new CraftResourceInfo( 0x4c7, 1160001, "Spike", CraftAttributeInfo.Spike, CraftResource.Spike, typeof( SpikeIngot ), typeof( SpikeOre), typeof( SpikeGranite ) ),
		new CraftResourceInfo( 0x46e, 1160002, "Fruity", CraftAttributeInfo.Fruity, CraftResource.Fruity, typeof( FruityIngot ), typeof( FruityOre), typeof( FruityGranite ) ),
		new CraftResourceInfo( 0x45e, 1160003, "Bronze", CraftAttributeInfo.Bronze, CraftResource.Bronze, typeof( BronzeIngot ), typeof( BronzeOre), typeof( BronzeGranite ) ),
		new CraftResourceInfo( 0x480, 1160004, "Ice rock", CraftAttributeInfo.IceRock, CraftResource.IceRock, typeof( IceRockIngot ), typeof( IceRockOre), typeof( IceRockGranite ) ),
		new CraftResourceInfo( 0x451, 1160005, "Black dwarf", CraftAttributeInfo.BlackDwarf, CraftResource.BlackDwarf, typeof( BlackDwarfIngot ), typeof( BlackDwarfOre), typeof( BlackDwarfGranite ) ),
		new CraftResourceInfo( 0x3ea, 1160006, "Dull copper", CraftAttributeInfo.DullCopper, CraftResource.DullCopper, typeof( DullCopperIngot ), typeof( DullCopperOre), typeof( DullCopperGranite ) ),
		new CraftResourceInfo( 0x457, 1160007, "Platinum", CraftAttributeInfo.Platinum, CraftResource.Platinum, typeof( PlatinumIngot ), typeof( PlatinumOre), typeof( PlatinumGranite ) ),
		new CraftResourceInfo( 0x3e9, 1160008, "Silver rock", CraftAttributeInfo.SilverRock, CraftResource.SilverRock, typeof( SilverRockIngot ), typeof( SilverRockOre), typeof( SilverRockGranite ) ),
		new CraftResourceInfo( 0x46b, 1160009, "Dark pagan", CraftAttributeInfo.DarkPagan, CraftResource.DarkPagan, typeof( DarkPaganIngot ), typeof( DarkPaganOre), typeof( DarkPaganGranite ) ),
		new CraftResourceInfo( 0x602, 1160010, "Copper", CraftAttributeInfo.Copper, CraftResource.Copper, typeof( CopperIngot ), typeof( CopperOre), typeof( CopperGranite ) ),
		new CraftResourceInfo( 0x17f, 1160011, "Mystic", CraftAttributeInfo.Mystic, CraftResource.Mystic, typeof( MysticIngot ), typeof( MysticOre), typeof( MysticGranite ) ),
		new CraftResourceInfo( 2744, 1160012, "Spectral", CraftAttributeInfo.Spectral, CraftResource.Spectral, typeof( SpectralIngot ), typeof( SpectralOre), typeof( SpectralGranite ) ),
		new CraftResourceInfo( 0x852, 1160013, "Old britain", CraftAttributeInfo.OldBritain, CraftResource.OldBritain, typeof( OldBritainIngot ), typeof( OldBritainOre), typeof( OldBritainGranite ) ),
		new CraftResourceInfo( 0x455, 1160014, "Onyx", CraftAttributeInfo.Onyx, CraftResource.Onyx, typeof( OnyxIngot ), typeof( OnyxOre), typeof( OnyxGranite ) ),
		new CraftResourceInfo( 0x4b9, 1160015, "Red elven", CraftAttributeInfo.RedElven, CraftResource.RedElven, typeof( RedElvenIngot ), typeof( RedElvenOre), typeof( RedElvenGranite ) ),
		new CraftResourceInfo( 0x279, 1160016, "Undead", CraftAttributeInfo.Undead, CraftResource.Undead, typeof( UndeadIngot ), typeof( UndeadOre), typeof( UndeadGranite ) ),
		new CraftResourceInfo( 0x6b8, 1160017, "Pyrite", CraftAttributeInfo.Pyrite, CraftResource.Pyrite, typeof( PyriteIngot ), typeof( PyriteOre), typeof( PyriteGranite ) ),
		new CraftResourceInfo( 0x482, 1160018, "Virginity", CraftAttributeInfo.Virginity, CraftResource.Virginity, typeof( VirginityIngot ), typeof( VirginityOre), typeof( VirginityGranite ) ),
		new CraftResourceInfo( 2748, 1160019, "Malachite", CraftAttributeInfo.Malachite, CraftResource.Malachite, typeof( MalachiteIngot ), typeof( MalachiteOre), typeof( MalachiteGranite ) ),
		new CraftResourceInfo( 2747, 1160020, "Lavarock", CraftAttributeInfo.Lavarock, CraftResource.Lavarock, typeof( LavarockIngot ), typeof( LavarockOre), typeof( LavarockGranite ) ),
		new CraftResourceInfo( 0x4df, 1160021, "Azurite", CraftAttributeInfo.Azurite, CraftResource.Azurite, typeof( AzuriteIngot ), typeof( AzuriteOre), typeof( AzuriteGranite ) ),
		new CraftResourceInfo( 2771, 1160022, "Dripstone", CraftAttributeInfo.Dripstone, CraftResource.Dripstone, typeof( DripstoneIngot ), typeof( DripstoneOre), typeof( DripstoneGranite ) ),
		new CraftResourceInfo( 2766, 1160023, "Executor", CraftAttributeInfo.Executor, CraftResource.Executor, typeof( ExecutorIngot ), typeof( ExecutorOre), typeof( ExecutorGranite ) ),
		new CraftResourceInfo( 2769, 1160024, "Peachblue", CraftAttributeInfo.Peachblue, CraftResource.Peachblue, typeof( PeachblueIngot ), typeof( PeachblueOre), typeof( PeachblueGranite ) ),
		new CraftResourceInfo( 2773, 1160025, "Destruction", CraftAttributeInfo.Destruction, CraftResource.Destruction, typeof( DestructionIngot ), typeof( DestructionOre), typeof( DestructionGranite ) ),
		new CraftResourceInfo( 0x48b, 1160026, "Anra", CraftAttributeInfo.Anra, CraftResource.Anra, typeof( AnraIngot ), typeof( AnraOre), typeof( AnraGranite ) ),
		new CraftResourceInfo( 2759, 1160027, "Crystal", CraftAttributeInfo.Crystal, CraftResource.Crystal, typeof( CrystalIngot ), typeof( CrystalOre), typeof( CrystalGranite ) ),
		new CraftResourceInfo( 2772, 1160028, "Doom", CraftAttributeInfo.Doom, CraftResource.Doom, typeof( DoomIngot ), typeof( DoomOre), typeof( DoomGranite ) ),
		new CraftResourceInfo( 2774, 1160029, "Goddess", CraftAttributeInfo.Goddess, CraftResource.Goddess, typeof( GoddessIngot ), typeof( GoddessOre), typeof( GoddessGranite ) ),
		new CraftResourceInfo( 2749, 1160030, "New zulu", CraftAttributeInfo.NewZulu, CraftResource.NewZulu, typeof( NewZuluIngot ), typeof( NewZuluOre), typeof( NewZuluGranite ) ),
		new CraftResourceInfo( 2761, 1160032, "Dark sable ruby", CraftAttributeInfo.DarkSableRuby, CraftResource.DarkSableRuby, typeof( DarkSableRubyIngot ), typeof( DarkSableRubyOre), typeof( DarkSableRubyGranite ) ),
		new CraftResourceInfo( 2760, 1160031, "Ebon twilight sapphire", CraftAttributeInfo.EbonTwilightSapphire, CraftResource.EbonTwilightSapphire, typeof( EbonTwilightSapphireIngot ), typeof( EbonTwilightSapphireOre), typeof( EbonTwilightSapphireGranite ) ),
		new CraftResourceInfo( 2765, 1160033, "Radiant nimbus diamond", CraftAttributeInfo.RadiantNimbusDiamond, CraftResource.RadiantNimbusDiamond, typeof( RadiantNimbusDiamondIngot ), typeof( RadiantNimbusDiamondOre), typeof( RadiantNimbusDiamondGranite ) ),
	    };

	private static CraftResourceInfo[] m_ScaleInfo = new CraftResourceInfo[]
	    {
		new CraftResourceInfo( 0x66D, 1053129, "Red Scales",	CraftAttributeInfo.RedScales,		CraftResource.RedScales,		typeof( RedScales ) ),
		new CraftResourceInfo( 0x8A8, 1053130, "Yellow Scales",	CraftAttributeInfo.YellowScales,	CraftResource.YellowScales,		typeof( YellowScales ) ),
		new CraftResourceInfo( 0x455, 1053131, "Black Scales",	CraftAttributeInfo.BlackScales,		CraftResource.BlackScales,		typeof( BlackScales ) ),
		new CraftResourceInfo( 0x851, 1053132, "Green Scales",	CraftAttributeInfo.GreenScales,		CraftResource.GreenScales,		typeof( GreenScales ) ),
		new CraftResourceInfo( 0x8FD, 1053133, "White Scales",	CraftAttributeInfo.WhiteScales,		CraftResource.WhiteScales,		typeof( WhiteScales ) ),
		new CraftResourceInfo( 0x8B0, 1053134, "Blue Scales",	CraftAttributeInfo.BlueScales,		CraftResource.BlueScales,		typeof( BlueScales ) )
	    };

	private static CraftResourceInfo[] m_LeatherInfo = new CraftResourceInfo[]
	    {
		new CraftResourceInfo( 0x000, 1049353, "Normal", CraftAttributeInfo.Blank, CraftResource.RegularLeather, typeof( Leather ), typeof( Hides ) ),
		new CraftResourceInfo( 0x283, 1049354, "Spined", CraftAttributeInfo.Spined, CraftResource.SpinedLeather, typeof( SpinedLeather ), typeof( SpinedHides ) ),
		new CraftResourceInfo( 0x227, 1049355, "Horned", CraftAttributeInfo.Horned, CraftResource.HornedLeather, typeof( HornedLeather ), typeof( HornedHides ) ),
		new CraftResourceInfo( 0x1C1, 1049356, "Barbed", CraftAttributeInfo.Barbed, CraftResource.BarbedLeather, typeof( BarbedLeather ), typeof( BarbedHides ) ),
		new CraftResourceInfo( 0x7e2, 1160415, "Rat", CraftAttributeInfo.Rat, CraftResource.RatLeather, typeof( RatLeather ), typeof( RatHide ) ), //yes no plural on "hide", can change later --sith TODO
		new CraftResourceInfo( 1102, 1160416, "Wolf", CraftAttributeInfo.Wolf, CraftResource.WolfLeather, typeof( WolfLeather ), typeof( WolfHide ) ),
		new CraftResourceInfo( 44, 1160417, "Bear", CraftAttributeInfo.Bear, CraftResource.BearLeather, typeof( BearLeather ), typeof( BearHide ) ),
		new CraftResourceInfo( 0x8fd, 1160418, "Serpent", CraftAttributeInfo.Serpent, CraftResource.SerpentLeather, typeof( SerpentLeather ), typeof( SerpentHide ) ),
		new CraftResourceInfo( 0x852, 1160419, "Lizard", CraftAttributeInfo.Lizard, CraftResource.LizardLeather, typeof( LizardLeather ), typeof( LizardHide ) ),
		new CraftResourceInfo( 0x54a, 1160420, "Troll", CraftAttributeInfo.Troll, CraftResource.TrollLeather, typeof( TrollLeather ), typeof( TrollHide ) ),
		new CraftResourceInfo( 0x415, 1160421, "Ostard", CraftAttributeInfo.Ostard, CraftResource.OstardLeather, typeof( OstardLeather ), typeof( OstardHide ) ),
		new CraftResourceInfo( 84, 1160422, "Necromancer", CraftAttributeInfo.Necromancer, CraftResource.NecromancerLeather, typeof( NecromancerLeather ), typeof( NecromancerHide ) ),
		new CraftResourceInfo( 2747, 1160423, "Lava", CraftAttributeInfo.Lava, CraftResource.LavaLeather, typeof( LavaLeather ), typeof( LavaHide ) ),
		new CraftResourceInfo( 2763, 1160424, "Liche", CraftAttributeInfo.Liche, CraftResource.LicheLeather, typeof( LicheLeather ), typeof( LicheHide ) ),
		new CraftResourceInfo( 2759, 1160425, "Ice Crystal", CraftAttributeInfo.IceCrystal, CraftResource.IceCrystalLeather, typeof( IceCrystalLeather ), typeof( IceCrystalHide ) ),
		new CraftResourceInfo( 2761, 1160426, "Dragon", CraftAttributeInfo.Dragon, CraftResource.DragonLeather, typeof( DragonLeather ), typeof( DragonHide ) ),
		new CraftResourceInfo( 2747, 1160427, "Wyrm", CraftAttributeInfo.Wyrm, CraftResource.WyrmLeather, typeof( WyrmLeather ), typeof( WyrmHide ) ),
		new CraftResourceInfo( 1175, 1160428, "Balron", CraftAttributeInfo.Balron, CraftResource.BalronLeather, typeof( BalronLeather ), typeof( BalronHide ) ),
		new CraftResourceInfo( 48, 1160429, "Golden Dragon", CraftAttributeInfo.GoldenDragon, CraftResource.GoldenDragonLeather, typeof( GoldenDragonLeather ), typeof( GoldenDragonHide ) )
	    };

	private static CraftResourceInfo[] m_AOSLeatherInfo = new CraftResourceInfo[]
	    {
		new CraftResourceInfo( 0x000, 1049353, "Normal",		CraftAttributeInfo.Blank,		CraftResource.RegularLeather,	typeof( Leather ),			typeof( Hides ) ),
		new CraftResourceInfo( 0x8AC, 1049354, "Spined",		CraftAttributeInfo.Spined,		CraftResource.SpinedLeather,	typeof( SpinedLeather ),	typeof( SpinedHides ) ),
		new CraftResourceInfo( 0x845, 1049355, "Horned",		CraftAttributeInfo.Horned,		CraftResource.HornedLeather,	typeof( HornedLeather ),	typeof( HornedHides ) ),
		new CraftResourceInfo( 0x851, 1049356, "Barbed",		CraftAttributeInfo.Barbed,		CraftResource.BarbedLeather,	typeof( BarbedLeather ),	typeof( BarbedHides ) ),
	    };

	private static CraftResourceInfo[] m_WoodInfo = new CraftResourceInfo[]
	    {
		new CraftResourceInfo( 0x000, 1011542, "Normal",		CraftAttributeInfo.Blank,		CraftResource.RegularWood,	typeof( Log ),			typeof( Board ) ),
		new CraftResourceInfo( 1132, 1160034, "Pinetree", CraftAttributeInfo.Pinetree, CraftResource.Pinetree, typeof( PinetreeLog ), typeof( PinetreeBoard) ),
		new CraftResourceInfo( 2206, 1160035, "Cherry", CraftAttributeInfo.Cherry, CraftResource.Cherry, typeof( CherryLog ), typeof( CherryBoard) ),
		new CraftResourceInfo( 1045, 1160036, "Oak", CraftAttributeInfo.Oak, CraftResource.Oak, typeof( OakLog ), typeof( OakBoard) ),
		new CraftResourceInfo( 515, 1160037, "Purple Passion", CraftAttributeInfo.PurplePassion, CraftResource.PurplePassion, typeof( PurplePassionLog ), typeof( PurplePassionBoard) ),
		new CraftResourceInfo( 48, 1160038, "Golden Reflection", CraftAttributeInfo.GoldenReflection, CraftResource.GoldenReflection, typeof( GoldenReflectionLog ), typeof( GoldenReflectionBoard) ),
		new CraftResourceInfo( 2778, 1160039, "Hardranger", CraftAttributeInfo.Hardranger, CraftResource.Hardranger, typeof( HardrangerLog ), typeof( HardrangerBoard) ),
		new CraftResourceInfo( 1162, 1160040, "Jadewood", CraftAttributeInfo.Jadewood, CraftResource.Jadewood, typeof( JadewoodLog ), typeof( JadewoodBoard) ),
		new CraftResourceInfo( 1109, 1160041, "Darkwood", CraftAttributeInfo.Darkwood, CraftResource.Darkwood, typeof( DarkwoodLog ), typeof( DarkwoodBoard) ),
		new CraftResourceInfo( 1154, 1160042, "Stonewood", CraftAttributeInfo.Stonewood, CraftResource.Stonewood, typeof( StonewoodLog ), typeof( StonewoodBoard) ),
		new CraftResourceInfo( 2766, 1160043, "Sunwood", CraftAttributeInfo.Sunwood, CraftResource.Sunwood, typeof( SunwoodLog ), typeof( SunwoodBoard) ),
		new CraftResourceInfo( 2777, 1160044, "Gauntlet", CraftAttributeInfo.Gauntlet, CraftResource.Gauntlet, typeof( GauntletLog ), typeof( GauntletBoard) ),
		new CraftResourceInfo( 2767, 1160045, "Swampwood", CraftAttributeInfo.Swampwood, CraftResource.Swampwood, typeof( SwampwoodLog ), typeof( SwampwoodBoard) ),
		new CraftResourceInfo( 2751, 1160046, "Stardust", CraftAttributeInfo.Stardust, CraftResource.Stardust, typeof( StardustLog ), typeof( StardustBoard) ),
		new CraftResourceInfo( 2301, 1160047, "Silver leaf", CraftAttributeInfo.Silverleaf, CraftResource.Silverleaf, typeof( SilverleafLog ), typeof( SilverleafBoard) ),
		new CraftResourceInfo( 1346, 1160048, "Stormteal", CraftAttributeInfo.Stormteal, CraftResource.Stormteal, typeof( StormtealLog ), typeof( StormtealBoard) ),
		new CraftResourceInfo( 2748, 1160049, "Emerald wood", CraftAttributeInfo.Emeraldwood, CraftResource.Emeraldwood, typeof( EmeraldwoodLog ), typeof( EmeraldwoodBoard) ),
		new CraftResourceInfo( 1645, 1160050, "Bloodwood", CraftAttributeInfo.Bloodwood, CraftResource.Bloodwood, typeof( BloodwoodLog ), typeof( BloodwoodBoard) ),
		new CraftResourceInfo( 2759, 1160051, "Crystal wood", CraftAttributeInfo.Crystalwood, CraftResource.Crystalwood, typeof( CrystalwoodLog ), typeof( CrystalwoodBoard) ),
		new CraftResourceInfo( 2780, 1160052, "Bloodhorse", CraftAttributeInfo.Bloodhorse, CraftResource.Bloodhorse, typeof( BloodhorseLog ), typeof( BloodhorseBoard) ),
		new CraftResourceInfo( 2772, 1160053, "Doom wood", CraftAttributeInfo.Doomwood, CraftResource.Doomwood, typeof( DoomwoodLog ), typeof( DoomwoodBoard) ),
		new CraftResourceInfo( 2749, 1160054, "Zulu", CraftAttributeInfo.Zulu, CraftResource.Zulu, typeof( ZuluLog ), typeof( ZuluBoard) ),
		new CraftResourceInfo( 1175, 1160055, "Darkness", CraftAttributeInfo.Darkness, CraftResource.Darkness, typeof( DarknessLog ), typeof( DarknessBoard) ),
		new CraftResourceInfo( 1165, 1160056, "Elven", CraftAttributeInfo.Elven, CraftResource.Elven, typeof( ElvenLog ), typeof( ElvenBoard) ),
	    };

	/// <summary>
	/// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or RegularWood. False if otherwise.
	/// </summary>
	public static bool IsStandard( CraftResource resource )
	{
	    return ( resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather || resource == CraftResource.RegularWood );
	}

	private static Dictionary<Type, CraftResource> m_TypeTable;

	/// <summary>
	/// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
	/// </summary>
	public static void RegisterType( Type resourceType, CraftResource resource )
	{
	    if ( m_TypeTable == null )
		m_TypeTable = new Dictionary<Type, CraftResource>();

	    m_TypeTable[resourceType] = resource;
	}

	/// <summary>
	/// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
	/// </summary>
	public static CraftResource GetFromType( Type resourceType )
	{
	    if ( m_TypeTable == null )
		return CraftResource.None;

	    CraftResource res;
			
	    if (!m_TypeTable.TryGetValue(resourceType, out res))
		return CraftResource.None;

	    return res;
	}

	/// <summary>
	/// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
	/// </summary>
	public static CraftResourceInfo GetInfo( CraftResource resource )
	{
	    CraftResourceInfo[] list = null;

	    switch ( GetType( resource ) )
	    {
		case CraftResourceType.Metal: list = m_MetalInfo; break;
		case CraftResourceType.Leather: list = m_LeatherInfo; break;
		case CraftResourceType.Scales: list = m_ScaleInfo; break;
		case CraftResourceType.Wood: list = m_WoodInfo; break;
	    }

	    if ( list != null )
	    {
		int index = GetIndex( resource );

		if ( index >= 0 && index < list.Length )
		    return list[index];
	    }

	    return null;
	}

	/// <summary>
	/// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
	/// </summary>
	public static CraftResourceType GetType( CraftResource resource )
	{
	    if ( resource >= CraftResource.Iron && resource <= CraftResource.RadiantNimbusDiamond )
		return CraftResourceType.Metal;

	    if ( resource >= CraftResource.RegularLeather && resource <= CraftResource.GoldenDragonLeather )
		return CraftResourceType.Leather;

	    if ( resource >= CraftResource.RedScales && resource <= CraftResource.BlueScales )
		return CraftResourceType.Scales;

	    if ( resource >= CraftResource.RegularWood && resource <= CraftResource.Elven )
		return CraftResourceType.Wood;

	    return CraftResourceType.None;
	}

	/// <summary>
	/// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
	/// </summary>
	public static CraftResource GetStart( CraftResource resource )
	{
	    switch ( GetType( resource ) )
	    {
		case CraftResourceType.Metal: return CraftResource.Iron;
		case CraftResourceType.Leather: return CraftResource.RegularLeather;
		case CraftResourceType.Scales: return CraftResource.RedScales;
		case CraftResourceType.Wood: return CraftResource.RegularWood;
	    }

	    return CraftResource.None;
	}

	/// <summary>
	/// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
	/// </summary>
	public static int GetIndex( CraftResource resource )
	{
	    CraftResource start = GetStart( resource );

	    if ( start == CraftResource.None )
		return 0;

	    return (int)(resource - start);
	}

	/// <summary>
	/// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
	/// </summary>
	public static int GetLocalizationNumber( CraftResource resource )
	{
	    CraftResourceInfo info = GetInfo( resource );

	    return ( info == null ? 0 : info.Number );
	}

	/// <summary>
	/// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
	/// </summary>
	public static int GetHue( CraftResource resource )
	{
	    CraftResourceInfo info = GetInfo( resource );

	    return ( info == null ? 0 : info.Hue );
	}

	/// <summary>
	/// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
	/// </summary>
	public static string GetName( CraftResource resource )
	{
	    CraftResourceInfo info = GetInfo( resource );

	    return ( info == null ? String.Empty : info.Name );
	}

    }

}
