using System;
using Server;
using Server.Gumps;
/***********************/
using Server.Network;/**/
/***********************/
/* This is needed for the OnResponse method. */

namespace Server.GumpTutorial
{
	public class Buttons : Gump
	{
		public Buttons()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;

/* Up untill now, these AddPage(0)'s have not been looked at */
/* So I will describe them now...
 * Page 0 is ALWAYS show in a gump.  This means that if you change pages with a page button,
 * the stuff that is on Page 0 is shown as well. */
			AddPage(0);
/* That means these two background will show all the time, or untill the gump is closed */
			AddBackground(0, 185, 100, 100, 9270);
			AddBackground(100, 185, 100, 100, 9270);

/* Page 1 is the first page to be displayed. (On top of 0)
 * If there is no Page 1, you will only see Page 0, with nothing else. */
			AddPage(1);
			AddLabel(28, 200, 0, "Page 1");
/*          AddButton( X, Y, UnpressedGumpID, PressedGumpID, ButtonID, GumpButtonType, Page ); */
			AddButton(18, 235, 247, 248, (int)Buttonss.Page2Button, GumpButtonType.Page, 2);
/* You can see here that I am using this |^^^^^^^^^^^^^^^^^^^^^^^| form of ButtonID.
 * The enum Buttonss keeps track of what ButtonID each button has, and it's easier to keep track of which buttons are which.(IMO)
 * You can also simply put an int there, like 2, 4, 92, etc.
 * ButtonID 0 is the Exit button.  This means if you right click on the gump to close, you are 'pressing' a button with ButtonID=0. /*
/* In a Page button, (to my knowledge) the ButtonID does not matter. */
/* The last number in the call is which page to open.  In this case, it is page 2, so when the button is pushed
 * the gump jumps to where you see AddPage(2) and runs the following code. */

/* This is where Page 2 code starts */
			AddPage(2);
/* Added a new background, as you can see in game */
			AddBackground(1, 285, 100, 100, 9270);

/* Also added a Page button to go back to Page 1.
 * When this button is pressed, the Gump will show Page 1 again.
 * Notice I used a 3 for the ButtonID.  You can use any int you want for this.
 * Notice how the other stuff is gone that showed up when Page 2 was opened */
			AddButton(18, 235, 247, 248, 3, GumpButtonType.Page, 1);
			AddLabel(28, 300, 0, "Page 2");

/* This is a Reply Button.  It has the same call as the Page button, but it uses the GumpButtonType.Reply.
 * In the Reply buttons, the ButtonID matters.  You will use this ID to check which button was pressed in OnResponse.
 * In Reply buttons, the Page (Last number in the call) does not matter.  (That I know of)
 * When a Reply button is pressed, the gump closes and runs the OnResponse method.
 * That method checks to see what each ButtonID is assigned to do. (More on this below) */
			AddButton(118, 235, 247, 248, (int)Buttonss.ReplyButton, GumpButtonType.Reply, 0);
			AddLabel(132, 200, 0, "Reply");
		}

/* This is the enum of the Buttonss. */
/* Gump Studio uses this form when creating the scripts of the gumps.
 * I find it (in most cases) easier to keep track of the buttons by using this method
 * so I can give each button a name. */
/* One thing GumpStudio does not do is add in the 'Exit' button.
 * The first button in this list below is going to have an int value of 0, being the Exit button.
 * GumpStudio does not take that into account, so I just add in a new entry of 'Exit' */
		public enum Buttonss
		{
			Exit,
			Page2Button,
			ReplyButton,
		}


/* This is the OnResponse method.
 * It is called when a Reply type button is pressed.
 * It does a switch to check what button was pressed, and what it does. (Or however you wish to check which button does what)
 * If it finds the button that was pressed, it runs the code */
/* The main thing from NetState state you will want is state.Mobile.  This is the Mobile that clicked the button.
 * RelayInfo contains information from the gump like ButtonID, Switches and TextEntries.  We will focus on ButtonID here.*/
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
/* This example uses a switch statement. */
			switch( info.ButtonID )
			{
				case (int)Buttonss.Exit:
				{
					from.SendMessage( "Button 0 was pressed" );
					break;
				}
				case (int)Buttonss.ReplyButton:
				{
					from.SendMessage( "Reply button was pressed, Gump closed!" );
					break;
				}
				case 3:
				{
					from.SendMessage( "Page 1 Opening" );
					break;
				}
			}
/* You can see that even though I defined the '3' button, it doesn't do anything.
 * That is because pages, do not get definitions here. */

/* You can also use if/else if/else tests: */
     		if( info.ButtonID == 2 )
     			from.SendMessage( "If statement Reply" );
		}
	}
}