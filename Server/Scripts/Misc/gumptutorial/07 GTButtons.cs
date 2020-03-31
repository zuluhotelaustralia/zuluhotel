using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTButtons : Gump
	{
		public GTButtons()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 285, 185, 9270);
			AddButton(12, 10, 1304, 1305, 0, GumpButtonType.Reply, 0);
			AddButton(60, 138, 2440, 2446, 1, GumpButtonType.Reply, 0);
			AddLabel(116, 16, 1153, "Buttons");
			AddLabel(94, 140, 1153, "Run Buttons.cs");
			AddHtml( 10, 50, 265, 82, "There are two types of buttons.  Page and Reply.  Page buttons 'turn the page' of the gump.  Reply buttons close the gump and perform tasks.  You can simulate a 'page turner' with a reply button by opening a new gump when the button is pressed.  Please open Buttons.cs to see code/comments.", true, true);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( Buttons ) );
					break;
				}
				case 1:
				{
					from.SendGump( new GTButtons() );
					from.SendGump( new Buttons() );
					break;
				}
			}
		}
	}
}