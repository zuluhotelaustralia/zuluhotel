using System;

using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server {
    public class Prots {
	public static void Initialize(){
	    CommandSystem.Register("GetProts", AccessLevel.GameMaster, new CommandEventHandler( GetProts_OnCommand ) );
	    CommandSystem.Register("Prots", AccessLevel.Player, new CommandEventHandler( Prots_OnCommand ) );
	}

	public static void Prots_OnCommand( CommandEventArgs e ){
	    PlayerMobile pm = e.Mobile as PlayerMobile;
	    pm.Prots.UpdateProts();
	    pm.SendMessage("Air: {0}, Earth: {1}, Fire: {2}, Necro: {3}, Water: {4}", pm.Prots.Air, pm.Prots.Earth, pm.Prots.Fire, pm.Prots.Necro, pm.Prots.Water );
	}
	    
	public static void GetProts_OnCommand( CommandEventArgs e ){
	    PlayerMobile pm = e.Mobile as PlayerMobile;

	    pm.Target = new InternalTarget();
	}
	private Mobile _parent;

	private int _fire;
	private int _water;
	private int _air;
	private int _earth;
	private int _necro;
	
	public Prots( Mobile parent ){
	    _parent = parent;
	    _air = 0;
	    _fire = 0;
	    _earth = 0;
	    _water = 0;
	    _necro = 0;
	}

	public int Fire {
	    get { return _fire; }
	    set { _fire = value; }
	}
	public int Water {
	    get { return _water; }
	    set { _water = value; }
	}
	public int Air {
	    get { return _air; }
	    set { _air = value; }
	}
	public int Earth {
	    get { return _earth; }
	    set { _earth = value; }
	}
	public int Necro {
	    get { return _necro; }
	    set { _necro = value; }
	}

       	public void UpdateProts(){
	    _air = PiecewiseScale(DamageType.Air);
	    _earth = PiecewiseScale(DamageType.Earth);
	    _fire = PiecewiseScale(DamageType.Fire);
	    _necro = PiecewiseScale(DamageType.Necro);
	    _water = PiecewiseScale(DamageType.Water);
	}

	private int PiecewiseScale( DamageType dt ){
	    double tally = 0;

	    tally += 0.25 * WristProt(dt);
	    tally += 0.25 * RingProt(dt);
	    tally += 0.25 * NeckProt(dt);
	    tally += 0.25 * HandsProt(dt);
	    tally += 0.33 * ArmsProt(dt);
	    tally += 0.33 * HeadProt(dt);
	    tally += 0.33 * LegsProt(dt);
	    tally += 0.5 * ChestProt(dt);
	    tally += 0.5 * ShieldProt(dt);

	    if( tally > 3 ){
		tally = 3; //hard cap at 75% prot
	    }
	    
	    return (int)tally;
	}

	private int Assess(Item theitem, DamageType dmgtype){
	    CraftResource cr;
	    
	    if( theitem is BaseArmor ){
		cr = ((BaseArmor)theitem).Resource;
	    }
	    else if( theitem is BaseJewel ){
		cr = ((BaseJewel)theitem).Resource;
	    }
	    else {
		return 0;
	    }

	    return GetResist( cr, dmgtype );
	}

	private int GetResist( CraftResource cr, DamageType dmgtype ){
	    switch(dmgtype){
		case DamageType.Air:
		    if( cr == CraftResource.Azurite ){
			return 2;
		    }
		    if( cr == CraftResource.Goddess ){
			return 1;
		    }
		    if( cr == CraftResource.RadiantNimbusDiamond ) {
			return 3;
		    }
		    if( cr == CraftResource.GoldenDragonLeather ){
			return 1;
		    }
		    break;
		case DamageType.Earth:
		    if( cr == CraftResource.Destruction ){
			return 1;
		    }
		    if( cr == CraftResource.Crystal ){
			return 1;
		    }
		    if( cr == CraftResource.WyrmLeather ){
			return 1;
		    }
		    if( cr == CraftResource.GoldenDragonLeather ){
			return 1;
		    }
		    if( cr == CraftResource.RadiantNimbusDiamond ){
			return 3;
		    }
		    break;
		case DamageType.Fire:
		    if( cr == CraftResource.Lavarock ){
			return 2;
		    }
		    if( cr == CraftResource.LavaLeather ){
			return 2;
		    }
		    if( cr == CraftResource.WyrmLeather ){
			return 2;
		    }
		    if( cr == CraftResource.GoldenDragonLeather ){
			return 3;
		    }
		    if( cr == CraftResource.DarkSableRuby ){
			return 3;
		    }
		    break;
		case DamageType.Necro:
		    if( cr == CraftResource.SilverRock ){
			return 1;
		    }
		    if( cr == CraftResource.LicheLeather ) {
			return 1;
		    }
		    if( cr == CraftResource.NecromancerLeather ){
			return 1;
		    }
		    if( cr == CraftResource.BalronLeather ) {
			return 1;
		    }
		    if( cr == CraftResource.Undead ){
			return 2;
		    }
		    if( cr == CraftResource.Virginity ){
			return 2;
		    }
		    if( cr == CraftResource.RadiantNimbusDiamond ){
			return 3;
		    }
		    break;
		case DamageType.Water:
		    if( cr == CraftResource.IceRock ){
			return 1;
		    }
		    if( cr == CraftResource.Dripstone ){
			return 1;
		    }
		    if( cr == CraftResource.IceCrystalLeather ){
			return 2;
		    }
		    if( cr == CraftResource.EbonTwilightSapphire ){
			return 3;
		    }
		    break;
		default:
		    return 0;
	    }
	    return 0;
	}
	
	private double NeckProt( DamageType dt ){
	    if( _parent.NeckArmor != null ){
		Item armor = _parent.NeckArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}
	private double HandsProt( DamageType dt){
	    if( _parent.HandArmor != null ){
		Item armor = _parent.HandArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}
       
	private double ArmsProt( DamageType dt){
	    if( _parent.ArmsArmor != null ){
		Item armor = _parent.ArmsArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}
	private double HeadProt( DamageType dt){
	    if( _parent.HeadArmor != null ){
		Item armor = _parent.HeadArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}
	private double LegsProt( DamageType dt){
	    if( _parent.LegsArmor != null ){
		Item armor = _parent.LegsArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}
	private double ChestProt( DamageType dt){
	    if( _parent.ChestArmor != null ){
		Item armor = _parent.ChestArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}	
	private double ShieldProt( DamageType dt){
	    if( _parent.ShieldArmor != null ){
		Item armor = _parent.ShieldArmor;
		return (double)Assess(armor, dt);
	    }
	    else {
		return 0;
	    }
	}
	private double RingProt( DamageType dt ){
	    Item ring = _parent.FindItemOnLayer( Layer.Ring ) as Item;
	    if( ring != null ){
		return (double)Assess(ring, dt);
	    }
	    else{
		return 0;
	    }
	}

	private double WristProt( DamageType dt ){
	    Item bracelet = _parent.FindItemOnLayer( Layer.Bracelet ) as Item;
	    if( bracelet != null ){
		return (double)Assess(bracelet, dt);
	    }
	    else{
		return 0;
	    }
	}
	
	private class InternalTarget : Target{
	    public InternalTarget( ) : base( 12, false, TargetFlags.None ){
	    }

	    protected override void OnTarget( Mobile from, object o ){
		if( o is Mobile){
		    Mobile m = o as Mobile;
		    from.SendMessage("Air: {0}, Earth: {1}, Fire: {2}, Necro: {3}, Water: {4}", m.Prots.Air, m.Prots.Earth, m.Prots.Fire, m.Prots.Necro, m.Prots.Water );
		}
		else {
		    from.SendMessage("Can only target Mobiles");
		}
	    }
	}
    }
}
