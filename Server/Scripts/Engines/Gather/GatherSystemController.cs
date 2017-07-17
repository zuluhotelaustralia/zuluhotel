using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Harvest;
using Server.Engines.Gather;
using Server.Commands;
using Server.Targeting;

namespace Server.Items {
    public class GatherSystemController : Item {

	public enum ControlledSystem{
	    None,
	    Mining,
	    Lumberjacking,
	    Fishing
	}

	private ControlledSystem m_ControlledSystem = ControlledSystem.None;
	
	private GatherSystem m_System;
	public GatherSystem System {
	    get {
		return m_System;
	    }
	    set {
		m_System = value;
	    }
	}

	public GatherSystemController() : base ( 0xED4 ) {
	}
	public GatherSystemController( Serial serial ) : base( serial ){
	}
	
	public static void Initialize() {
	    CommandSystem.Register( "SetGatherSystem", AccessLevel.Developer, new CommandEventHandler( SetGatherSystem_OnCommand ) );
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
	    m_ControlledSystem = (ControlledSystem)cs;
	    
	    switch( m_ControlledSystem )
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

	    writer.Write( (int)m_ControlledSystem );

	}

	public override void Deserialize( GenericReader reader ) {
	    int version = reader.ReadInt();

	    switch( version )
	    {
		case 0:
		    {
			int cs = reader.ReadInt(); //ControlledSystem
			SetSystemReference( cs );
			
			break;
		    }
		default:
		    {
			Console.WriteLine("GatherSystemController:  This should never happen!");
			break;
		    }
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
