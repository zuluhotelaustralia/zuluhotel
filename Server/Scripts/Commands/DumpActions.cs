using System;
using Server;
using Server.Commands;
using Server.Targeting;

// miscellaneous debugging and utility function that I didn't want to clutter other files with
// --sith
namespace Server {
    public static class DevTools {

	public static void Initialize(){
	    // dump a mobile's _actions variable
	    CommandSystem.Register("DumpActions", AccessLevel.Developer, new CommandEventHandler( DumpActions_OnCommand) );
	}

	public static void DumpActions_OnCommand( CommandEventArgs e ){
	    e.Mobile.Target = new DumpActionsTarget();
	}

    }

    public class DumpActionsTarget : Target {
	public DumpActionsTarget() : base( 15, true, TargetFlags.None) {}

	protected override void OnTarget( Mobile from, object targ ) {
	    if( targ is Mobile ){
		Mobile m = targ as Mobile;
		if( m.UnderscoreActions != null ){
		    foreach( object o in m.UnderscoreActions ){
			from.SendMessage(o.ToString());
		    }
		}
	    }
	    else {
		from.SendMessage("bruh");
	    }
	}
    }
}
