using System;
using Server;
using Server.Gumps;

namespace Server.GumpTutorial
{
	public class Labels : Gump
	{
		public Labels( Mobile from )
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 185, 285, 100, 9270);

/* AddLabel( X, Y, Hue, string Message ); */
			AddLabel(20, 210, 0, "I am a plain label");
/* You can change the hue of the label by changing the Hue to any hue number. */
							/* */
			AddLabel(20, 225, 2, "I am a hued label" );
/* You can pass in other strings into the Message part......|           | */
			AddLabel(20, 240, 1153, "I was sent the name : " + from.Name );
		}
	}
}