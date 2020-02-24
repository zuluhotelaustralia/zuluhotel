using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Auction {
    // an item that's for sale on the auction
    public class AuctionItem {
	private Item m_Item;
	public Item SaleItem{
	    get { return m_Item; }
	    set { m_Item = value; }
	}
	
	private int m_ListPrice;
	public int ListPrice{
	    get { return m_ListPrice; }
	    set { m_ListPrice = value; }
	}
	
	private PlayerMobile m_Seller;
	public PlayerMobile Seller {
	    get { return m_Seller; }
	    set { m_Seller = value; }
	}
	
	private DateTime m_ListDate;
	public DateTime ListDate {
	    get { return m_ListDate; }
	    set { m_ListDate = value; }
	}
	private DateTime m_SellByDate;
	public DateTime SellByDate {
	    get { return m_SellByDate; }
	    set { m_SellByDate = value; }
	}
	
	private DateTime m_LastBidDate;
	public DateTime LastBidDate {
	    get { return m_LastBidDate; }
	    set { m_LastBidDate = value; }
	}

	private int m_LeadingBid;
	public int LeadingBid {
	    get { return m_LeadingBid; }
	    set { m_LeadingBid = value; }
	}
	
        private PlayerMobile m_LeadingBidder;
	public PlayerMobile LeadingBidder {
	    get { return m_LeadingBidder; }
	    set { m_LeadingBidder = value; }
	}

	private int _bids;
	public int Bids{
	    get { return _bids; }
	    set { _bids = value; }
	}

	private int _amount;
	public int Amount{
	    get{ return _amount; }
	    set{ _amount = value; }
	}
	
	public AuctionItem( Item saleitem, int listprice, PlayerMobile seller, PlayerMobile bidder, int leadingbid, DateTime listdate, DateTime biddate, int bids, int amount ){
	    m_Item = saleitem;
	    m_ListPrice = listprice;
	    m_Seller = seller;
	    m_LeadingBidder = bidder;
	    m_LeadingBid = leadingbid;
	    m_ListDate = listdate;
	    m_SellByDate = m_ListDate.AddHours( 24.0 );
	    m_LastBidDate = biddate;
	    _bids = bids;
	    _amount = amount;
	}
	
    }
}
