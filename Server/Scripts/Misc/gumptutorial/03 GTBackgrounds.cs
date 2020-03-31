using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTBackgrounds : Gump
	{
		public GTBackgrounds()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 285, 210, 9270);
			AddButton(12, 10, 1304, 1305, 0, GumpButtonType.Reply, 0);
			AddButton(60, 170, 2440, 2446, 1, GumpButtonType.Reply, 0);
			AddLabel(102, 16, 1153, "Backgrounds");
			AddLabel(80, 172, 1153, "Run Backgrounds.cs");
			AddHtml( 10, 50, 265, 115, "There is really not much information about Backgrounds.  So I will demonstrate the code for it, and give examples of the different ones.  Check Backgrounds.cs for the code.", true, false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( Backgrounds ) );
					break;
				}
				case 1:
				{
					from.SendGump( new Backgrounds() );
					from.SendGump( new GTBackgrounds() );
					break;
				}
			}
		}
	}
}