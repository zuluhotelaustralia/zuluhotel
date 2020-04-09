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

	    FancyHues = new List<int> { 0x480, 0x17f, 2744, 2748, 2747, 2771, 2769, 2773, 2759, 2772, 2774, 2749, 2761, 2760, 2765, 2766, 2778, 2777 };
	    
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
	    Stat = 0,
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

	private static ModType DecideEnchantment( List<ModType> modtypes ){
	    int mod = Utility.Random( 0, modtypes.Count ); //returns [0,count) i.e. exclusive of 2nd argument
	    return modtypes[mod];
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
	    if( r < 0.20 ){
		return StatType.Dex;
	    }
	    else if( r < 0.50 ){
		return StatType.Str;
	    }
	    else {
		return StatType.Int;
	    }
	}

	public static List<int> FancyHues; 
	    
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
		    {
			if( Utility.RandomDouble() > 0.98 ){
			    return FancyHues[ Utility.Random( FancyHues.Count ) ];
			}
			return Utility.RandomDyedHue();
		    }
	    }
	}	    

	// so clothing could have up to 3 types:  skill or stat, prot, and armor
	// weapons and actual armor can have just skill or stat 
	// jewels can have prot, skill or stat, and armor
	// so if decidenumber returns less than 3, need to exclude basearmor/baseweapon
	private static Type DecideType( int num ) {
	    // we can have the following types:  rangedweapon, weapon, armor, shield, jewelry, clothing
	    // ranged weapons and shields constitute a small fraction of weapons and shields, respectively
	    double roll = Utility.RandomDouble();
	    switch( num ){
		case 3:
		    if( roll < 0.5 ){
			return Loot.ClothingTypes[ Utility.Random( 0, Loot.ClothingTypes.Length - 1 )];
		    }
		    else {
			return Loot.JewelryTypes[ Utility.Random( 0, Loot.JewelryTypes.Length - 1 )];
		    }
		case 2:
		    if( roll < 0.33 ){
			if( Utility.RandomDouble() <= 0.20 ){
			    return Loot.RangedWeaponTypes[ Utility.Random( 0, Loot.RangedWeaponTypes.Length - 1 )];
			}
			else{
			    return Loot.WeaponTypes[ Utility.Random( 0, Loot.WeaponTypes.Length - 1 )];
			}
		    }
		    else if( roll < 0.66 ){
			return Loot.ClothingTypes[ Utility.Random( 0, Loot.ClothingTypes.Length - 1 )];
		    }
		    else {
			return Loot.JewelryTypes[ Utility.Random( 0, Loot.JewelryTypes.Length - 1 )];
		    }
		case 1:
		default:
		    if( roll < 0.25 ){
			if( Utility.RandomDouble() <= 0.20 ){
			    return Loot.ShieldTypes[ Utility.Random( 0, Loot.ShieldTypes.Length - 1 )];
			}
			else{
			    return Loot.ArmorTypes[ Utility.Random( 0, Loot.ArmorTypes.Length - 1 )];
			}
		    }
		    else if( roll < 0.5 ){
			if( Utility.RandomDouble() <= 0.20 ){
			    return Loot.RangedWeaponTypes[ Utility.Random( 0, Loot.RangedWeaponTypes.Length - 1 )];
			}
			else{
			    return Loot.WeaponTypes[ Utility.Random( 0, Loot.WeaponTypes.Length - 1 )];
			}
		    }
		    else if( roll < 0.75 ){
			return Loot.ClothingTypes[ Utility.Random( 0, Loot.ClothingTypes.Length - 1 )];
		    }
		    else {
			return Loot.JewelryTypes[ Utility.Random( 0, Loot.JewelryTypes.Length - 1 )];
		    }
	    }
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

	public static int DecideNumber() {
	    double roll = Utility.RandomDouble();
	    if( roll < 0.6 ){
		return 1;
	    }
	    else if( roll < 0.9 ){
		return 2;
	    }
	    else{
		return 3;
	    }
	}
	
	public static Item Generate( int maxbonus ){
	    int numberofenchants = DecideNumber();
	    Type itemtype = DecideType( numberofenchants );

	    Item theitem;
	    
	    try {
		theitem = (Item)Activator.CreateInstance( itemtype );
	    }
	    catch {
		Console.WriteLine("FIXME: MagicClothing Engine");
		return null;
	    }
	    
	    List<ModType> usedmods = new List<ModType>{ ModType.Armor, ModType.Stat, ModType.Skill, ModType.Prot };
	    if( theitem is BaseWeapon || theitem is BaseArmor ){
		if( numberofenchants > 2 ){
		    Console.WriteLine( "Houston, we have a problem");
		}

		usedmods.Remove(ModType.Armor);
		usedmods.Remove(ModType.Prot); // no prots on armor because we're gonna make elemental suits later
	    }
	    
	    for( int i=0; i< numberofenchants; i++ ){
		ModType themod = DecideEnchantment( usedmods );
		usedmods.Remove( themod );

		int modamount = DecideAmount( maxbonus );
		StatType thestat = DecideStat();
		SkillName sn = DecideSkill();
	    
		if( theitem is BaseClothing ) {
		    BaseClothing clothes = theitem as BaseClothing;
		    clothes.Identified = false;
		
		    if( themod == ModType.Stat ){
			switch( thestat ){
			    case StatType.Str:
				clothes.StrBonus = modamount;
				break;
			    case StatType.Dex:
				clothes.DexBonus = modamount;
				break;
			    case StatType.Int:
				clothes.IntBonus = modamount;
				break;
			}
		    }
		    else if( themod == ModType.Prot ){
			DamageType dt = (DamageType) Utility.RandomMinMax( (int)DamageType.Air, (int)DamageType.Poison);
			clothes.Prot = new Prot( dt, modamount );
			//clothes.Hue = DecideHue( dt );
		    }
		    else if( themod == ModType.Armor ){
			//clothes.Hue = 2406;
			clothes.VirtualArmorMod = modamount;
		    }
		    else {
			//themod == modtype.skill
			clothes.ZuluSkillMods.SetMod( sn, (double)modamount );
		    }
		}
		else if( theitem is BaseWeapon ){
		    BaseWeapon weap = theitem as BaseWeapon;
		    weap.Identified = false;
		    if( themod == ModType.Stat ){
			switch( thestat ){
			    case StatType.Str:
				weap.StrBonus = modamount;
				break;
			    case StatType.Dex:
				weap.StrBonus = modamount;
				break;
			    case StatType.Int:
				weap.StrBonus = modamount;
				break;
			}
		    }
		    else {
			weap.ZuluSkillMods.SetMod( sn, (double) modamount );
		    }
		}
		else if( theitem is BaseArmor ){
		    BaseArmor armr = theitem as BaseArmor;
		    armr.Identified = false;
		    if( themod == ModType.Stat ){
			switch( thestat ){
			    case StatType.Str:
				armr.StrBonus = modamount;
				break;
			    case StatType.Dex:
				armr.StrBonus = modamount;
				break;
			    case StatType.Int:
				armr.StrBonus = modamount;
				break;
			}
		    }
		    else {
			armr.ZuluSkillMods.SetMod( sn, (double) modamount );
		    }
		}	
		else if( theitem is BaseJewel ){
		    BaseJewel jewel = theitem as BaseJewel;
		    jewel.Identified = false;
		    if( themod == ModType.Stat ){
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
		    else if( themod == ModType.Armor ){
			//jewel.Hue = 2406;
			jewel.VirtualArmorMod = modamount;
		    }
		    else {
			//themod == modtype.skill
			jewel.ZuluSkillMods.SetMod( sn, (double)modamount );
		    }
		}
	    }
	    
	    return theitem;
	}
    }
    
    public class MagicClothingDummyType{
	public MagicClothingDummyType () {}
    }
}
