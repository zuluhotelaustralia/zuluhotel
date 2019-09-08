using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Commands;
using Server.Accounting;
using Server.Antimacro;

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

	//for mining sand
	protected List<GatherNode> m_SandNodes;
	public List<GatherNode> SandNodes { get { return m_SandNodes; } }

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
	    //CommandSystem.Register( "AutoLoop", AccessLevel.Player, new CommandEventHandler( AutoLoop_OnCommand ) );
	    //CommandSystem.Register( "GatherSystemSetup", AccessLevel.Developer, new CommandEventHandler( GatherSystemSetup_OnCommand ) );
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

		Account acct = from.Account as Account;
		//check for unattended/afk gathering here
		//from msdn:  DateTime.Compare(t1, t2) returns positive integer if t1 is later than t2
				
		if ( DateTime.Compare(acct.NextTransaction, DateTime.UtcNow ) <= 0 ) {
		    // give high-trust users a break... can tweak this later
		    // e.g. if your trust score is 98% you'll only get a challenge 2% of the time your
		    // challenge date comes up
		    if ( Utility.RandomDouble() > acct.TrustScore ) {
			new AntimacroTransaction(from);
		    }
		}		    
		
		//TODO:  can this be called if from is dead?
		from.Target = new GatherTarget( tool, this );
		return true;
	    }

	    return false; //should this function return false irrespective of checktool?
	}

	//target calls this
	public virtual void StartGathering( Mobile from, Item tool, object targeted ) {
	    StartGathering( from, tool, targeted, false );
	}

	//bool sand should always be false unless the user is actually mining sand
	public virtual void StartGathering( Mobile from, Item tool, object targeted, bool sand ) {
	    from.RevealingAction();
	    
	    object toLock = GetLock( from, tool, targeted );

	    if ( !from.BeginAction( toLock ) ){
		OnConcurrentGather( from, tool, targeted );
		return;
	    }
	    
	    Skill s = from.Skills[m_SkillName];

	    //select node
	    GatherNode n = Strike( BuildNodeList( s, from, sand ) );
	    
	    //new GatherTimer( from, tool, this, n, targeted, toLock ).Start();
	    StartGatherTimer( from, tool, this, n, targeted, toLock );
	}

	public abstract void StartGatherTimer( Mobile from, Item tool, GatherSystem system, GatherNode node, object targeted, object locked );

	public void OnConcurrentGather( Mobile from, Item tool, object targeted ) {
	    Console.WriteLine("FIXME:  OnConcurrentGather");
	    //l33t code goes here yo
	}

	// also make sure they haven't moved, aren't dead, etc.
	public virtual bool CheckWhileGathering( Mobile from, Item tool, object targeted, object locked, GatherNode n ) {
	    //if they moved, etc. return false
	    if( !CheckRange(from, tool, targeted) ){
		from.SendLocalizedMessage( 1076766 ); //that is too far away
		from.EndAction(locked);
		return false;
	    }
	    if( !from.Alive ){
		from.SendLocalizedMessage( 1060190 ); //can't do that while dead fucktard
		from.EndAction(locked);
		return false;
	    }
	    
	    from.RevealingAction();
	    return true;
	}

	public virtual void FinishGathering( Mobile from, Item tool, object targeted, object locked, GatherNode n ) {
	    FinishGathering( from, tool, targeted, locked, n, false );
	}
	public virtual void FinishGathering( Mobile from, Item tool, object targeted, object locked, GatherNode n, bool sand ) {
	    Skill s = from.Skills[m_SkillName];
	    
	    //this is our chance to succeed at harvesting, not the chance to actually hit the node
	    double chance;

	    //I know we called it "Difficulty" in the GatherNode definition but that was an older
	    //"version" if you will, and really the variable Difficulty now describes the ease
	    //of gathering a given resource, and is kinda-sorta-defined on a scale of 250 to 10,
	    // with 250 being "easiest" and 10 being "very very hard".
	    //
	    // we'll use minskill as a proxy for this since it makes the math a bit easier:
	    
	    if( n.MinSkill <= 0.0 ) {
		chance = s.Value * n.Abundance;
	    }
	    else {
		chance = s.Value * n.Abundance / n.MinSkill;
	    }
	
	    //cap harvesting success rate at 98%
	    if ( chance > 0.98 ) {
		chance = 0.98;
	    }
	    
	    if ( from.CheckSkill( s, chance ) ){
		if( sand ) {
		    SendSandSuccessMessage(from);
		}
		else {
		    SendSuccessMessage(from);
		}
		GiveResources( n, from, true );
		from.EndAction( locked );
		return;
	    }

	    if( sand ) {
		SendSandFailMessage(from);
	    }
	    else {
		SendFailMessage(from);
	    }
	    from.EndAction( locked );
	}

	public abstract void SendFailMessage( Mobile m );
	public abstract void SendNoResourcesMessage( Mobile m );
	public abstract void SendSuccessMessage( Mobile m );

	// there's gotta be a more elegant way to do this, but fuck it, I want to get Beta out the door --sith
	public virtual void SendSandFailMessage( Mobile m ) {}
	public virtual void SendSandNoResourcesMessage( Mobile m ) {}
	public virtual void SendSandSuccessMessage( Mobile m ) {}
	
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
	    object o = Activator.CreateInstance( type );
	    return (Item)o;
	}


	//attenuate abundance by distance from node
	public bool IncludeByDistance( GatherNode n, Mobile m ){
	    int deltaX = Math.Abs( m.X - n.X );
	    int deltaY = Math.Abs( m.Y - n.Y );

	    double dxsquared = Math.Pow( (double)deltaX, 2.0);
	    double dysquared = Math.Pow( (double)deltaY, 2.0);
	    
	    double dist = Math.Sqrt( dxsquared + dysquared );

	    double a = ( n.Abundance * n.Difficulty ) / dist; // kinda exponential decay

	    if ( a > 1.0 ) {
		return true;
	    }
	    else if ( a < 0.01 ) {
		return false;
	    }

	    return ( a >= Utility.RandomDouble() );
	}

	// build a list of which nodes are available to the player, skillwise
	public List<GatherNode> BuildNodeList( Skill s, Mobile m, bool sand ){
	    List<GatherNode> nodes = new List<GatherNode>();

	    //if they're mining on sand we don't want to spawn them icerock or something
	    // although perhaps we might want to consider specifically lavarock?
	    if( sand ){
		nodes.Add(m_SandNodes[0]);
	    }
	    else {
		foreach (GatherNode n in m_Nodes) {
		    if ( n.MinSkill <= s.Value &&
			 n.MaxSkill >= s.Value) {

			if ( IncludeByDistance(n, m) ){
			    //add the node from m_Nodes to the ephemeral list we're building
			    nodes.Add(n);
			}
		    }
		}

		if ( nodes.Count <= 0 ) {
		    //it's unlikely but entirely possible to be in a dead spot where you're too far away from every node
		    // and can't hit anything.  this will cause a server crash in Strike(), and more importantly
		    // is shitty game design:  there's a tree sprite right there, why can't the player get wood off it?  etc.
		    // --sith

		    nodes.Add( m_Nodes[0] ); //force-add the first node which should be Iron, normal wood, etc.
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
