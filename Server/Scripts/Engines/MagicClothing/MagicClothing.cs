using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;

/* this system is for the Zulu Hotel "magic clothing" feature and implements 
 * an easy way to spawn clothes with skill or stat mods in loot
 */

namespace Server{
    public class MagicClothing{

	[Usage("SetSkillMod <skillID> <amount>")]
	[Description("Set item skill mods")]
	public static void SetSkillMod_OnCommand( CommandEventArgs e ){
	    if( e.Length != 2 ){
		e.Mobile.SendMessage("Example:  SetSkillMod EvalInt 15.0");
	    }
	    else{
		SkillName sn;
		if( Enum.TryParse( e.GetString( 0 ), true, out sn ) ){
		    double amount = e.GetDouble( 1 );
		    e.Mobile.Target = new SetSkillmodTarget( sn, amount );
		}
	    }
	}

	public static void GetSkillMod_OnCommand( CommandEventArgs e ){
	    e.Mobile.Target = new GetSkillmodTarget();
	}
		
	public static void Initialize() {
	    CommandSystem.Register( "GetSkillMod", AccessLevel.GameMaster, new CommandEventHandler( GetSkillMod_OnCommand ) );
	    CommandSystem.Register( "SetSKillMod", AccessLevel.Administrator, new CommandEventHandler( SetSkillMod_OnCommand ) );
	    
	    m_CraftingSkills = new List<SkillName>();

	    m_CraftingSkills.Add(SkillName.Alchemy);
	    m_CraftingSkills.Add(SkillName.Blacksmith);
	    m_CraftingSkills.Add(SkillName.Mining);
	    m_CraftingSkills.Add(SkillName.Tinkering);
	    m_CraftingSkills.Add(SkillName.Lumberjacking);
	    m_CraftingSkills.Add(SkillName.Carpentry);
	    m_CraftingSkills.Add(SkillName.Cooking);
	    m_CraftingSkills.Add(SkillName.Fletching);
	    m_CraftingSkills.Add(SkillName.ArmsLore);
	    m_CraftingSkills.Add(SkillName.Tailoring);
	    m_CraftingSkills.Add(SkillName.Fishing);
	    m_CraftingSkills.Add(SkillName.Inscribe);
	    
	    m_CombatSkills = new List<SkillName>();

	    m_CombatSkills.Add(SkillName.Anatomy);
	    m_CombatSkills.Add(SkillName.Healing);
	    m_CombatSkills.Add(SkillName.Fencing);
	    m_CombatSkills.Add(SkillName.Swords);
	    m_CombatSkills.Add(SkillName.Macing);
	    m_CombatSkills.Add(SkillName.Tactics);
	    m_CombatSkills.Add(SkillName.Archery);
	    m_CombatSkills.Add(SkillName.Wrestling);
	    m_CombatSkills.Add(SkillName.Parry);
	    
	    m_OtherSkills = new List<SkillName>();

	    m_OtherSkills.Add(SkillName.AnimalLore);
	    m_OtherSkills.Add(SkillName.AnimalTaming);
	    m_OtherSkills.Add(SkillName.Begging);
	    m_OtherSkills.Add(SkillName.ItemID);
	    m_OtherSkills.Add(SkillName.Peacemaking);

	    m_OtherSkills.Add(SkillName.Camping);
	    m_OtherSkills.Add(SkillName.Cartography);
	    m_OtherSkills.Add(SkillName.DetectHidden);
	    m_OtherSkills.Add(SkillName.Discordance);
	    m_OtherSkills.Add(SkillName.EvalInt);
	    m_OtherSkills.Add(SkillName.Forensics);
	    m_OtherSkills.Add(SkillName.Herding);
	    m_OtherSkills.Add(SkillName.Hiding);
	    m_OtherSkills.Add(SkillName.Provocation);
	    m_OtherSkills.Add(SkillName.Lockpicking);
	    m_OtherSkills.Add(SkillName.Magery);
	    m_OtherSkills.Add(SkillName.MagicResist);
	    m_OtherSkills.Add(SkillName.Snooping);
	    m_OtherSkills.Add(SkillName.Musicianship);
	    m_OtherSkills.Add(SkillName.Poisoning);
	    m_OtherSkills.Add(SkillName.SpiritSpeak);
	    m_OtherSkills.Add(SkillName.Stealing);
	    m_OtherSkills.Add(SkillName.TasteID);
	    m_OtherSkills.Add(SkillName.Tracking);
	    m_OtherSkills.Add(SkillName.Veterinary);
	    m_OtherSkills.Add(SkillName.Meditation);
	    m_OtherSkills.Add(SkillName.Stealth);
	    m_OtherSkills.Add(SkillName.RemoveTrap);
	}

	private const double _statmodchance = 0.25; //chance to get a stat mod
	private const double _skillmodchance = 0.25;
	private const double _protchance = 0.25;
	private const double _armorchance = 0.25;
	private const double _cursedchance = 0.33; //chance that the loot is cursed
	private const double _combatweight = 0.33;
	private const double _craftingweight = 0.1;
	private const SkillName _minskillID = SkillName.Alchemy; //0 
	private const SkillName _maxskillID = SkillName.RemoveTrap;// 48

	public enum ModType{
	    Stat,
	    Skill,
	    Prot,
	    Armor
	}

	private static List<SkillName> m_CraftingSkills;
	public static List<SkillName> CraftingSkills{
	    get { return m_CraftingSkills; }
	}

	private static List<SkillName> m_CombatSkills;
	public static List<SkillName> CombatSkills{
	    get { return m_CombatSkills; }
	}

	private static List<SkillName> m_OtherSkills;
	public static List<SkillName> OtherSkills{
	    get { return m_OtherSkills; }
	}

	private static Type[] m_AllowedTypes = new Type[]
	    {
		typeof( Cloak ), typeof( Bonnet ), typeof( Cap ), typeof( FeatheredHat ), typeof( FloppyHat ), typeof( JesterHat ), typeof( Surcoat ),
		typeof( SkullCap ), typeof( StrawHat ),	typeof( TallStrawHat ), typeof( TricorneHat ), typeof( WideBrimHat ), typeof( WizardsHat ),
		typeof( BodySash ), typeof( Doublet ), typeof( Boots ), typeof( FullApron ), typeof( JesterSuit ), typeof( Sandals ), typeof( Tunic ),
		typeof( Shoes ), typeof( Shirt ), typeof( Kilt ), typeof( Skirt ), typeof( FancyShirt ), typeof( FancyDress ), typeof( ThighBoots ),
		typeof( LongPants ), typeof( PlainDress ), typeof( Robe ), typeof( ShortPants ), typeof( HalfApron ), typeof( GoldRing ),
		typeof( GoldBracelet ), typeof( SilverRing ), typeof( SilverBracelet ), typeof( Necklace ), 
		typeof( GoldNecklace ), typeof( GoldBeadNecklace ), typeof( SilverNecklace ), typeof( SilverBeadNecklace )
	    };

	public static Type[] AllowedTypes{ get{ return m_AllowedTypes; } }

	private static ModType DecideEnchantment(){
	    double r = Utility.RandomDouble();
	    if( r < _statmodchance ){
		return ModType.Stat;
	    }
	    else if ( r < _protchance + _statmodchance ){
		return ModType.Prot;
	    }
	    else if( r < _armorchance + _protchance + _statmodchance ){
		return ModType.Armor;
	    }
	    else {
		return ModType.Skill;
	    }
	}

	//return true if item should be cursed
	private static bool DecideCursed() {
	    return false; // I don't have the OnSingleClick shit done TODO sith

	    /*
	    double r = Utility.RandomDouble();
	    if( r > _cursedchance ){
		return false;
	    }
	    else {
		return true;
	    }
	    */
	}

	private static StatType DecideStat() {
	    double r = Utility.RandomDouble();
	    if( r > 0.80 ){
		return StatType.Dex;
	    }
	    if( r <= 0.30 ){
		return StatType.Str;
	    }

	    return StatType.Int;
	}

	public static int DecideHue( DamageType dt ) {
	    switch( dt ){
		case DamageType.Air:
		    return 1361;
		case DamageType.Earth:
		    return 2749;
		case DamageType.Fire:
		    return 1360;
		case DamageType.Water:
		    return 2756;
		case DamageType.Necro:
		    return 1373;
		case DamageType.Poison:
		    return 1372;
		default:
		    return 0;
	    }
	}	    
	
	private static Type DecideType() {
	    int numtypes = m_AllowedTypes.Length;
	    int index = Utility.Random(numtypes);
	    index --;
	    
	    return m_AllowedTypes[index];
	}
	
	private static SkillName DecideSkill() {
	    double r = Utility.RandomDouble();
	    int index;
	    if( r <= _craftingweight ){
		//make crafting items
		index = Utility.Random(m_CraftingSkills.Count);
		return m_CraftingSkills[index];
	    }
	    else if( r > _craftingweight && r <= ( _craftingweight + _combatweight ) ){
		//make combat item
		index = Utility.Random(m_CombatSkills.Count);
		return m_CombatSkills[index];
	    }
	    else {
		//make item from residual skills
		index = Utility.Random(m_OtherSkills.Count);
		return m_OtherSkills[index];
	    }

	}

	private static int DecideAmount( int max ) {
	    double r = Utility.RandomDouble();

	    if( 0.90 <= r && max >= 6){
		return 6;
	    }
	    else if( 0.80 <= r && r < 0.90 && max >= 5 ){
		return 5;
	    }
	    else if( 0.65 <= r && r < 0.80 && max >= 4 ){
		return 4;
	    }
	    else if( 0.45 <= r && r < 0.65 && max >= 3 ){
		return 3;
	    }
	    else if( 0.20 <= r && r < 0.45 && max >= 2 ){
		return 2;
	    }
	    else {
		return 1;
	    }
	}

	public static void getNamePrefix( ref Item item ){
	    
	}

	public static Item Generate( int maxbonus ){
	    Type itemtype = DecideType();
	    ModType statskill = DecideEnchantment();
	    int modamount = DecideAmount( maxbonus );
	    StatType thestat = DecideStat();
	    SkillName sn = DecideSkill();
	    Item theitem;

	    try {
		theitem = (Item)Activator.CreateInstance( itemtype );
	    }
	    catch {
		Console.WriteLine("FIXME: MagicClothing Engine");
		return null;
	    }

	    //Console.WriteLine(" MagicClothing engine: {0} {1} {2} {3} {4} {5}", itemtype.ToString(), statskill.ToString(), modamount, thestat.ToString(), sn, theitem.GetType() );

	    if( statskill == ModType.Prot ){
		//temporarily (TODO SITH)
		// doing this because BaseJewel doesn't take a Prot object
		// since you could just make prot earrings from gathered restources
		itemtype = typeof( Robe );
	    }
	    
	    if( theitem is BaseClothing ) {
		BaseClothing clothes = theitem as BaseClothing;
		clothes.Identified = false;
		
		if( statskill == ModType.Stat ){
		    switch( thestat ){
			case StatType.Str:
			    clothes.StrBonus = modamount;
			    break;
			case StatType.Dex:
			    clothes.StrBonus = modamount;
			    break;
			case StatType.Int:
			    clothes.StrBonus = modamount;
			    break;
		    }
		}
		else if( statskill == ModType.Prot ){
		    DamageType dt = (DamageType) Utility.RandomMinMax( (int)DamageType.Air, (int)DamageType.Poison);
		    clothes.Prot = new Prot( dt, modamount );
		    //clothes.Hue = DecideHue( dt );
		}
		else if( statskill == ModType.Armor ){
		    //clothes.Hue = 2406;
		    clothes.VirtualArmorMod = modamount;
		}
		else {
		    //statskill == modtype.skill
		    clothes.ZuluSkillMods.SetMod( sn, (double)modamount );
		}
	    }
	    else if( theitem is BaseJewel ){
		BaseJewel jewel = theitem as BaseJewel;
		jewel.Identified = false;
		if( statskill == ModType.Stat ){
		    switch( thestat ){
			case StatType.Str:
			    jewel.StrBonus = modamount;
			    break;
			case StatType.Dex:
			    jewel.StrBonus = modamount;
			    break;
			case StatType.Int:
			    jewel.StrBonus = modamount;
			    break;
		    }
		}
		else if( statskill == ModType.Armor ){
		    //jewel.Hue = 2406;
		    jewel.VirtualArmorMod = modamount;
		}
		else {
		    //statskill == modtype.skill
		    jewel.ZuluSkillMods.SetMod( sn, (double)modamount );
		}
	    }

	    return theitem;
	}
    }

    public class MagicClothingDummyType{
	public MagicClothingDummyType () {}
    }
}
