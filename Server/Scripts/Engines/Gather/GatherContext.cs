using System;
using Server;
using Server.Mobiles;

namespace Server.Engines.Gather{
    public class GatherContext {
	private int _x;
	private int _y;
	private Mobile _owner;
	private GatherNode _node;

	public GatherContext( int x, int y, Mobile from, GatherNode node ){
	    _x = x;
	    _y = y;
	    _owner = from;
	    _node = node;
	}

	public bool Validate( int x, int y ){
	    if( x == _x && y == _y ){
		return true;
	    }

	    return false;
	}
    }
}
