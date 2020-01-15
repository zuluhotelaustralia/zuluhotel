using Server;

namespace Server {

//DAMAGETYPE is the elemental damage type (we're bypassing AOS's bullshit)
    public enum DamageType {
	None,
	Air,
	Earth,
	Fire,
	Water,
	Necro,
	Poison
    }

    //ATTACKTYPE is the actual type of attack e.g. shooting a guy with a bow vs casting a spell
    //Differentiating these things in this manner allows for e.g. elemental fire bows doing
    // ranged fire damage, or magical attacks (spells) that do different elemental damages
    // but still lets us handle spec-based bonuses in an elegant way
    
    public enum AttackType {
	Raw,
	Physical,
	Ranged,
	Magical
    }
	
    public class DamageScalar {

	public DamageScalar(){
	}
	
	public virtual int ScaleDamage( double amount, Mobile from, Mobile m, DamageType dmgtype, AttackType atktype) {
	    return (int)amount;
	}
    }
}
