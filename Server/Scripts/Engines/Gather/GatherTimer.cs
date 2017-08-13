using System;

namespace Server.Engines.Gather
{
    public class GatherTimer : Timer
    {
	private Mobile m_From;
	private Item m_Tool;
	private GatherSystem m_System;
	private object m_Targeted, m_Locked;
	private int m_Count;
	private GatherNode m_Node;

	public GatherTimer( Mobile from, Item tool, GatherSystem system, GatherNode node, object targeted, object locked ) : base( TimeSpan.Zero )
	{
	    m_From = from;
	    m_Tool = tool;
	    m_System = system;
	    m_Targeted = targeted;
	    m_Locked = locked;
	    m_Count = from.AutoLoop;
	    m_Node = node;
	}

	protected override void OnTick()
	{
	    m_Count--;
	    
	    if ( !m_System.CheckWhileGathering( m_From, m_Tool, m_ToHarvest, m_Locked, m_Node ) ){
		Stop();
	    }
	    if ( m_Count <= 0 ) {
		Stop();
	    }
	}
    }
}
