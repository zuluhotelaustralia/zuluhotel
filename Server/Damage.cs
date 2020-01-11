using Server;

namespace Server {
    public enum DamageType {
	None,
	Air,
	Earth,
	Fire,
	Water,
	Necro,
	Poison
    }

    public enum AttackType {
	Raw,
	Physical,
	Ranged,
	Magical
    }
	
    public class DamageScalar {

	public DamageScalar(){
	}
	
	public virtual int ScaleDamage( int amount, Mobile from, Mobile m, DamageType dmgtype, AttackType atktype) {
	    return amount;
	}
    }
}
