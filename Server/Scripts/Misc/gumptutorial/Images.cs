using System;
using Server;
using Server.Gumps;

namespace Server.GumpTutorial
{
	public class Images : Gump
	{
		public Images()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);

/* AddImage( X, Y, GumpID ); */
/* Shows a single GumpID */
			AddImage(285, 0, 1211);
			AddLabel(325, 10, 1153, "Single Image");

/* AddImageTiled( X, Y, SizeX, SizeY, GumpID );
/* Shows multiple 'copies' of a GumpID */
/* Will cut the picture if it is bigger than the tiled size */
			AddImageTiled(0, 235, 111, 117, 1211);
			AddLabel(10, 212, 1153, "Tiled Image (Cut)");
/* Just an example that if will tile both ways, right/left and up/down */
			AddImageTiled(135, 235, 142, 271, 1211);
			AddLabel(145, 212, 1153, "Tiled Image (Full)");

		}


	}
}