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

	[CommandProperty(AccessLevel.Developer)]
	public AuctionBox Box {
	    get { return m_Box; }
	    set { m_Box = value; }
	}
	
	private List<AuctionItem> m_SaleItems;
	public List<AuctionItem> SaleItems{
	    get { return m_SaleItems; }
	}
	
	[Constructable]
	public AuctionController() : base ( 0xED4 ){
	    this.Name = "Auction Controller Stone";
	    this.Hue = 2765;
	}

	public AuctionController( Serial serial ) : base( serial ){
	    this.Name = "Auction Controller Stone";
	    this.Hue = 2765;
	}

	public override void OnDoubleClick(Mobile from){
	    from.SendGump( new AuctionGump( from ) );
	}

	public override void Serialize( GenericWriter writer ) {
	    base.Serialize( writer );
	    writer.Write( (int) 0 ); //version

	    writer.Write( m_SaleItems.Count );
	    if( m_SaleItems.Count != 0 ){
		foreach( AuctionItem ai in m_SaleItems ){

		    writer.Write( ai.Bids.Count );
		    foreach( KeyValuePair<PlayerMobile, int> entry in ai.Bids ){
			writer.Write( entry.Key );
			writer.Write( entry.Value );
		    }

		    writer.Write( ai.SaleItem );
		    writer.Write( ai.ListPrice );
		    writer.Write( ai.Seller );
		    writer.Write( ai.ListDate );
		    writer.Write( ai.SellByDate );
		    writer.Write( ai.LastBid );
		}
	    }
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );
	    int version = reader.ReadInt();

	    switch( version ){
		case 0:
		    int saleItemsCount = reader.ReadInt();
		    m_SaleItems = new List<AuctionItem>();
		    if( saleItemsCount == 0 ){
			return;
		    }
		    else {
			for( int i=0; i<saleItemsCount; i++ ){
			    int bidscount = reader.ReadInt();
			    Dictionary<PlayerMobile, int> bidslist = new Dictionary<PlayerMobile, int>();
			    for( int j=0; j<bidscount; j++ ){
				bidslist.Add(reader.ReadMobile() as PlayerMobile, reader.ReadInt() );
			    }
			    m_SaleItems.Add(new AuctionItem(reader.ReadItem(),
							    reader.ReadInt(),
							    reader.ReadMobile() as PlayerMobile,
							    reader.ReadDateTime(),
							    reader.ReadDateTime(),
							    reader.ReadDateTime(),
							    bidslist));
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
		    m_Stone.Box = (AuctionBox)targeted;
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
