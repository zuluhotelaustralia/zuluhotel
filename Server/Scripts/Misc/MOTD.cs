using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps {
    public class MOTDGump : Gump {
	public static void Initialize() {
	    CommandSystem.Register("MOTD", AccessLevel.Player, new CommandEventHandler( MOTD_OnCommand ) );
	    EventSink.Login += new LoginEventHandler( EventSink_Login );
	}

	public static void MOTD_OnCommand( CommandEventArgs e ){
	    e.Mobile.SendGump( new MOTDGump( e.Mobile ) );
	}

	private static void EventSink_Login( LoginEventArgs args ){
	    Mobile m = args.Mobile;

	    m.SendGump( new MOTDGump( m ) );
	}

	public enum Buttons {
	    Exit,
	    OKButton
	}

	public MOTDGump( Mobile from ) : base( 100, 100 ){

	    Closable = true;
	    Disposable = true;
	    Dragable = true;
	    Resizable = false;

	    AddPage(0);
	    AddBackground( 0, 0, 480, 320, 9300 );
	    AddHtml( 10, 10, 460, 100, "<h2>Update: 14 January 2020</h2>", false, false );
	    AddHtml( 10, 50, 460, 140, "Welcome to Zulu Hotel Canada, brought to you by Sith and Daleron.  This shard is currently in Beta Testing, and so may have bugs.  We ask that you please be patient with us.  Current testing priority is crafting:  Please try to craft everything you can, and report bugs that you find!  Note that we have updated new clilocs for the client, however Daleron's updater is not currently ready for production, so we have put the cliloc file up on https://zuluhotel.ca for download.  Latest feature change:  Spec modifier is now 10% per level.  Known issue:  Crafters are not getting spec bonuses right now.  A fix is in the works but that system is complex so there's lots to test.", false, false );
	    
	    AddButton( 10, 280, 247, 248, (int)Buttons.OKButton, GumpButtonType.Reply, 2);
	    AddImageTiled( 10, 280, 68, 22, 2624);
	    AddLabel( 20, 282, 49, @"Okay");
	}

	public override void OnResponse( NetState state, RelayInfo info ){
	    Mobile from = state.Mobile;

	    switch( info.ButtonID ){
		case (int) Buttons.OKButton:
		    {
			from.SendMessage("Thanks for reading the MOTD.  To call this window up again, just use the MOTD command.");
			break;
		    }
	    }
	}
    }
}
