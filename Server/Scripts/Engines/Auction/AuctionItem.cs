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
	
	private DateTime m_LastBid;
	public DateTime LastBid {
	    get { return m_LastBid; }
	    set { m_LastBid = value; }
	}
	
	private Dictionary<PlayerMobile, int> m_Bids;
	public Dictionary<PlayerMobile, int> Bids {
	    get { return m_Bids; }
	    set { m_Bids = value; }
	}
	
	public AuctionItem() : this( null, 0, null ){}

	public AuctionItem( Item saleItem, int listPrice, PlayerMobile seller ) :
	    this( saleItem, listPrice, seller, DateTime.Today, DateTime.Today, DateTime.Today.AddHours(48), new Dictionary<PlayerMobile, int>() ){}
	
	public AuctionItem( Item saleItem, int listPrice, PlayerMobile seller, DateTime listdate, DateTime sellbydate, DateTime lastbid, Dictionary<PlayerMobile, int> bids ){
	    m_Item = saleItem;
	    m_ListPrice = listPrice;
	    m_Seller = seller;
	    m_ListDate = listdate;
	    m_SellByDate = sellbydate;
	    m_LastBid = lastbid;

	    m_Bids = bids;
	}
	
    }
}
