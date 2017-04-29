using System;
using System.Collections;
using System.Collections.Generic;
using Server;
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
    }
}
