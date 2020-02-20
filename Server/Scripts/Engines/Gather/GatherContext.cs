using System;
using Server;
using Server.Mobiles;

//honestly I hate how convoluted runuo is with all the fucking meta-objects everywhere
// but I need an object to hold the current vein they're on so I can build upon that functionality
// --sith

namespace Server.Engines.Gather{
    public class GatherContext {
	private int _x;
	private int _y;
	private Mobile _owner;
	private GatherNode _node;
	private bool _valid;
	
	public GatherNode Node{
	    get { return _node; }
	}

	public bool Validity{
	    get { return _valid; }
	    set { _valid = value; }
	}

	public GatherContext( int x, int y, Mobile from, GatherNode node ){
	    _x = x;
	    _y = y;
	    _owner = from;
	    _node = node;
	}

	public bool Validate( ){
	    if( _owner.X == _x && _owner.Y == _y ){
		_valid = true;
		return true;
	    }

	    _valid = false;
	    return false;
	}
    }
}
