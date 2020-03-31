using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class Alphas : Gump
	{
		public Alphas()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);

/* AddAlphaRegion( X, Y, SizeX, SizeY ); */
/* Notice the order of adding these two. */
/* The first adds the alpha region 'over' (after) the background.
 * And 'under' (before) the label/button */
			AddBackground(0, 185, 100, 100, 9270);
			AddAlphaRegion(10, 195, 80, 80);
			AddLabel(12, 200, 1153, "Alpha Under");
			AddButton(18, 235, 247, 248, 1, GumpButtonType.Reply, 0);
/* This one adds the alpha region 'over' (after) both the background AND the label/button.
 * See the difference in game? */
			AddBackground(100, 185, 100, 100, 9270);
			AddLabel(115, 200, 1153, "Alpha Over");
			AddButton(118, 235, 247, 248, 1, GumpButtonType.Reply, 0);
			AddAlphaRegion(110, 195, 80, 80);
/* Notice you can still press the button with the alpha region 'over' it. */
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 1:
				{
					from.SendMessage( "Button pressed" );
					from.SendGump( new Alphas() );
					break;
				}
			}
		}
	}
}