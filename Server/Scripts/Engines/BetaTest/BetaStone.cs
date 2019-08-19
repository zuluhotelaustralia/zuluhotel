using System;
using Server;

namespace Server.Items {
    public class BetaStone : Item {

	[Constructable]
	public BetaStone() : base( 0xF8B ){
	    this.Name = "A fiery moonstone";
	    this.Hue = 2747; //lavarock 
	}

	public BetaStone( Serial serial ) : base( serial ){
	    this.Name = "A fiery moonstone";
	    this.Hue = 2747;
	}

	public override void OnDoubleClick( Mobile from ){
	    if( Core.BetaTest ){
		if( !IsChildOf( from.Backpack ) ){
		    from.SendLocalizedMessage( 1042001 ); //that must be in your pack
		}
		else {
		    String s = Server.Beta.BetaStone.Stone.RequestNewKey( from );
		    from.SendMessage("You hear spirits whisper words that will grant a friend access to the Beta Realm: " + s );
		}
	    }
	}
    }
}
