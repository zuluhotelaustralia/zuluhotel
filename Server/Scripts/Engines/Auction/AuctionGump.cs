using System;
using System.Collections;
using System.Collections.Generic;
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
	private const int m_ItemsPerPage = 8;

	private const int _col1X = 20;
	private const int _col2X = 190;
	private const int _col3X = 360;
	private const int _col4X = 530;
	private const int _row1Y = 50;
	private const int _row2Y = 220;
	private const int _footerY = 400;

	private const int _boxsize = 150;
	private const int _boxspacing = 20;

	private const int _windowX = 700;
	private const int _windowY = 500;
	
	public AuctionGump( Mobile from, AuctionController stone ) : base( 100, 100 )
	{
	    m_Viewer = from;
	    m_Stone = stone;
	    
	    Closable = true;
	    Disposable = true;
	    Dragable = true;
	    Resizable = false;

	    int pages = NumPages();

	    //public void AddImageTiled( int x, int y, int width, int height, int gumpID )
	    //AddButton( X, Y, UnCheckedGumpID, CheckedGumpID, StartChecked?, SwitchID ); 
	
	    AddPage(0);
	    AddBackground( 0, 0, _windowX, _windowY, 9300 );
	    AddHtml( 20, 10, 460, 100, "<h2>Auction Items for Sale</h2>", false, false );

	    AddImageTiled( _col1X, _footerY, 150, 22, 0 );
	    AddTextEntry( _col1X + 10, _footerY, 140, 50, 49, 0, "" );
	    
	    AddButton( _col2X, _footerY, 247, 248, (int)Buttons.ReplyButton, GumpButtonType.Reply, 2);
	    AddImageTiled( _col2X, _footerY, 68, 22, 2624); // Submit Button BG
	    AddLabel( _col2X + 10, _footerY + 2, 49, @"Bid");

//	    for( i=0; i<pages; i++ ){
	//	AddPage(i);
		
//		List<AuctionItem> sublist = new List<AuctionItem>();
	    AddBackground( _col1X, _row1Y, _boxsize, _boxsize, 9300 );
	    AddRadio( _col1X, _row1Y, 208, 209, false, 1 );
	    AddBackground( _col2X, _row1Y, _boxsize, _boxsize, 9300 );
	    AddBackground( _col3X, _row1Y, _boxsize, _boxsize, 9300 );
	    AddBackground( _col4X, _row1Y, _boxsize, _boxsize, 9300 );

	    AddBackground( _col1X, _row2Y, _boxsize, _boxsize, 9300 );
	    AddBackground( _col2X, _row2Y, _boxsize, _boxsize, 9300 );
	    AddBackground( _col3X, _row2Y, _boxsize, _boxsize, 9300 );
	    AddBackground( _col4X, _row2Y, _boxsize, _boxsize, 9300 );
	}

	public int NumPages() {
	    if( m_Stone.SaleItems == null ) {
		return 0;
	    }
	    
	    int totalItems = m_Stone.SaleItems.Count;
	    int pages = totalItems / m_ItemsPerPage; //intentional integer division
	    int remainder = totalItems - (pages * m_ItemsPerPage);

	    if( remainder != 0 ){
		pages++;
	    }
	    
	    return pages;
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
