using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Targeting;
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

	public void SetBox( AuctionBox target ){
	    m_Box = target;
	}
	
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
	}

	public void RegisterBid( Mobile from, int amt, int index ){
	    AuctionItem item = m_SaleItems[index];
	    
	    if( ValidateBid( from, amt, item ) ) {
		
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
	    }    
	}

	public bool CheckStorage( Item item ) {
	    if( m_Box.MaxItems > ( m_Box.TotalItems + item.TotalItems ) ){
		if( m_Box.MaxWeight > ( m_Box.TotalWeight + item.TotalWeight ) ){
		    return true;
		}
	    }

	    return false;
	}

	public void AcceptSaleItem( Mobile from, Item item, int askprice ){
	    //take item, create an AuctionItem, move real item to box
	    if( item.Parent == from.Backpack ){
		if( CheckStorage( item ) ){
		    m_Box.DropItem( item );
		    m_SaleItems.Add(new AuctionItem(item,
						    askprice,
						    from as PlayerMobile,
						    null,
						    0,
						    DateTime.Now,
						    DateTime.Now) );
		}
		else {
		    from.SendMessage("The Auction storage is full. This should never happen: please contact server staff.");
		}
	    }
	    else {
		from.SendMessage("The item must be in your backpack.");
	    }
	}

	public void DispenseSaleItem( Mobile to, AuctionItem ai ){
	    //give item to To, in their bank box, then delete auctionitem from list.
	    if( to.BankBox.TryDropItem(to, ai.SaleItem, true) ){
		to.SendMessage("An item you bid on at the auction has been placed in your bankbox!");
		
	    }
	    else {
		to.SendMessage("Your bankbox is full and you were unable to receive an auction item!  Please contact shard staff.");
		//what do we do now
	    }
	    
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
	    if( amount <= item.LeadingBid ) {
		bidder.SendMessage("Your bid must be greater than {0} gold piece(s).", item.LeadingBid );
		return false;
	    }

	    if( bidder == item.LeadingBidder ) {
		bidder.SendMessage("You are already the leading bidder on that item.");
		return false;
	    }

	    if( bidder.BankBox.GetAmount( typeof(Gold), true) <= amount ){
		bidder.SendMessage("You have insufficient gold in your bank box to place this bid.");
		return false;
	    }

	    return true;
	}

	public override void OnDoubleClick(Mobile from){
	    from.SendGump( new AuctionGump( from, this ) );
	}

	public override void Serialize( GenericWriter writer ) {
	    base.Serialize( writer );
	    writer.Write( (int) 0 ); //version

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
		}
	    }
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );
	    int version = reader.ReadInt();

	    int listprice, leadingbid;
	    PlayerMobile seller, bidder;
	    DateTime listdate, biddate;
	    Item saleitem;

	    switch( version ){
		case 0:
		    int saleItemsCount = reader.ReadInt();
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
			    
			    m_SaleItems.Add( new AuctionItem(saleitem, listprice, seller, bidder, leadingbid, listdate, biddate) );
			}
		    }
		    break;
	    }
	}

	private class FirstInternalTarget : Target {
	    private AuctionController m_Stone;
	    
	    public FirstInternalTarget( ) : base( -1, true, TargetFlags.None ) {}

	    protected override void OnTarget( Mobile from, object targeted ) {
		if( targeted is AuctionController ){
		    m_Stone = (AuctionController)targeted;
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
	    
	    public SecondInternalTarget( AuctionController stone ) : base( -1, true, TargetFlags.None ) {
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

	public class AuctionBox : BaseContainer {

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

		int version = reader.ReadInt();
	    }
	}
    }
}
