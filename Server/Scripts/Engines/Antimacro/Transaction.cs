using System;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Mobiles;

/* 
   An "antimacro transaction" represents an event where in a player is sent a challenge from the system and either responds correctly, incorrectly, or not at all and then the system 
   decides what action to take against the player, if any
   --sith
   */

namespace Server.Antimacro
{
    public class AntimacroTransaction {
	private Mobile m_Subject;
	private Account m_SubjectAccount;
	private int m_AttemptsRemaining;
	private AntimacroTimer m_Timer;
	private static bool m_SystemEnabled = true;

	public static bool Enabled {
	    get { return m_SystemEnabled; }
	}
	
	public enum ResponseType {
	    BadMath,
	    TimeOut,
	    GoodMath
	}

	public static void Initialize()
	{
	    CommandSystem.Register( "ToggleAntiMacroSystem", AccessLevel.Counselor, new CommandEventHandler( ToggleAntiMacroSystem_OnCommand ) );
	}

	[Usage( "ToggleAntiMacroSystem" )]
	public static void ToggleAntiMacroSystem_OnCommand( CommandEventArgs e )
	{
	    if( m_SystemEnabled ) {
		m_SystemEnabled = false;
		e.Mobile.SendMessage("Anti-macro System has been DISABLED.");
	    }
	    else {
		m_SystemEnabled = true;
		e.Mobile.SendMessage("Anti-macro System has been ENABLED.");
	    }
	}

	public AntimacroTransaction( Mobile m ) {
	    m_Subject = m;
	    m_SubjectAccount = m_Subject.Account as Accounting.Account;
	    m_AttemptsRemaining = 3;
	    m_Timer = new AntimacroTimer( m_Subject, this );
	    SendChallenge();	    
	}

	private void Decide( ResponseType response ) {
	    switch( response ) {
		case ResponseType.BadMath:
		    m_Subject.SendMessage("You failed the test and will be disconnected.");
		    m_Timer.Stop();
		    MutateTrustScore( false );
		    if( m_Subject.NetState != null ){
			m_Subject.NetState.Dispose();
		    }
		    break;
		case ResponseType.TimeOut:
		    m_Subject.SendMessage("You failed to respond to the Anti-AFK Gathering test in time and will be disconnected.");
		    MutateTrustScore( false );
		    if( m_Subject.NetState != null ){
			m_Subject.NetState.Dispose(); //kick
		    }
		    break;
		case ResponseType.GoodMath:
		    m_Subject.SendMessage( "Thank you.  Your presence at the keyboard has been recorded and we apologize for the inconvenience." );
		    MutateTrustScore( true );
		    m_Timer.Stop();
		    break;
		default:
		    Console.WriteLine("WARNING:  THIS SHOULD NEVER HAPPEN (AntimacroTransaction)");
		    break;
	    }

	    SetNextDateTime();
	}

	public void SetNextDateTime() {
	    double score = m_SubjectAccount.TrustScore;
	    DateTime next = DateTime.UtcNow;
	    
	    if ( score > 0.85 ) {
		next = next.Add(new TimeSpan(7, 0, 0, 0) ); //test em next week
	    }

	    if ( score <= 0.85 && score > 0.5 ) {
		next = next.Add( new TimeSpan(2, 0, 0, 0) ); // test em in a few days
	    }

	    if ( score <= 0.5 && score > 0.3 ) {
		next = next.Add( new TimeSpan(8, 0, 0) ); // test em in 8hrs
	    }

	    if ( score <= 0.3 ) {
		next = next.Add( new TimeSpan(1, 0, 0) ); // test em basically as soon as they reconnect and start gathering again.
	    }

	    m_SubjectAccount.NextTransaction = next;
	    m_Subject.BeingMacroTested = false;
	}
	
	public void MutateTrustScore( bool good ) {
	    double current = m_SubjectAccount.TrustScore;
	    if ( good ) {
		if( m_SubjectAccount.TrustScore <= 0.9 ){
		    m_SubjectAccount.TrustScore += 0.05; //i dunno
		}
	    }
	    else{
		if( m_SubjectAccount.TrustScore >= 0.01 ){
		    m_SubjectAccount.TrustScore = current * 0.5;
		}
	    }
	}

	public void OKResponseCallback() {
	    m_Subject.BeingMacroTested = false;
	    Decide( ResponseType.GoodMath );
	}

	public void BadResponseCallback() {
	    // decrement and keep track of attempts remaining
	    if ( m_AttemptsRemaining > 1 ) {
		m_AttemptsRemaining--;
		SendChallenge();
	    }
	    else {
		m_Subject.BeingMacroTested = false;
		//they've had 3 strikes and are now out
		Decide( ResponseType.BadMath );
	    }
	}

	public void TimeOutCallback() {
	    m_Subject.BeingMacroTested = false;
	    Decide( ResponseType.TimeOut );
	}
	
	public void SendChallenge() {
	    //entrypoint, sorta

	    if( m_Subject is PlayerMobile && !m_Subject.BeingMacroTested ){
		m_Subject.SendGump( new AntimacroGump( m_Subject, m_AttemptsRemaining, this));
		m_Subject.BeingMacroTested = true;
		m_Timer.Start();
	    }
	}
    }
}
