using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;

namespace Server.Engines.Harvest {
    public abstract class GatherSystem {
	
	private List<GatherNode> m_Nodes;
	public List<GatherNode> Nodes { get { return m_Nodes; } }

	public void Setup() {
	    //hopefully this solves the race condition??
	    m_Nodes = new List<GatherNode>();
	}
	
	//danger, will robinson
	public void ClearNodes() {
	    m_Nodes.Clear();
	}
	
	public void AddNode( GatherNode n ){
	    m_Nodes.Add(n);
	}

	//entry point
	public bool BeginGathering( Mobile from, Item tool ){
	    //check if valid gathering location/tool uses remaining/tool broken/etc.
	    
	    from.Target = new GatherTarget( tool, this );
	    return true;
	}

	public void StartGathering( Mobile from, Item tool, object targeted ) {
	    // wtf goes here?
	}
	
	//attenuate abundance by distance from node
	public double ScaleByDistance( GatherNode n, Mobile m ){
	    int deltaX = Math.Abs( m.X - n.X );
	    int deltaY = Math.Abs( m.Y - n.Y );

	    double a = ( (double)deltaX + (double)deltaY ) / 2.0;

	    return a;
	}
	
	// build a list of which nodes are available to the player, skillwise
	public List<GatherNode> BuildNodeList( Skill s ){
	    List<GatherNode> nodes = new List<GatherNode>();

	    foreach (GatherNode n in m_Nodes) {
		if ( n.Resource.ReqSkill <= s.Value ) {
		    nodes.Add(n);  //add the node from m_Nodes to the ephemeral list we're building
		}
	    }

	    return nodes;
	}

	//roll a random number against the list from BuildNodeList to determine which node we try to strike
	public GatherNode Strike( List<GatherNode> nodes){
	    int numNodes = nodes.Count();
	    int nodeStruck = Utility.Dice( 1, numNodes, 0 );

	    // list indices are zero-based
	    return nodes.Item( nodeStruck - 1 );
	}

	//attempt to harvest from selected node
	public bool TryGather( PlayerMobile m, Skill s ){
	    GatherNode n = Strike( BuildNodeList( s ) );

	    //this is our chance to succeed at harvesting, not the chance to actually hit the node
	    double chance;

	    if ( s.Value - n.Resource.ReqSkill < 0.0 ) {
		chance = 0.0;
	    }
	    else {
		chance = s.Value * n.Abundance / n.Resource.ReqSkill;
		
		// e.g. for a rare ore (executor, let's say) with a=0.1,
		// with mining skill of 90.0 against a reqskill of 80.0
		// chance = 11.1%
		// whereas, for a common ore (e.g. iron) with a=0.9
		// with mining skill of 90.0 against a reqskill of 30.0
		// chance = 270% i.e. you'll hit it every time you try
	    }

	    //cap harvesting success rate at 98%
	    if ( chance > 1.0 ) {
		chance = 0.98;
	    }

	    m_System.GiveResources();
	    
	    return m.CheckSkill( s, chance );
	}
    }
}
