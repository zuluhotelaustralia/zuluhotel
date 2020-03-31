using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTSwitches : Gump
	{
		public GTSwitches()
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
			AddLabel(116, 16, 1153, "Switches");
			AddLabel(94, 140, 1153, "Run Switches.cs");
			AddHtml( 10, 50, 265, 82, "There are two types of Switches.  Radio and Check.  You can check as many Check buttons as you want, but you can only select ONE Radio button.  Please open Switches.cs to see code/comments.", true, true);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( Switches ) );
					break;
				}
				case 1:
				{
					from.SendGump( new GTSwitches() );
					from.SendGump( new Switches() );
					break;
				}
			}
		}
	}
}