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
	
	public ActualDamageScalar() {
	}

	public static void Initialize() {
	    Mobile.DamageScalar = new ActualDamageScalar();
	}
	
	// we are scaling damage _received_ here, not dealt.
	// do we need "from"?  not really.
	public override int ScaleDamage( int amount, Mobile m, DamageType type ) {

	    double result = (double)amount;
	    double bonus = 1.0;
	    SpecName s = SpecName.None;

	    //if they are spec, scale by their class bonus
	    if( m is PlayerMobile && ((PlayerMobile)m).Spec.SpecName != SpecName.None ){
		bonus *= ((PlayerMobile)m).Spec.Bonus;
		s = ((PlayerMobile)m).Spec.SpecName;
	    }
	    else {
		return amount; //might as well save the cycles if there's no reason to go through this
	    }

	    //take a deep breath
	    switch( type ){
		case DamageType.Magical:
		    if (s == SpecName.Mage)
		    {
			//mages take less magic dmg
			result /= bonus;
		    }
		    if (s == SpecName.Warrior)
		    {
			//warriors take more
			result *= bonus;
		    }
		    break;
		    
		case DamageType.Physical:
		    if ( s == SpecName.Warrior )
		    {
			//warriors get melee reduction
			result /= bonus;
		    }
		    if (s == SpecName.Mage)
		    {
			//mages take more
			result *= bonus;
		    }
		    break;

		case DamageType.Ranged:
		    if(s == SpecName.Ranger)
		    {
			//rangers get ranged protection
			result /= bonus;
		    }
		    break;
		default:
		    break;
	    }

	    return (int)result;
	}

    }
}
