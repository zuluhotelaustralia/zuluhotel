using System;
using System.Text;
using System.Collections.Generic;
using Server;
using Server.Prompts;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Misc;
using Server.Commands;

// this ain't your daddy's town crier.  It has a simple API that any other module can
//call into.  Just call TownCrier.AddAnnouncement and the criers will shout your shit anywhere
//
// to prevent spam there's a timer with a queue that limits speech to once per minute

namespace Server.Mobiles
{
    public class TownCrier : Mobile
    {
	private class InternalTimer : Timer
	{
	    public InternalTimer() : base( TimeSpan.FromMinutes( 1.0) )
	    {}

	    protected override void OnTick()
	    {
		Announce();
	    }
	}
	
	private static InternalTimer m_TalkTimer;
	private static Queue<string> _talkqueue;
	private static List<TownCrier> _Criers = new List<TownCrier>();

	public static List<TownCrier> Criers
	{
	    get{ return _Criers; }
	}

	public static void Initialize(){
	    CommandSystem.Register("towncriermsg", AccessLevel.GameMaster, new CommandEventHandler( TownCrierMsg_OnCommand ) );
	    //TODO:  add a way to bypass the queue for urgent in-character messages that you wouldn't use [broadcast for
	}

	[Usage( "TownCrierMsg <message>" )]
	[Description( "Manually adds a string of text as an item to the Town Crier queue." )]
	public static void TownCrierMsg_OnCommand( CommandEventArgs e )
	{
	    if( e.Arguments.Length != 1 ){
		AddAnnouncement( e.Arguments[1] );
	    }
	    else {
		e.Mobile.SendMessage("Usage:  [towncriermsg <message to send>" );
	    }
	}


	public static void AddAnnouncement( string toAnnounce ){
	    _talkqueue.Enqueue( toAnnounce );
	    if( m_TalkTimer == null || !m_TalkTimer.Running ){
		m_TalkTimer = new InternalTimer();
		m_TalkTimer.Start();
	    }
	}

	private static void Announce() {
	    if( _talkqueue.Count > 0 ){
		foreach( TownCrier tc in _Criers ){
		    tc.Say( "Hear ye, hear ye!" );
		    tc.Say( _talkqueue.Dequeue() );
		}
	    }

	    //check again because we just dequeued
	    if( _talkqueue.Count > 0 ){
		m_TalkTimer = new InternalTimer();
		m_TalkTimer.Start();
	    }
	}
	    

	[Constructable]
	public TownCrier()
	{
	    _Criers.Add( this );

	    InitStats( 100, 100, 25 );

	    Title = "the town crier";
	    Hue = Utility.RandomSkinHue();

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

	    AddItem( new FancyShirt( Utility.RandomBlueHue() ) );

	    Item skirt;

	    switch ( Utility.Random( 2 ) )
	    {
		case 0: skirt = new Skirt(); break;
		default: case 1: skirt = new Kilt(); break;
	    }

	    skirt.Hue = Utility.RandomGreenHue();

	    AddItem( skirt );

	    AddItem( new FeatheredHat( Utility.RandomGreenHue() ) );

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
	    _Criers.Remove( this );
	    base.OnDelete();
	}

	public TownCrier( Serial serial ) : base( serial )
	{
	    _Criers.Add( this );
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

	    NameHue = 0x35;
	}
    }
}
