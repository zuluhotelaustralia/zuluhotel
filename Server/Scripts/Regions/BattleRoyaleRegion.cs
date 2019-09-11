using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using Server;

namespace Server.Regions {
    public class BattleRoyaleRegion : TownRegion {
	public BattleRoyaleRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent ) {}

	public override TimeSpan GetLogoutDelay( Mobile m ) {
	    List<Mobile> players = Server.BattleRoyale.GameController.PlayerList;

	    if( players.Contains( m ) ){
		return TimeSpan.FromMinutes( 30 );
	    }

	    return base.GetLogoutDelay( m );
	}
    }
}
