using System;
using Server;
using Server.Mobiles;

/*
  Raw,
  Physical,
  Ranged,
  Magical,
  Air,
  Earth,
  Fire,
  Water,
  Necro,
  Holy,
  Poison
*/

namespace Server {
  
    public class ActualDamageScalar : DamageScalar {
	
	public ActualDamageScalar() : base() {
	}

	public static void Initialize() {
	    System.Console.Write("Initializing new Damage Scalar object...");
	    Mobile.DamageScalar = new ActualDamageScalar();
	    System.Console.WriteLine("Done!");
	}

	private double ApplyElementalScaling(double amount, Mobile m, DamageType type){
	    //todo:  attenuate or amplify damage based on target's elemental protection(s).
	    // e.g. if you're wearing Lavarock platemail, fire doesn't hurt you as much but
	    // if you're wearing icerock, fire rips you a new asshole.

	    int protamount = 0;

	    switch( type ){
		case DamageType.Air:
		    protamount = m.Prots.Air;
		    break;
		case DamageType.Earth:
		    protamount = m.Prots.Earth;
		    break;
		case DamageType.Fire:
		    protamount = m.Prots.Fire;
		    break;
		case DamageType.Necro:
		    protamount = m.Prots.Necro;
		    break;
		case DamageType.Water:
		    protamount = m.Prots.Water;
		    break;
		default:
		    break;
	    }

	    if( protamount == 0 ){
		return amount;
	    }

	    if ( protamount > 4 ){
		protamount = 4;
	    }
	    	    
	    double scale = (double)protamount * 0.25;
	    double adjusted = (double)amount * scale;
	    double ret = (double)amount - adjusted;
	    
	    if( ret < 0.0 ){
		ret = 0.0;
	    }
	    
	    return ret;
	}

	//this function gets called in Mobile.Damage() and is intended to be the single point of all
	// damage scaling caused by zulu-related systems.  Every time a mobile gets damaged, Mobile.Damage needs
	// to be the instrument by which this is done.

	//public int ScaleDamage( int amount, Mobile from, Mobile m ){
	//    return ScaleDamage(amount, from, m, DamageType.Physical);
	//}

	public int ScaleDamage( int amount, Mobile from, Mobile m, DamageType type ) {
	    return ScaleDamage( amount, from, m, type, AttackType.Physical );
	}
	
	public override int ScaleDamage( double amount, Mobile from, Mobile m, DamageType dmgtype, AttackType atktype ) {
	    double result = amount;
	    double tgtbonus = 1.0;
	    double frombonus = 1.0;
	    SpecName tgtSpec = SpecName.None;
	    SpecName fromSpec = SpecName.None;

	    if( Core.Debug ){
		Console.WriteLine( "WE SCALEING NAO");
	    }

	    //if they are spec, scale by their class bonus
	    if( from is PlayerMobile &&
		((PlayerMobile)from).Spec.SpecName != SpecName.None &&
		((PlayerMobile)from).Spec.SpecName != SpecName.Powerplayer ){
		frombonus = ((PlayerMobile)from).Spec.Bonus;
		fromSpec = ((PlayerMobile)from).Spec.SpecName;
	    }

	    if( m is PlayerMobile &&
		((PlayerMobile)m).Spec.SpecName != SpecName.None &&
		((PlayerMobile)m).Spec.SpecName != SpecName.Powerplayer ){
		tgtbonus = ((PlayerMobile)m).Spec.Bonus;
		tgtSpec = ((PlayerMobile)m).Spec.SpecName;
	    }
	    
	    if( frombonus == 1.0 &&
		tgtbonus == 1.0){

		//apply the elemental scaling here if we're just going to return right away.
		result = ApplyElementalScaling(amount, m, dmgtype);
		
		return (int)result;
		//might as well save the cycles if there's no reason to go through this
	    }

	    //note that this is not being double-applied.  First one only gets called if we return early.
	    result = ApplyElementalScaling(amount, m, dmgtype);
	    
	    //take a deep breath, this gets ugly.
	    switch( atktype ){
		case AttackType.Magical:
		    //outgoing
		    if (fromSpec == SpecName.Mage)
		    {
			//mages deal more magic damage
			result *= frombonus;
		    }
		    if (fromSpec == SpecName.Warrior)
		    {
			//warriors deal less
			result /= frombonus;
		    }

		    //incoming
		    if (tgtSpec == SpecName.Mage){
			//mages take less
			result /= tgtbonus;
		    }
		    if (tgtSpec == SpecName.Warrior){
			result *= tgtbonus;
		    }
		    break;
		    
		case AttackType.Physical:
		    
		    //outgoing
		    if(fromSpec == SpecName.Warrior){
			result *= frombonus;
		    }
		    if(fromSpec == SpecName.Mage){
			result /= frombonus;
		    }

		    //incoming
		    if ( tgtSpec == SpecName.Warrior )
		    {
			//warriors get melee reduction
			result /= tgtbonus;
		    }
		    if (tgtSpec == SpecName.Mage)
		    {
			//mages take more
			result *= tgtbonus;
		    }
		    break;

		case AttackType.Ranged:
		    //outgoing
		    if(fromSpec == SpecName.Ranger){
			result *= frombonus;
		    }
		    
		    if(tgtSpec == SpecName.Ranger)
		    {
			//rangers get ranged protection
			result /= tgtbonus;
		    }
		    break;
		default:
		    break;
	    }

	    return (int)result;
	}
    }
}
