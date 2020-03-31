using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTAlphas : Gump
	{
		public GTAlphas()
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
			AddLabel(98, 16, 1153, "Alpha Regions");
			AddLabel(99, 140, 1153, "Run Alphas.cs");
			AddHtml( 10, 50, 265, 82, "Alpha Regions give a gump transparency.  It allows the user to see things that are 'under' the gump.  Anything of the current gump that is 'under' the alpha region will show transparent, so be sure to put it in the right place in the script!  Open Alphas.cs for comments/code.", true, true);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( Alphas ) );
					break;
				}
				case 1:
				{
					from.SendGump( new GTAlphas() );
					from.SendGump( new Alphas() );
					break;
				}
			}
		}
	}
}