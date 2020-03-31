using System;
using Server;
using Server.Gumps;

namespace Server.GumpTutorial
{
	public class BasicsA : Gump
	{
/*    This is the CTOR of this gump.    *
 *********/public BasicsA()/********** *
 ************************************** *
 * So to call this gump, you would use "from.SendGump( new BasicsA();"
 * It's just like every other CTOR, so I won't waste yours or my time on it :)

 */


/*  This is the base call. *
 ******/: base( 0, 0 )/*****
 ***************************
 * The base for a Gump takes in the starting X and Y coordinates.
 * These coordinates start at the top-left of the Field of View (FoV) in game.
 * So anything in this gump would start at '0,0' and go from there.
 */
		{
/*  These are the Attributes of this gump  *
 **********/Closable=true;   /**************
 **********/Disposable=false;/**************
 **********/Dragable=false;  /**************
 **********/Resizable=false; /**************
 *******************************************
 *Closable - True means the player can close it with a right click.  False....duh :)
 *Disposable - I don't exactly know what this one is, sorry.
 *Dragable - True means the player can drag the gump around the screen.  False....duh :)
 *Resizable - I don't know how to allow a player to resize a gump, so I can't elaborate on this one, sorry.
 */
			AddPage(0);

			/*             X   Y  Location of 'Background 2' (in game) */
			AddBackground( 0, 185, 100, 100, 9270 );
			AddLabel( 10, 195, 1153, "Background 2" );
			/* Since ': base( 0, 0 )' 'adds' nothing, this background will be shown at 0, 185
			*/
		}
	}

   	public class BasicsB : Gump
 	{
		/* You can also pass things into the CTOR, like a Mobile from, making it look like this */
		public BasicsB( Mobile from )
		/* This allows us to do things with the mobile that the gump recieves, like send a message (done in a sec)

		/* And change the location of where it 'starts' */
		: base( 285, 0 )
		/* This will make everything be offset 285 to the right (X) */
		{
			Closable = true;
			Disposable = true;
			Dragable = true; //Can drag this one
			Resizable = false;

			AddPage(0);

			/*             X  Y  location of 'Background 1' (in game) */
			AddBackground( 0, 0, 100, 100, 9270 );
			AddLabel( 10, 10, 0, "Background 1" );
			/* Notice how even though it's 0,0, it doesn't put the backgroud in the upper-left corner of the FoV
			 * This is because of the ' : base( 285, 0 )' above.  It 'adds' 285 to all X coordinates, and 0 to all Y's
			 */

			//Sending 'Mobile from' a message that the gump was opened.
			from.SendMessage( "Basics Gump Ran" );
		}
	}
}