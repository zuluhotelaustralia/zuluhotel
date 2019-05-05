using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Auction{
    public class AuctionGump : Gump
    {	
	public enum Buttons
	    {
		Exit,
		NextPageButton,
		PrevPageButton,
		BidButton,
		ReplyButton
	    }

	private Mobile m_Viewer;
	private AuctionController m_Stone;
	
	public AuctionGump( Mobile from ) : base( 100, 100 )
	{
	    m_Viewer = from;
	    
	    Closable = true;
	    Disposable = true;
	    Dragable = true;
	    Resizable = false;

	    AddPage(0);
	    AddBackground( 0, 0, 480, 320, 9300 );
	    AddHtml( 10, 10, 460, 100, "<h2>Auction</h2>", false, false );
	    
	    int num1 = Utility.Dice(1, 10, 0);
	    int num2 = Utility.Dice(1, 10, 0);
	    
	    AddHtml( 60, 200, 50, 50, "<b>" + num1 + "</b>", false, false );
	    AddHtml( 120, 200, 50, 50, "<b>+</b>", false, false );
	    AddHtml( 180, 200, 50, 50, "<b>" + num2 + "</b>", false, false );
	    AddHtml( 240, 200, 50, 50, "<b>=</b>", false, false );
	    AddImageTiled( 280, 200, 50, 22, 0 );
	    AddTextEntry( 290, 200, 50, 50, 49, 0, "" );
	    
	    AddButton(10, 280, 247, 248, (int)Buttons.ReplyButton, GumpButtonType.Reply, 2);
	    AddImageTiled( 10, 280, 68, 22, 2624); // Submit Button BG
	    AddLabel(20, 282, 49, @"Bid");
	}

	public override void OnResponse( NetState state, RelayInfo info )
	{
	    Mobile from = state.Mobile;

	    switch( info.ButtonID )
	    {
		case (int)Buttons.ReplyButton:
		    {
			break;
		    }
	    }
	}
    }
}
