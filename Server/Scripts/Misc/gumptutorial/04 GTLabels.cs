using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTLabels : Gump
	{
		public GTLabels()
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
			AddLabel(121, 16, 1153, "Labels");
			AddLabel(100, 140, 1153, "Run Labels.cs");
			AddHtml( 10, 50, 265, 82, "Labels are just about as complicated as Backgrounds.  As such, I will give only a couple examples of what you can do with them.  Check Labels.cs for comments/code.", true, true);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( Labels ) );
					break;
				}
				case 1:
				{
					from.SendGump( new GTLabels() );
					from.SendGump( new Labels( from ) );
					break;
				}
			}
		}
	}
}