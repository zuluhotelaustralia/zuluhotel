using System;
using System.Collections;
using System.Collections.Generic;

using Server;
using Server.Targeting;
using Server.Engines.Gather;

namespace Server.Items
{
    public class SurveyTool : Item
    {
	private List<GatherNode> m_ReportList;

	private DateTime _nextUse;
	
	public enum SurveyType {
	    Tree,
	    Terrain
	}
	
	[Constructable]
	public SurveyTool( ) : base( 0xF39 )
	{
	    m_ReportList = new List<GatherNode>();
	    _nextUse = DateTime.Now;
	}

	public SurveyTool( Serial serial ) : base( serial )
	{
	    m_ReportList = new List<GatherNode>();
	    _nextUse = DateTime.Now;
	}

	public override void OnDoubleClick( Mobile from ){
	    if( DateTime.Compare( DateTime.Now, _nextUse ) >= 0 ){
		from.Target = new InternalTarget( this );
	    }
	}

	public void MunchMunch( Mobile from ){
	    from.PlaySound( Utility.Random( 0x3A, 3 ) );
	    from.Emote("Munch munch munch");

	    if ( from.Body.IsHuman && !from.Mounted ){
		from.Animate( 34, 5, 1, true, false, 0 );
	    }
	}

	//generate the actual report here
	public void WriteReport(){}
	    
	//actually take the sample of what's there
	public void Sample( Point3D loc, SurveyType t, Mobile from ){
	    // determine what was clciked (tree or terrain)
	    // shit out list of all nodes above 10% chance
	    m_ReportList.Clear();

	    MunchMunch( from );
	    
	    GatherSystem sys;
	    if( t == SurveyType.Tree ){
		sys = Server.Engines.Gather.Lumberjacking.System;
	    }
	    else {
		sys = Server.Engines.Gather.Mining.System;
	    }
	    	    
	    foreach( GatherNode node in sys.Nodes ){
		int dx = Math.Abs( loc.X - node.X );
		int dy = Math.Abs( loc.Y - node.Y );

		double dxsq = Math.Pow( (double)dx, 2.0 );
		double dysq = Math.Pow( (double)dy, 2.0 );
		
		double dist = Math.Sqrt( dxsq + dysq );
		double a = ( node.Abundance * node.Difficulty ) / dist;

		if( a >= 0.1 ){
		    m_ReportList.Add( node );
		}
	    }
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); // version
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	}

	public class InternalTarget : Target {
	    private SurveyTool m_Tool;
	    private Lumberjacking m_LumberjackingSystem;
	    private Mining m_MiningSystem;

	    public InternalTarget( SurveyTool tool ) : base( 4, true, TargetFlags.None ) {
		m_Tool = tool;
		m_LumberjackingSystem = Server.Engines.Gather.Lumberjacking.System;
		m_MiningSystem = Server.Engines.Gather.Mining.System;
	    }

	    protected override void OnTarget( Mobile from, object targeted ){
		int tileID = m_LumberjackingSystem.GetTileID( targeted );

		if( m_LumberjackingSystem.Validate( tileID ) ){
		    m_Tool.Sample( from.Location, SurveyType.Tree, from );
		}
		else if( m_MiningSystem.ValidateRock( tileID ) ){
		    m_Tool.Sample( from.Location, SurveyType.Terrain, from );
		}
		else {
		    from.SendMessage("You must target terrain (not sand), or trees.");
		}
	    }
	}
    }
}
