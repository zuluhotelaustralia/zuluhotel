using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;

/* this system is for the Zulu Hotel "magic clothing" feature and implements 
 * an easy way to spawn clothes with skill or stat mods in loot
 */

namespace Server{
    public class MagicClothing{

	public static void Initialize() {
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

	private const double _statmodchance = 0.15; //chance to get a stat mod
	private const double _cursedchance = 0.33; //chance that the loot is cursed
	private const double _combatweight = 0.33;
	private const double _craftingweight = 0.1;
	private const SkillName _minskillID = SkillName.Alchemy; //0 
	private const SkillName _maxskillID = SkillName.RemoveTrap;// 48

	public enum ModType{
	    Stat,
	    Skill
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
		typeof( GoldBracelet ), typeof( SilverRing ), typeof( SilverBracelet )
	    };

	public static Type[] AllowedTypes{ get{ return m_AllowedTypes; } }

	private static ModType DecideStatSkill(){
	    double r = Utility.RandomDouble();
	    if( r > _statmodchance ){
		return ModType.Skill;
	    }
	    else {
		return ModType.Stat;
	    }
	}

	//return true if item should be cursed
	private static bool DecideCursed() {
	    double r = Utility.RandomDouble();
	    if( r > _cursedchance ){
		return false;
	    }
	    else {
		return true;
	    }
	}

	private static StatType DecideStat() {
	    double r = Utility.RandomDouble();
	    if( r > 0.66 ){
		return StatType.Dex;
	    }
	    if( r <= 0.33 ){
		return StatType.Str;
	    }

	    return StatType.Int;
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
	    
	    if( 0.98 <= r && max >= 20){
		return 20;
	    }
	    else if( 0.90 <= r && r < 0.98 && max >= 15 ){
		return 15;
	    }
	    else if( 0.80 <= r && r < 0.90 && max >= 10 ){
		return 10;
	    }
	    else if( 0.70 <= r && r < 0.80 && max >= 5 ){
		return 5;
	    }
	    else if( 0.55 <= r && r < 0.70 && max >= 2 ){
		return 2;
	    }
	    else if( 0.30 <= r && r < 0.55 && max >= 1 ){
		return 1;
	    }
	    else {
		return 0; //30% of the time
	    }
	}

	public static void getNamePrefix( ref Item item ){
	    
	}

	public static Item Generate( int maxbonus ){
	    Type itemtype = DecideType();
	    ModType statskill = DecideStatSkill();
	    int modamount = DecideAmount( maxbonus );
	    StatType thestat = DecideStat();
	    SkillName sn = DecideSkill();
	    Item theitem;

	    try {
		theitem = (Item)Activator.CreateInstance( itemtype );
	    }
	    catch {
		return null;
	    }

	    if( theitem is BaseClothing ) {
		BaseClothing clothes = theitem as BaseClothing;
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
		else {
		    //statskill == modtype.skill
		    clothes.ZuluSkillMods.SetMod( sn, (double)modamount );
		}
	    }
	    else if( theitem is BaseJewel ){
		BaseJewel jewel = theitem as BaseJewel;
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
