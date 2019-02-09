using System;
using Server;
using Server.Accounting;
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

	public enum ResponseType {
	    BadMath,
	    TimeOut,
	    GoodMath
	}

	public AntimacroTransaction( Mobile m ) {
	    m_Subject = m;
	    m_SubjectAccount = m_Subject.Account as Accounting.Account;
	    m_AttemptsRemaining = 3;
	    m_Timer = new AntimacroTimer( m_Subject, this );
	}

	private void Decide( ResponseType response ) {
	    switch( response ) {
		case ResponseType.BadMath:
		    m_Subject.SendMessage("You failed the test and will be disconnected.");
		    m_Timer.Stop();
		    m_Subject.NetState.Dispose();
		    break;
		case ResponseType.TimeOut:
		    m_Subject.SendMessage("You failed to respond to the Anti-AFK Gathering test in time and will be disconnected.");
		    m_Subject.NetState.Dispose(); //kick
		    break;
		case ResponseType.GoodMath:
		    m_Subject.SendMessage( "Thank you.  Your presence at the keyboard has been recorded and we apologize for the inconvenience." );
		    //adjust trust score here
		    m_Timer.Stop();
		    break;
		default:
		    Console.WriteLine("WARNING:  THIS SHOULD NEVER HAPPEN (AntimacroTransaction)");
		    break;
	    }	    
	}

	public void OKResponseCallback() {
	    Decide( ResponseType.GoodMath );
	}

	public void BadResponseCallback() {
	    // decrement and keep track of attempts remaining
	    if ( m_AttemptsRemaining > 1 ) {
		m_AttemptsRemaining--;
		SendChallenge();
	    }
	    else {
		//they've had 3 strikes and are now out
		Decide( ResponseType.BadMath );
	    }
	}

	public void TimeOutCallback() {
	    Decide( ResponseType.TimeOut );
	}
	
	public void SendChallenge() {
	    //entrypoint
	    m_Subject.SendGump( new AntimacroGump( m_Subject, m_AttemptsRemaining, this));
	    m_Timer.Start();
	}
    }
}
