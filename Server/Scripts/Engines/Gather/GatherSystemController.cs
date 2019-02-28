using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Harvest;
using Server.Engines.Gather;
using Server.Commands;
using Server.Targeting;

namespace Server.Items {

    public enum ControlledSystem{
	None,
	Mining,
	Lumberjacking,
	Fishing
    }
    
    public class GatherSystemController : Item {

	private ControlledSystem m_SystemType = ControlledSystem.None;
	public ControlledSystem SystemType {
	    get{
		return m_SystemType;
	    }
	}
	
	private GatherSystem m_System;
	public GatherSystem System {
	    get {
		return m_System;
	    }
	    set {
		m_System = value;
	    }
	}

	[Constructable]
	public GatherSystemController() : base ( 0xED4 ) {
	    this.Name = "Gather System Controller";
	}

	public GatherSystemController( Serial serial ) : base( serial ){
	    this.Name = "Gather System Controller";
	}
	
	public static void Initialize() {
	    CommandSystem.Register( "SetGatherSystem", AccessLevel.Developer, new CommandEventHandler( SetGatherSystem_OnCommand ) );
	    CommandSystem.Register( "GetNodeInfo", AccessLevel.Developer, new CommandEventHandler( GetNodeInfo_OnCommand ) );
	    CommandSystem.Register( "NumNodes", AccessLevel.Developer, new CommandEventHandler( NumNodes_OnCommand ));
	    
	    //specified with namespaces because of conflict with harvesting
	    if( Server.Engines.Gather.Lumberjacking.Controller == null ) {
		Console.WriteLine( "Warning: Lumberjacking Controller is not properly set!" );
	    }
	    if( Server.Engines.Gather.Mining.Controller == null ) {
		Console.WriteLine( "Warning: Mining Controller is not properly set!" );
	    }
	    if( Server.Engines.Gather.Fishing.Controller == null ) {
		Console.WriteLine( "Warning: Fishing Controller is not properly set!" );
	    }
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

	[Usage( "NumNodes" )]
	[Description( "Returns length of node list for a given control stone.")]
	public static void NumNodes_OnCommand( CommandEventArgs e ) {
	    e.Mobile.Target = new NumNodesTarget();
	}

	[Usage( "SetGatherSystem <system>" )]
	[Description( "Used to bind a GatherSystemController stone to a particular Gather System." )]
	public static void SetGatherSystem_OnCommand( CommandEventArgs e ) {
	    if ( e.Length != 1 ){
		e.Mobile.SendMessage("Format: {0}SetGatherSystem <0-3>");
		e.Mobile.SendMessage("   0 - None");
		e.Mobile.SendMessage("   1 - Mining");
		e.Mobile.SendMessage("   2 - Lumberjacking");
		e.Mobile.SendMessage("   3 - Fishing");
	    }
	    else {
		e.Mobile.Target = new SetGatherSystemTarget( e.GetInt32(0) );
	    }
	}

	public void SetSystemReference( int cs ) {
	    // the engine has to know which stone it takes orders from
	    m_SystemType = (ControlledSystem)cs;
	    
	    switch( m_SystemType )
	    {
		case ControlledSystem.None:
		    {
			break;
		    }
		case ControlledSystem.Mining:
		    {
			Server.Engines.Gather.Mining.Setup(this);
			break;
		    }
		case ControlledSystem.Lumberjacking:
		    {
			Server.Engines.Gather.Lumberjacking.Setup(this);
			break;
		    }
		case ControlledSystem.Fishing:
		    {
			Server.Engines.Gather.Fishing.Setup(this);
			break;
		    }
	    }
	}
	
	public override void Serialize( GenericWriter writer ){
	    base.Serialize(writer);

	    writer.Write( (int) 0 ); //version

	    writer.Write( (int)m_SystemType );

	    if( m_SystemType != ControlledSystem.None ){
		writer.Write( m_System.Nodes.Count );
		
		foreach( GatherNode n in m_System.Nodes ) {
		    n.Drift();
		    
		    writer.Write( n.X );
		    writer.Write( n.Y );
		    writer.Write( n.vX );
		    writer.Write( n.vY );
		    writer.Write( n.Abundance );
		    writer.Write( n.Difficulty );
		    writer.Write( n.Resource.ToString() );
		    writer.Write( n.MinSkill );
		    writer.Write( n.MaxSkill );
		}
	    }
	}

	public override void Deserialize( GenericReader reader ) {
	    base.Deserialize( reader );
	    
	    int version = reader.ReadInt();

	    //do this first

	    int cs = reader.ReadInt(); //ControlledSystem
	    SetSystemReference( cs );
	    int x, y, vx, vy;
	    double a, d, min, max;
	    string res;

	    int length;

	    m_System.ClearNodes();
	    switch( version )
	    {
		case 0:
		    {
			switch( m_SystemType )
			{
			    case ControlledSystem.None:
				{
				    break;
				}
			    case ControlledSystem.Mining:
				{
				    //we foreach it so that there are the correct number of read calls
				    length = reader.ReadInt();
				    
				    for( int i=0; i<length; i++ ) {
					//make this more elegant, because yuck
					x = reader.ReadInt();
					y = reader.ReadInt();
					vx = reader.ReadInt();
					vy = reader.ReadInt();
					a = reader.ReadDouble();
					d = reader.ReadDouble();
					res = reader.ReadString();
					min = reader.ReadDouble();
					max = reader.ReadDouble();
					
					//	public GatherNode( int initialX, int initialY, int dirX, int dirY, double a, double d, double minskill, double maxskill, Type res )
					m_System.Nodes.Add( new GatherNode( x, y, vx, vy, a, d, min, max, Type.GetType(res) ) );
				    }
				    
				    break;
				}
			    case ControlledSystem.Lumberjacking:
				{
				    length = reader.ReadInt();
				    
				    for( int i=0; i<length; i++ ) {
					//make this more elegant, because yuck
					x = reader.ReadInt();
					y = reader.ReadInt();
					vx = reader.ReadInt();
					vy = reader.ReadInt();
					a = reader.ReadDouble();
					d = reader.ReadDouble();
					res = reader.ReadString();
					min = reader.ReadDouble();
					max = reader.ReadDouble();
					
					//	public GatherNode( int initialX, int initialY, int dirX, int dirY, double a, double d, double minskill, double maxskill, Type res )
					m_System.Nodes.Add( new GatherNode( x, y, vx, vy, a, d, min, max, Type.GetType(res) ) );
				    }

				    break;
				}
			    case ControlledSystem.Fishing:
				{
				    length = reader.ReadInt();
				    
				    for( int i=0; i<length; i++ ) {
					//make this more elegant, because yuck
					x = reader.ReadInt();
					y = reader.ReadInt();
					vx = reader.ReadInt();
					vy = reader.ReadInt();
					a = reader.ReadDouble();
					d = reader.ReadDouble();
					res = reader.ReadString();
					min = reader.ReadDouble();
					max = reader.ReadDouble();
					
					//	public GatherNode( int initialX, int initialY, int dirX, int dirY, double a, double d, double minskill, double maxskill, Type res )
					m_System.Nodes.Add( new GatherNode( x, y, vx, vy, a, d, min, max, Type.GetType(res) ) );
				    }

				    break;
				}
			}
			
			
			break; //break case0
		    }
		default:
		    {
			Console.WriteLine("GatherSystemController:  This should never happen!");
			break;
		    }
	    }
	}
    }

    class NumNodesTarget : Target {

	public NumNodesTarget() : base (15, true, TargetFlags.None ) {
	}

	protected override void OnTarget( Mobile from, object targ ) {
	    if( targ is GatherSystemController ) {
		int count = ((GatherSystemController)targ).System.Nodes.Count;
		from.SendMessage("That Controller has {0} item(s).", count);
	    }
	    else{
		from.SendMessage("You must target a GatherSystemController stone.");
	    }
	}
    }
    
    class SetGatherSystemTarget : Target {

	private int m_Type;
	
	public SetGatherSystemTarget( int type ) : base ( 15, true, TargetFlags.None ) {
	    m_Type = type;
	}

	protected override void OnTarget( Mobile from, object targ ) {
	    if( targ is GatherSystemController ) {
		((GatherSystemController)targ).SetSystemReference( m_Type );
	    }
	    else {
		from.SendMessage("You must target a GatherSystemController stone.");
	    }
	}
    }
}

