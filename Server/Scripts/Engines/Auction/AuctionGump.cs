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

	    //so for e.g. 12 items on sale, numpages() will return 2 and totalItems is 12,
	    // therefore the first page needs to have 8 items and the 2nd page just 4
	    for( int i=1; i<(pages + 1); i++ ){
		AddPage(i);
	
		if( m_Stone.SaleItems.Count >= (i * m_ItemsPerPage) ){

		    for( int k=0; i<9; i++ ){

			int listindex = (i * m_ItemsPerPage) + k;
			AuctionItem ai = m_Stone.SaleItems[ listindex ];
			
			MakeBox( _col1X + (170 * k), _row1Y + (170 * (k/4)), listindex, ai );
		    }

		    AddButton( _col3X, _footerY, 247, 248, (int)Buttons.NextPageButton, GumpButtonType.Reply, 2);
		    AddImageTiled( _col3X, _footerY, 68, 22, 2624 ); //paging button BG
		    AddLabel( _col3X + 20, _footerY + 2, 49, @"Next");

		    AddButton( _col4X, _footerY, 247, 248, (int)Buttons.PrevPageButton, GumpButtonType.Reply, 2);
		    AddImageTiled( _col4X, _footerY, 68, 22, 2624 ); //paging button BG
		    AddLabel( _col4X + 10, _footerY + 2, 49, @"Prev");
		}
		else if( m_Stone.SaleItems.Count == 0 ){
		    AddHtml(_col1X, _row1Y, 460, 100, "Nothing for sale at this time.", false, false);
		}
		else{
		    int itemsthispage = m_Stone.SaleItems.Count - ( (i-1) * m_ItemsPerPage );
	
		    for( int j=1; j < (itemsthispage + 1); j++){

			int listindex = ( (i-1) * m_ItemsPerPage ) + (j-1);
			AuctionItem ai = m_Stone.SaleItems[ listindex ];
			
			MakeBox( _col1X + (170 * (j-1) ), _row1Y + (170 * (j/4)), listindex, ai );

			if( i > 1 ){
			    AddButton( _col4X, _footerY, 247, 248, (int)Buttons.PrevPageButton, GumpButtonType.Reply, 2);
			    AddImageTiled( _col4X, _footerY, 68, 22, 2624 ); //paging button BG
			    AddLabel( _col4X + 10, _footerY + 2, 49, @"Prev");
			}
		    }
		}
	    }
	}

	private void MakeBox( int x, int y, int idx, AuctionItem ai ){
	    AddBackground( x, y, _boxsize, _boxsize, 9300 );
	    AddRadio( x, y, 208, 209, false, idx );
	    AddItem( x + 20, y, ai.SaleItem.ItemID, ai.SaleItem.Hue );
	    AddHtml( x, y + 20, _boxsize, 20, ai.SaleItem.Name == null ? ai.SaleItem.GetType().Name : ai.SaleItem.Name, false, false);
	    AddHtml( x, y + 40, _boxsize, 20, "Amount: " + ai.SaleItem.Amount, false, false);
	    AddHtml( x, y + 60, _boxsize, 20, "Bid: " + (ai.LeadingBid == 0 ? ai.ListPrice.ToString() : ai.LeadingBid.ToString()), false, false);
	    AddHtml( x, y + 80, _boxsize, 20, "Close: " + ai.SellByDate.ToString() , false, false);
	    AddHtml( x, y + 100, _boxsize, 20, "Time: " + ai.SellByDate.Hour.ToString() + ":" + ai.SellByDate.Minute.ToString(), false, false);
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

	    for( int i = 0; i < info.Switches.Length; i++ ) {
		if( info.IsSwitched( i ) ){
		    // i *should* be the index in the master saleitem list of the item we bid on
		    //get text entry amt
		    //register new bid
		}
	    }	    

	    switch( info.ButtonID )
	    {
		case (int)Buttons.ReplyButton:
		    {
			Console.WriteLine(" reply button {0} (should be bid)", info.ButtonID );
			break;
		    }
		case (int)Buttons.NextPageButton:
		    {
			Console.WriteLine(" nextpage (id {0})", info.ButtonID );
			break;
		    }
		case (int)Buttons.PrevPageButton:
		    {
			Console.WriteLine(" prevpage (id {0})", info.ButtonID );
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
