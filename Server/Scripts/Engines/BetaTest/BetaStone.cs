using System;
using Server;

namespace Server.Items {
    public class InviteStone : Item {

	[Constructable]
	public InviteStone() : base( 0xF8B ){
	    this.Name = "A fiery moonstone";
	    this.Hue = 2747; //lavarock 
	}

	public InviteStone( Serial serial ) : base( serial ){
	    this.Name = "A fiery moonstone";
	    this.Hue = 2747;
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize( writer );
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );
	}

	public override void OnDoubleClick( Mobile from ){
	    if( Core.BetaTest ){
		if( !IsChildOf( from.Backpack ) ){
		    from.SendLocalizedMessage( 1042001 ); //that must be in your pack
		}
		else {
		    String s = BetaStone.Stone.RequestNewKey( from );
		    from.SendMessage("You hear spirits whisper words that will grant a friend access to the Beta Realm: " + s );
		    BetaStone.Stone.ConsumeKey( s );
		}
	    }
	}
    }
}
