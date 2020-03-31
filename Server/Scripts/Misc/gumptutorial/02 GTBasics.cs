using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTBasics : Gump
	{
		public GTBasics()
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
			AddLabel(89, 16, 1153, "Basics of a Gump");
			AddLabel(98, 140, 1153, "Run Basics.cs");
			AddHtml( 10, 50, 265, 82, "This tutorial will explain some of the basic parts of most gumps.  All information pertaining to this part is located in 'Basics.cs'", true, false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( BasicsA ) );
					from.CloseGump( typeof( BasicsB ) );
					break;
				}
				case 1:
				{
					from.SendGump( new GTBasics() );
					from.SendGump( new BasicsA() );
					from.SendGump( new BasicsB( from ) );
					break;
				}
			}
		}
	}
}