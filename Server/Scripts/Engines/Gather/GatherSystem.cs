using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Commands;

/* 
   Zulu Hotel Gathering System
   System logic:
   1. Player uses their tool and arrives at BeginGather(), the entry point
   1a.Do checks and stuff for double gather, tool, movement, antimacro, etc.
   2. Assemble a list of which nodes are available to the player based on 
   lower bound of difficulty curve (aka max gatherable radius)
   3. Trim any nodes necessary based on min/max skill
   4. Roll a die to select from that list which ore attempt to hit
   5. Check if gather attempt actually succeeds, weighted by abundance
   6. Award resources

*/

namespace Server.Engines.Gather {
    public abstract class GatherSystem {

	protected List<GatherNode> m_Nodes;
        public List<GatherNode> Nodes { get { return m_Nodes; } }

	protected SkillName m_SkillName;
	public SkillName SkillName { get { return m_SkillName; } set { m_SkillName = value; } }
	
	//danger
	public void ClearNodes() {
	    m_Nodes.Clear();
	}

	public void AddNode( GatherNode n ){
	    m_Nodes.Add(n);
	}

	public static void Initialize() {
	    CommandSystem.Register( "AutoLoop", AccessLevel.Player, new CommandEventHandler( AutoLoop_OnCommand ) );
	    CommandSystem.Register( "GatherSystemSetup", AccessLevel.Developer, new CommandEventHandler( GatherSystemSetup_OnCommand ) );
	    CommandSystem.Register( "GetNodeInfo", AccessLevel.Developer, new CommandEventHandler( GetNodeInfo_OnCommand ) );
	}

	[Usage( "GetNodeInfo <integer>" )]
	[Description( "Returns info about GatherNode <integer> on the targeted control stone" )]
	public static void GetNodeInfo_OnCommand( CommandEventArgs e ){
	    if( e.Length !=1 ){
		e.Mobile.SendMessage("Usage: {0}GetNodeInfo <integer>");
	    }
	    else {
		e.Mobile.Target = new NodeDebugTarget(e.GetInt32(0));
	    }
	}
	
	[Usage( "GatherSystemSetup" )]
	[Description( "Performs initial setup of one type of GatherNode")]
	public static void GatherSystemSetup_OnCommand( CommandEventArgs e ){
	    e.Mobile.SendMessage("Select a control stone.");
	    e.Mobile.Target = new SetupGatherTarget();
	}

	[Usage( "AutoLoop <number from 1 to 1000>" )]
	[Description( "Sets the number of consecutive times you would like to attempt to gather resources." )]
	public static void AutoLoop_OnCommand( CommandEventArgs e ) {
	    if ( e.Length != 1 ){
		e.Mobile.SendMessage("Usage: {0}AutoLoop <1-1000>");
	    }
	    else {
		int loops = e.GetInt32(0);
		if( loops > 1000 || loops < 1 ){
		    e.Mobile.SendMessage("Usage: {0}AutoLoop <1-1000>");
		}
		else{
		    e.Mobile.AutoLoop = loops;
		    e.Mobile.SendMessage("AutoLoop set to {0}", loops);
		}
	    }
	}

	public virtual object GetLock( Mobile from, Item tool, object targeted ){
	    // return tool; to allow gathering from multiple tools
	    // return GetType(); to allow gathering from multiple skills i.e. lumberjacking and mining
	    // return typeof( GatherSystem ); IOT completely disable concurrent gathering

	    return GetType();
	}

	public virtual bool CheckTool( Mobile from, Item tool )
	{
	    bool wornOut = ( tool == null || tool.Deleted || (tool is IUsesRemaining && ((IUsesRemaining)tool).UsesRemaining <= 0) );
	    
	    if ( wornOut )
		from.SendLocalizedMessage( 1044038 ); // You have worn out your tool!
	    
	    return !wornOut;
	}

	//returns true if they're in acceptable range
	public virtual bool CheckRange( Mobile from, Item tool, object targeted )
	{
	    Map map;
	    Point3D loc;
	    
	    //check targeted type first
	    if ( targeted is Static && !((Static)targeted).Movable )
	    {
		Static obj = (Static)targeted;

		map = obj.Map;
		loc = obj.GetWorldLocation();
	    }
	    else if ( targeted is StaticTarget )
	    {
		StaticTarget obj = (StaticTarget)targeted;

		map = from.Map;
		loc = obj.Location;
	    }
	    else if ( targeted is LandTarget )
	    {
		LandTarget obj = (LandTarget)targeted;

		map = from.Map;
		loc = obj.Location;
	    }
	    else
	    {
		map = null;
		loc = Point3D.Zero;
	    }
	    
	    bool inRange = ( from.Map == map && from.InRange( loc, 3 ) ); // is 3 ok?

	    if ( !inRange )
		from.SendLocalizedMessage( 500446 ); //that is too far away.

	    return inRange;
	}

	//entry point
	public virtual bool BeginGathering( Mobile from, Item tool ){
	    //check if valid gathering location/tool uses remaining/tool broken/etc.
	    if( CheckTool(from, tool) ){

		//check for unattended macroing here
		
		//TODO:  can this be called if from is dead?
		from.Target = new GatherTarget( tool, this );
		return true;
	    }
	    return false; //should this function return false irrespective of checktool?
	}

	//target calls this
	public virtual void StartGathering( Mobile from, Item tool, object targeted ) {
	    
	    object toLock = GetLock( from, tool, targeted );

	    if ( !from.BeginAction( toLock ) ){
		OnConcurrentGather( from, tool, targeted );
		return;
	    }
	    
	    //select node
	    Skill s = from.Skills[m_SkillName];
	    GatherNode n = Strike( BuildNodeList( s, from ) );

	    new GatherTimer( from, tool, this, n, targeted, toLock ).Start();
	    CheckWhileGathering( from, tool, targeted, toLock, n );
	}

	public void OnConcurrentGather( Mobile from, Item tool, object targeted ) {
	    // l33t code here
	}

	//play the animations/sfx, do some checks
	// also make sure they haven't moved, aren't dead, etc.
	public virtual bool CheckWhileGathering( Mobile from, Item tool, object targeted, object locked, GatherNode n ) {
	    //if they moved, etc. return false
	    if( !CheckRange(from, tool, targeted) ){
		return false;
	    }
	    if( !from.Alive ){
		return false;
	    }
	    
	    from.RevealingAction();

	    Skill s = from.Skills[m_SkillName];
	    
	    //this is our chance to succeed at harvesting, not the chance to actually hit the node
	    double chance;
	    
	    if ( s.Value < n.MinSkill ) {
		chance = 0.0;
	    }
	    else {
		chance = s.Value * n.Abundance / n.MinSkill;
		
		// e.g. for a rare ore (executor, let's say) with a=0.1,
		// with mining skill of 90.0 against a reqskill of 80.0
		// chance = 11.1%
		// whereas, for a common ore (e.g. iron) with a=0.9
		// with mining skill of 90.0 against a reqskill of 30.0
		// chance = 270% i.e. you'll hit it every time you try
	    }

	    //cap harvesting success rate at 98%
	    if ( chance > 0.98 ) {
		chance = 0.98;
	    }

	    if ( from.CheckSkill( s, chance ) ){
		GiveResources( n, from, true );
		return true;
	    }

	    return false;
	}

	public virtual void GiveResources( GatherNode n, Mobile m, bool placeAtFeet ){
	    //public virtual bool Give( Mobile m, Item item, bool placeAtFeet )
	    Item item = Construct ( n.Resource );

	    if( item.Stackable ){
		int amount = Utility.Dice(1, 5, 2); //1d5+2, TODO change this to be skill based
		if( m is PlayerMobile ){
		    if( ((PlayerMobile)m).Spec.SpecName == SpecName.Crafter ) {
			amount *= 2;
		    }
		}
		item.Amount = amount;
	    }
	    
	    if ( m.PlaceInBackpack( item ) )
		return;

	    if ( !placeAtFeet )
		return;

	    Map map = m.Map;

	    if ( map == null )
		return;

	    List<Item> atFeet = new List<Item>();

	    foreach ( Item obj in m.GetItemsInRange( 0 ) )
		atFeet.Add( obj );

	    for ( int i = 0; i < atFeet.Count; ++i )
	    {
		Item check = atFeet[i];

		if ( check.StackWith( m, item, false ) )
		    return;
	    }

	    item.MoveToWorld( m.Location, map );
	    return;
	}

	public virtual Item Construct( Type type )
	{
	    try{ return Activator.CreateInstance( type ) as Item; }
	    catch{ return null; }
	}


	//attenuate abundance by distance from node
	public bool IncludeByDistance( GatherNode n, Mobile m ){
	    int deltaX = Math.Abs( m.X - n.X );
	    int deltaY = Math.Abs( m.Y - n.Y );

	    double dist = Math.Sqrt( (double)(deltaX^2) * (double)(deltaY^2) );

	    double a = Math.Exp( -1 * dist / n.Difficulty ); // exponential decay

	    if ( a > 1.0 ) {
		return true;
	    }
	    else if ( a < 0.01 ) {
		return false;
	    }
	    
	    return ( a >= Utility.RandomDouble() );
	}

	// build a list of which nodes are available to the player, skillwise
	public List<GatherNode> BuildNodeList( Skill s, Mobile m ){
	    List<GatherNode> nodes = new List<GatherNode>();

	    foreach (GatherNode n in m_Nodes) {
		if ( n.MinSkill <= s.Value &&
		     n.MaxSkill >= s.Value) {

		    if ( IncludeByDistance(n, m) ){
			//add the node from m_Nodes to the ephemeral list we're building
			nodes.Add(n);
		    }
		}
	    }

	    return nodes;
	}

	//roll a random number against the list from BuildNodeList to determine which node we try to strike
	public GatherNode Strike( List<GatherNode> nodes ){
	    int numNodes = nodes.Count;
	    int nodeStruck = Utility.Dice( 1, numNodes, 0 );

	    // list indices are zero-based
	    return nodes[ nodeStruck - 1 ];
	}

    }
}
