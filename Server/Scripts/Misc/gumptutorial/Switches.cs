using System;
using Server;
using Server.Gumps;
/***********************/
using Server.Network;/**/
/***********************/
/* This is needed for the OnResponse method. */

namespace Server.GumpTutorial
{
	public class Switches : Gump
	{
		public Switches() : base( 0, 185 )
		{
			AddPage(0);

			AddBackground(0, 0, 100, 100, 9270);
			AddLabel(25, 5, 1153, "Radios");
/*          AddButton( X, Y, UnCheckedGumpID, CheckedGumpID, StartChecked?, SwitchID ); */
			AddRadio(15, 30, 208, 209, false, 1);
			AddRadio(60, 30, 208, 209, false, 2);
			AddRadio(15, 60, 208, 209, false, 3);
			AddRadio(60, 60, 208, 209, false, 4);
/* Out of the four Radio buttons above, you can only select one.
 * When you select one, the other will un-select (if it was selected) */
 			AddLabel( 20, 30, 1153, "1" );
 			AddLabel( 65, 30, 1153, "2" );
 			AddLabel( 20, 60, 1153, "3" );
 			AddLabel( 65, 60, 1153, "4" );

			AddBackground(100, 0, 100, 100, 9270);
			AddLabel(125, 5, 1153, "Checks");
/*			AddCheck( X, Y, UnCheckedGumpID, CheckedGumpID, StartChecked?, SwitchID ); */
			AddCheck( 115, 30, 208, 209, false, 10 );
			AddCheck( 160, 30, 208, 209, false, 11 );
			AddCheck( 115, 60, 208, 209, false, 12 );
			AddCheck( 160, 60, 208, 209, false, 13 );
/* Out of the four Check buttons above, you can select as many as you want. */
 			AddLabel( 120, 30, 1153, "1" );
 			AddLabel( 165, 30, 1153, "2" );
 			AddLabel( 120, 60, 1153, "3" );
 			AddLabel( 165, 60, 1153, "4" );

			AddBackground(0, 100, 100, 100, 9270);
			AddLabel(30, 105, 1153, "Reply");
			AddButton(17, 140, 247, 248, 20, GumpButtonType.Reply, 0);
		}

/* The main thing from NetState state you will want is state.Mobile.  This is the Mobile that clicked the button.
 * RelayInfo contains information from the gump like ButtonID, Switches and TextEntries.  We will focus on Switches here.*/
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			if( info.ButtonID == 20 )
			{
				string toSay = "";
/* info.Switches is an int array (int[]).
 * One way I've found to check the switches is to loop through the array
 * and do a switch/case for the SwitchIDs.
 * info.Switches will only contain the ID's if they are checked.
 */
				for( int i = 0; i < info.Switches.Length; i++ )
				{
					int m = info.Switches[i];
					switch( m )
					{
						case 1: toSay += "Radio 1 : "; break;
						case 2: toSay += "Radio 2 : "; break;
						case 3: toSay += "Radio 3 : "; break;
						case 4: toSay += "Radio 4 : "; break;
						case 10: toSay += "Check 1, "; break;
						case 11: toSay += "Check 2, "; break;
						case 12: toSay += "Check 3, "; break;
						case 13: toSay += "Check 4, "; break;
					}
				}
				from.SendMessage( toSay );

/* Another way of doing it is by doing info.IsSwitched( SwitchID ).
 * This call will return a true/false depending on if the switch is checked.
 */
				toSay = "";
				if( info.IsSwitched( 1 ) )
					toSay += "Radio 1 : ";
				else if( info.IsSwitched( 2 ) )
					toSay += "Radio 2 : ";
				else if( info.IsSwitched( 3 ) )
					toSay += "Radio 3 : ";
				else if( info.IsSwitched( 4 ) )
					toSay += "Radio 4 : ";

				toSay += String.Format( "Check 1={0}, 2={1}, 3={2}, 4={3}", info.IsSwitched( 10 ), info.IsSwitched( 11 ), info.IsSwitched( 12 ), info.IsSwitched( 13 ) );
				from.SendMessage( toSay );
			}
		}
	}
}