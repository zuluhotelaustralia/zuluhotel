using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Multis;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;

namespace Server.Engines.Gather
{
    public class NodeDebugTarget : Target
    {
	private int m_Index;

	public NodeDebugTarget( int index ) : base( -1, true, TargetFlags.None )
	{
	    m_Index = index;
	}


	protected override void OnTarget( Mobile from, object targeted )
	{
	    if( targeted is GatherSystemController ){
		GatherSystemController targ = (GatherSystemController)targeted;
		GatherNode n = targ.System.Nodes[m_Index];

		from.SendMessage("GatherNode: {0} {1} {2}", n.Resource, n.X, n.Y);
	    }
	    else{
		from.SendMessage("Bad target type");
	    }
	}
    }
}
