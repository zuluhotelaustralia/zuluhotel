using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;

namespace Server.Engines.Harvest {
    public static class GatherSystem {
	
	private static List<GatherNode> m_Nodes;
	public static List<GatherNode> Nodes { get { return m_Nodes; } }

	public static void Setup() {
	    //hopefully this solves the race condition??
	    m_Nodes = new List<GatherNode>();
	}
	
	//danger, will robinson
	public static void ClearNodes() {
	    m_Nodes.Clear();
	}
	
	public static void AddNode( GatherNode n ){
	    m_Nodes.Add(n);
	}

	//attenuate abundance by distance from node
	public static double ScaleByDistance( GatherNode n, Mobile m ){
	    int deltaX = Math.Abs( m.X - n.X );
	    int deltaY = Math.Abs( m.Y - n.Y );

	    double a = ( (double)deltaX + (double)deltaY ) / 2.0;

	    return a;
	}
	
	// determine which ores are available skillwise
	// that's your new list, normalize the abundances to 100%, order by maxskill
	// diceroll -> get ore
    }
}
