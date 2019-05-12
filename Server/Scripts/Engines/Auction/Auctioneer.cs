using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Auction;
using Server.Gumps;

namespace Server.Mobiles{
    public class Auctioneer : Mobile {
	
	private static List<Auctioneer> m_Instances = new List<Auctioneer>();
	private static DateTime m_NextShout;
	private static AuctionController m_Stone;

	public static void SetStone( AuctionController stone ){
	    m_Stone = stone;
	}
    
	public static List<Auctioneer> Instances
	{
	    get{ return m_Instances; }
	}

	public static void Announce( string what ){
	    if( DateTime.Compare(m_NextShout, DateTime.Now) <= 0 ){
		foreach( Auctioneer a in m_Instances ){
		    a.Say( what );
		}

		m_NextShout = DateTime.Now.AddMinutes(5.0);
	    }
	}
	
	public override void OnDoubleClick( Mobile from ){
	    from.SendGump( new AuctionGump(from, m_Stone) );
	}
	
	[Constructable]
	public Auctioneer()
	{
	    m_Instances.Add( this );

	    InitStats( 100, 100, 25 );

	    Title = "the auctioneer";
	    Hue = Utility.RandomSkinHue();

	    if ( !Core.AOS )
		NameHue = 0x35;

	    if ( this.Female = Utility.RandomBool() )
	    {
		this.Body = 0x191;
		this.Name = NameList.RandomName( "female" );
	    }
	    else
	    {
		this.Body = 0x190;
		this.Name = NameList.RandomName( "male" );
	    }

	    AddItem( new FancyShirt( Utility.RandomBrightHue() ) );

	    Item skirt;

	    switch ( Utility.Random( 2 ) )
	    {
		case 0: skirt = new Skirt(); break;
		default: case 1: skirt = new Kilt(); break;
	    }

	    skirt.Hue = Utility.RandomGreenHue();

	    AddItem( skirt );

	    AddItem( new FeatheredHat( Utility.RandomBlueHue() ) );

	    Item boots;

	    switch ( Utility.Random( 2 ) )
	    {
		case 0: boots = new Boots(); break;
		default: case 1: boots = new ThighBoots(); break;
	    }

	    AddItem( boots );

	    Utility.AssignRandomHair( this );
	}

	public override bool CanBeDamaged()
	{
	    return false;
	}

	public override void OnDelete()
	{
	    m_Instances.Remove( this );
	    base.OnDelete();
	}

	public Auctioneer( Serial serial ) : base( serial )
	{
	    m_Instances.Add( this );
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); // version
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();

	    if ( Core.AOS && NameHue == 0x35 )
		NameHue = -1;
	}
    }
}
