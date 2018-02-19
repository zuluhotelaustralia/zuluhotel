using Server;

namespace Server {
    public enum DamageType {
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
    }

    public class DamageScalar {

	public DamageScalar(){
	}
	
	public virtual int ScaleDamage( int amount, Mobile from, Mobile m, DamageType type ) {
	    return amount;
	}
    }
}
