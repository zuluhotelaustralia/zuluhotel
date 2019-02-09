using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Targeting;
using Server.Mobiles;

/*
There are a Gump, a Timer, and a utility Target class here.  The idea is that a separate system, perhaps with a control stone for easy staff access, will control when to send
macro test challenges to whom.  The actual call/query to the macrotest system should be made whenever someone swings an axe/fishing pole, etc.

  CheckSkill() <--+---> Control Stone --->-------> system object (this class) ->-+
                  |                                                              |
                  +----------<--------------<---------<------------<--------<----+
 */

namespace Server.Antimacro
{
    class AntimacroTarget : Target
    {
	private Mobile m_From;
	private Mobile m_Suspect;
	
	public AntimacroTarget( Mobile caster ) : base( 15, false, TargetFlags.None )
	{
	    m_From = caster;
	}

	protected override void OnTarget( Mobile from, object o )
	{
	    if ( o is PlayerMobile ) {
		m_Suspect = (Mobile)o;
		new AntimacroTransaction( m_Suspect );
	    }
	    else {
		m_From.SendMessage("Must target PlayerMobiles.");
	    }	
	}
    }

    class AntimacroTimer : Timer
    {
	private Mobile m_Suspect;
	private AntimacroTransaction m_Transaction;

	public AntimacroTimer( Mobile suspect, AntimacroTransaction txn ) : base( TimeSpan.FromMinutes( 5.0 ) )
	{
	    m_Suspect = suspect;
	    m_Transaction = txn;
	    Priority = TimerPriority.OneMinute;
	    Console.WriteLine("Antimacro timer created for character {0}", m_Suspect.Name);
	}

	protected override void OnTick()
	{
	    m_Transaction.TimeOutCallback();
	}
    }
    
    public class AntimacroGump : Gump
    {
	public static void Initialize()
	{
	    CommandSystem.Register( "macrotest", AccessLevel.Counselor, new CommandEventHandler( Macrotest_OnCommand ) );
	}

	[Usage( "macrotest" )]
	public static void Macrotest_OnCommand( CommandEventArgs e )
	{
	    e.Mobile.Target = new AntimacroTarget( e.Mobile );
	}
	
	public enum Buttons
	    {
		Exit,
		Page2Button,
		ReplyButton,
	    }

	private int num1;
	private int num2;
	private int m_AttemptsRemaining;
	private Mobile m_Suspect;
	private AntimacroTransaction m_Transaction;
	
	public AntimacroGump( Mobile suspect, int chances, AntimacroTransaction txn ) : base( 100, 100 )
	{
	    m_Suspect = suspect;
	    m_AttemptsRemaining = chances;
	    m_Transaction = txn;
	    
	    Closable = false;
	    Disposable = true;
	    Dragable = true;
	    Resizable = false;

	    AddPage(0);
	    AddBackground( 0, 0, 480, 320, 9300 );
	    AddHtml( 10, 10, 460, 100, "<h2>Unattended Resource Gathering Suspected</h2>", false, false );
	    AddHtml( 10, 50, 460, 140, "You appear to be gathering resources unattended.  This has harmful effects on the server's economy and so we ask that you please refrain from doing this.  You can confirm you are actually at the keyboard by performing the following simple arithmetic.  You have 5 minutes to reply.", false, false );
	    AddHtml( 10, 160, 460, 140, "You have " + m_AttemptsRemaining + " attempt(s) remaining.", false, false );

	    num1 = Utility.Dice(1, 10, 0);
	    num2 = Utility.Dice(1, 10, 0);
	    
	    AddHtml( 60, 200, 50, 50, "<b>" + num1 + "</b>", false, false );
	    AddHtml( 120, 200, 50, 50, "<b>+</b>", false, false );
	    AddHtml( 180, 200, 50, 50, "<b>" + num2 + "</b>", false, false );
	    AddHtml( 240, 200, 50, 50, "<b>=</b>", false, false );
	    AddImageTiled( 280, 200, 50, 22, 0 );
	    AddTextEntry( 290, 200, 50, 50, 49, 0, "" );
	    
	    AddButton(10, 280, 247, 248, (int)Buttons.ReplyButton, GumpButtonType.Reply, 2);
	    AddImageTiled( 10, 280, 68, 22, 2624); // Submit Button BG
	    AddLabel(20, 282, 49, @"Submit");
	}

	public override void OnResponse( NetState state, RelayInfo info )
	{
	    Mobile from = state.Mobile;

	    switch( info.ButtonID )
	    {
		case (int)Buttons.ReplyButton:
		    {
			int number; //temp
			int response; // their response to the gump
			int sum = num1 + num2; // the actual answer
	    
			string text = info.GetTextEntry(0).Text;
			bool success = Int32.TryParse(text, out response);
			if (!success) {
			    response = -1;
			}
			
			if( sum == response ) {
			    //they got it right, we can assume they're legit
			    m_Transaction.OKResponseCallback();
			    break;
			}

			//if we make it this far then sum != message i.e. they got it wrong
			
			from.SendMessage( "You replied: {0}, whereas the correct response was: {1}", response, sum );
			m_Transaction.BadResponseCallback();
			break;
		    }
	    }
	}
    }
}
