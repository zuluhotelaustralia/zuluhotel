using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Items;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Auction;

/*
  The general idea here is that you have a stone that controls everything.  It is linked to a physical box
  that contains the items being held in escrow for sale.  Upon registering a bid the controller stone
  causes the auctioneers to announce a new bid or item for sale.
*/
namespace Server.Items {
    public class AuctionController : Item {

	public static void Initialize() {
	    CommandSystem.Register( "SetAuctionBox", AccessLevel.Developer, new CommandEventHandler( SetAuctionBox_OnCommand ) );
	    CommandSystem.Register( "ListAuctionItems", AccessLevel.Developer, new CommandEventHandler( ListAuctionItems_OnCommand ) );
	    CommandSystem.Register( "SellAuctionItem", AccessLevel.Player, new CommandEventHandler( SellAuctionItem_OnCommand ) );

	    Auctioneer.SetStone( AuctionStone );
	}

	public static void SellAuctionItem_OnCommand( CommandEventArgs e ){
	    bool ok = false;

	    if( e.Length != 1 ){
		e.Mobile.SendMessage("Usage: SellAuctionItem <sell price>");
		return;
	    }
	    
	    foreach( Mobile m in e.Mobile.GetMobilesInRange( 8 ) ){
		if( m is Auctioneer ){
		    ok = true;
		    break;		       
		}
	    }

	    int askprice = e.GetInt32(0);

	    if( ok ){
		e.Mobile.SendMessage("Select the item you wish to sell for {0} gold pieces.", askprice );
		e.Mobile.Target = new AuctionSellTarget( askprice );
	    }
	    else{
		e.Mobile.SendMessage("You must be within 8 tiles of the Auctioneer to sell items on the Auction.");
	    }
	}

	private static AuctionController _auctionstone;
	public static AuctionController AuctionStone {
	    get { return _auctionstone; }
	    set { _auctionstone = value; }
	}
	
	public static void SetAuctionBox_OnCommand( CommandEventArgs e ){
	    e.Mobile.SendMessage("Target the stone.");
	    e.Mobile.Target = new FirstInternalTarget();
	}

	public static void ListAuctionItems_OnCommand( CommandEventArgs e ){
	    e.Mobile.SendMessage("Target the stone.");
	    e.Mobile.Target = new ListTarget();
	}
	    
	private AuctionBox m_Box;
	private const double m_Take = 0.1; //10% fee

	private TimeSpan[] _BidExtensions = { TimeSpan.FromHours(12.0),
	    TimeSpan.FromHours(8.0), TimeSpan.FromHours(4.0), TimeSpan.FromHours(1.0),
	    TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(1.0)};

	public void SetBox( AuctionBox target ){
	    m_Box = target;
	}

	private AuctionTimer _Timer;
	
	private List<AuctionItem> m_SaleItems;
	public List<AuctionItem> SaleItems{
	    get { return m_SaleItems; }
	}
	
	[Constructable]
	public AuctionController() : base ( 0xED4 ){
	    this.Name = "Auction Controller Stone";
	    this.Hue = 2765;

	    if( m_SaleItems == null ){
		m_SaleItems = new List<AuctionItem>();
	    }
	}

	public AuctionController( Serial serial ) : base( serial ){
	    this.Name = "Auction Controller Stone";
	    this.Hue = 2765;

	    if( m_SaleItems == null ){
		m_SaleItems = new List<AuctionItem>();
	    }

	    _auctionstone = this;
	}

	// the auction timer calls this every minute
	public void FinalizeSales() {
	    //if $rightnow is after the sellbydate, then pay the seller, give item to the bidder,
	    // and delete the auctionitem from the list.
	    for( int i = m_SaleItems.Count - 1; i >= 0; i-- ){
		if( DateTime.Compare( m_SaleItems[i].SellByDate, DateTime.Now ) <= 0 ){
		    if( m_SaleItems[i].LeadingBidder == null ){
			m_SaleItems[i].Seller.BankBox.DropItem( m_SaleItems[i].SaleItem ); //return to seller
			m_SaleItems.RemoveAt( i );
			continue;
		    }
		    double amount = (double) m_SaleItems[i].LeadingBid;
		    amount *= (1.0 - m_Take);
		    
		    m_SaleItems[i].Seller.BankBox.DropItem( new Gold( (int)amount ) );
		    DispenseSaleItem( m_SaleItems[i].LeadingBidder, m_SaleItems[i] );
		    m_SaleItems.RemoveAt(i);
		}
		
	    }

	    if( m_SaleItems.Count > 0 ){
		_Timer = new AuctionTimer( this );
		_Timer.Start();
	    }
	}


	public void RegisterBid( Mobile from, int amt, AuctionItem item ){
	    	    
	    if( ValidateBid( from, amt, item )  ) {
		
		//if we're here then from has placed a potential winning bid
		//check if we need to refund someone first
		if( item.LeadingBidder != null ){
		    PlayerMobile oldbidder = item.LeadingBidder;
		    RefundBid( oldbidder, item.LeadingBid );
		}
		
		//now place new bid
		from.BankBox.ConsumeTotal( typeof(Gold), amt, true );
		item.LeadingBid = amt;
		item.LeadingBidder = from as PlayerMobile;
		item.LastBidDate = DateTime.Now;
		item.Bids++;

		if(item.Bids <= _BidExtensions.Length){
		    item.SellByDate = item.SellByDate.Add( _BidExtensions[item.Bids - 1] );
		}
		else {
		    item.SellByDate = item.SellByDate.Add( TimeSpan.FromMinutes( 1.0 ) );
		}
	    }    
	}

	public void AcceptSaleItem( Mobile from, Item item, int askprice ){
	    //take item, create an AuctionItem, move real item to box
	    if( item.Parent == from.Backpack ){
		m_Box.DropItem( item );
		m_SaleItems.Add(new AuctionItem(item, askprice, from as PlayerMobile, null, 0, DateTime.Now, DateTime.Now, 0, item.Amount) );
		if( _Timer == null || _Timer.Running == false ){
		    _Timer = new AuctionTimer( this );
		    _Timer.Start();
		}
	    }
	    else {
		from.SendMessage("The item must be in your backpack.");
	    }
	}
	
	public void DispenseSaleItem( Mobile to, AuctionItem ai ){
	    //give item to To, in their bank box, then delete auctionitem from list.
	    to.BankBox.DropItem( ai.SaleItem );
	    to.SendMessage("An item you bid on at the auction has been placed in your bankbox!");
	    TownCrier.AddAnnouncement( "New Britain's Auction House has closed a sale to " + to.Name + "!");
	}

	public void RefundBid( Mobile bidder, int amt ){
	    // give them their amount back
	    //TODO what do if their bank is full??
	    bidder.BankBox.TryDropItem(bidder, new Server.Items.Gold( amt ), true);
	    bidder.SendMessage("You were outbid on an auction item and your bid has been refunded to your bank account." );
	    
	}

	public bool ValidateBid( Mobile bidder, int amount, AuctionItem item ){
	    //make sure they have gold in their bank to cover it
	    //make sure they aren't already the leading bidders

	    if( !bidder.Alive ){
		bidder.SendMessage("You cannot do that while dead.");
	    }

	    if( bidder == item.LeadingBidder ) {
		bidder.SendMessage("You are already the leading bidder on that item.");
		return false;
	    }
	    
	    if( item.LeadingBid > item.ListPrice ){
		if( amount <= item.LeadingBid ) {
		    //the bid is above the listprice but they didn't outbid the leader so show them the leading bid
		    bidder.SendMessage("Your bid must be greater than {0} gold piece(s).", item.LeadingBid );
		    return false;
		}
	    }
	    else if( amount <= item.ListPrice ) {
		// at this point there are theoretically no bids because leadingbid <= listprice
		// they haven't beat the listprice so show them that as the target
		bidder.SendMessage("Your bid must be greater than {0} gold piece(s).", item.ListPrice );
		return false;
	    }
	    
	    if( bidder.BankBox.GetAmount( typeof(Gold), true) <= amount ){
		bidder.SendMessage("You have insufficient gold in your bank box to place this bid.");
		return false;
	    }

	    bool ok = false;
	    foreach( Mobile m in bidder.GetMobilesInRange( 8 ) ){
		if( m is Auctioneer ){
		    ok = true;
		    break;		       
		}
	    }
	    if( !ok ){
		bidder.SendMessage("You are too far away from the Auctioneer!");
		return false;
	    }

	    if( !m_SaleItems.Contains( item ) ){
		bidder.SendMessage("Invalid bid, perhaps the sale has ended already?");
		return false;
	    }

	    if( bidder == item.Seller ){
		bidder.SendMessage("You cannot bid on your own items.");
		return false;
	    }
	    
	    bidder.SendMessage("Your bid of {0} gold pieces is accepted!", amount );
	    return true;
	}

	public override void OnDoubleClick(Mobile from){
	    from.SendGump( new AuctionGump( from, this ) );
	}

	public override void Serialize( GenericWriter writer ) {
	    base.Serialize( writer );
	    writer.Write( (int) 1 ); //version

	    writer.Write( m_SaleItems.Count );
	    if( m_SaleItems.Count != 0 ){
		foreach( AuctionItem ai in m_SaleItems ){
		    writer.Write( ai.SaleItem );
		    writer.Write( ai.ListPrice );
		    writer.Write( ai.Seller );
		    writer.Write( ai.LeadingBidder );
		    writer.Write( ai.LeadingBid );
		    writer.Write( ai.ListDate );
		    writer.Write( ai.LastBidDate );
		    writer.Write( ai.Bids );
		    writer.Write( ai.Amount );
		}
	    }
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );
	    int version = reader.ReadInt();

	    int listprice, leadingbid, bids, amount;
	    PlayerMobile seller, bidder;
	    DateTime listdate, biddate;
	    Item saleitem;

	    int saleItemsCount = reader.ReadInt();
	    
	    switch( version ){
		case 1:

		    m_SaleItems = new List<AuctionItem>();
		    if( saleItemsCount == 0 ){
			return;
		    }
		    else {
			for( int i=0; i<saleItemsCount; i++ ){
			    
			    saleitem = reader.ReadItem() as Item;
			    listprice = reader.ReadInt();
			    seller = reader.ReadMobile() as PlayerMobile;
			    bidder = reader.ReadMobile() as PlayerMobile;
			    leadingbid = reader.ReadInt();
			    listdate = reader.ReadDateTime();
			    biddate = reader.ReadDateTime();
			    bids = reader.ReadInt();
			    amount = reader.ReadInt();
			    
			    m_SaleItems.Add( new AuctionItem(saleitem, listprice, seller, bidder, leadingbid, listdate, biddate, bids, amount) );
			}
		    }
		    break;
		case 0:
	
		    m_SaleItems = new List<AuctionItem>();
		    if( saleItemsCount == 0 ){
			return;
		    }
		    else {
			for( int i=0; i<saleItemsCount; i++ ){
			    
			    saleitem = reader.ReadItem() as Item;
			    listprice = reader.ReadInt();
			    seller = reader.ReadMobile() as PlayerMobile;
			    bidder = reader.ReadMobile() as PlayerMobile;
			    leadingbid = reader.ReadInt();
			    listdate = reader.ReadDateTime();
			    biddate = reader.ReadDateTime();
			    bids = reader.ReadInt();
			    amount = 1;
			    
			    m_SaleItems.Add( new AuctionItem(saleitem, listprice, seller, bidder, leadingbid, listdate, biddate, bids, amount) );
			}
		    }
		    break;
	    } //switch
	}

	private class AuctionTimer : Timer{
	    private AuctionController m_Stone;
	    public AuctionTimer( AuctionController s ) : base( TimeSpan.FromMinutes( 1.0 ) )
	    {
		m_Stone = s;
	    }
	    
	    protected override void OnTick()
	    {
		m_Stone.FinalizeSales();
	    }
	}

	private class AuctionSellTarget : Target {
	    private int _price;
	    public AuctionSellTarget( int price ) : base( -1, false, TargetFlags.None ) {
		_price = price;
	    }

	    protected override void OnTarget( Mobile from, object targeted ) {
		if( targeted is Item ){
		    Item item = targeted as Item;

		    AuctionStone.AcceptSaleItem( from, item, _price );
		}
		else{
		    from.SendMessage("You can only target items.");
		}
	    }
	}
	
	private class FirstInternalTarget : Target {
	    private AuctionController m_Stone;
	    
	    public FirstInternalTarget( ) : base( -1, false, TargetFlags.None ) {}

	    protected override void OnTarget( Mobile from, object targeted ) {
		if( targeted is AuctionController ){
		    m_Stone = (AuctionController)targeted;
		    AuctionController.AuctionStone = m_Stone;
		    from.SendMessage("Now target the Box");
		    from.Target = new SecondInternalTarget( m_Stone );
		}
		else{
		    from.SendMessage("bruh");
		}
	    }
	}

	private class SecondInternalTarget : Target {
	    private AuctionController m_Stone;
	    
	    public SecondInternalTarget( AuctionController stone ) : base( -1, false, TargetFlags.None ) {
		m_Stone = stone;
	    }

	    protected override void OnTarget( Mobile from, object targeted ) {
		if( targeted is AuctionBox ){
		    m_Stone.SetBox( (AuctionBox)targeted );
		    Auctioneer.SetStone( m_Stone ); //yeahhh i know
		}
		else{
		    from.SendMessage("bruh");
		}
	    }
	}

	private class ListTarget : Target {
	    public ListTarget() : base( -1, true, TargetFlags.None ){}
		
	    protected override void OnTarget( Mobile from, object targ ){
		if( targ is AuctionController ){
		    AuctionController stone = targ as AuctionController;

		    foreach( AuctionItem ai in stone.SaleItems ){
			Console.WriteLine("A(n) {0}, for sale by {1}.", ai.SaleItem, ai.Seller );
		    }
		}
	    }
	}
    }
    public class AuctionBox : BaseContainer {

	private static AuctionBox _box;
	
	public static void Initialize(){
	    AuctionController.AuctionStone.SetBox( _box );
	}
	
	[Constructable]
	public AuctionBox() : base( 0xE41 ){
	    this.Name = "Auction Escrow Box";
	}

	public AuctionBox( Serial serial ) : base( serial ){
	    this.Name = "Auction Escrow Box";
	}

	public override bool IsAccessibleTo( Mobile m ) {
	    if( m.AccessLevel >= AccessLevel.Developer ) {
		return true;
	    }

	    return false;
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize( writer );
	    
	    writer.Write( (int) 1 ); // version
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );

	    _box = this;
	    int version = reader.ReadInt();
	}
    }
}
