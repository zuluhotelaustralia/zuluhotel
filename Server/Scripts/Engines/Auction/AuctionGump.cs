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
	    // add a "sell an item" button here




	    //so for 12 items on sale, numpages() will return 2 and totalItems is 12,
	    // therefore the first page needs to have 8 items and the 2nd page just 4
	    for( int i=1; i<(pages + 1); i++ ){
		AddPage(i);

		if( m_Stone.SaleItems.Count >= (i * m_ItemsPerPage) ){
		    AddBackground( _col1X, _row1Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col1X, _row1Y, 208, 209, false, 1 );
		    AddBackground( _col2X, _row1Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col2X, _row1Y, 208, 209, false, 2 );
		    AddBackground( _col3X, _row1Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col3X, _row1Y, 208, 209, false, 3 );
		    AddBackground( _col4X, _row1Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col4X, _row1Y, 208, 209, false, 4 );
		    
		    AddBackground( _col1X, _row2Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col1X, _row2Y, 208, 209, false, 5 );
		    AddBackground( _col2X, _row2Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col2X, _row2Y, 208, 209, false, 6 );
		    AddBackground( _col3X, _row2Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col3X, _row2Y, 208, 209, false, 7 );
		    AddBackground( _col4X, _row2Y, _boxsize, _boxsize, 9300 );
		    AddRadio( _col4X, _row2Y, 208, 209, false, 8 );
		}
		else{
		    int itemsthispage = m_Stone.SaleItems.Count - ( (i-1) * m_ItemsPerPage );
		    for( int j=1; j<itemsthispage; j++){
			AddBackground( _col1X + (170 * j), _row1Y + (170 * (j/4)), _boxsize, _boxsize, 9300 );
			AddRadio( _col1X + (170 * j), _row1Y + (170 * (j/4)), 208, 209, false, j );
		    }
		}

		//add paging controls here
	    }
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

    public class AuctionSellGump : Gump {
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

	private int _windowX = 320;
	private int _windowY = 240;

	public AuctionSellGump( Mobile from, AuctionController stone ) : base( 100, 100 )
	{
	    m_Viewer = from;
	    m_Stone = stone;
	    
	    Closable = true;
	    Disposable = true;
	    Dragable = true;
	    Resizable = false;

	    //int pages = NumPages();

	    //public void AddImageTiled( int x, int y, int width, int height, int gumpID )
	    //AddButton( X, Y, UnCheckedGumpID, CheckedGumpID, StartChecked?, SwitchID ); 
	
	    AddPage(0);
	    AddBackground( 0, 0, _windowX, _windowY, 9300 );
	    AddHtml( 20, 10, 460, 100, "<h2>List an item for auction.</h2>", false, false );
	}
    }
}
