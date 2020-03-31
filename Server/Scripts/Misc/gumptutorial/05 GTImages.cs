using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.GumpTutorial
{
	public class GTImages : Gump
	{
		public GTImages()
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
			AddLabel(99, 16, 1153, "Images / Tiled Images");
			AddLabel(98, 172, 1153, "Run Images.cs");
			AddHtml( 10, 50, 265, 115, "There are two types of Images.  Image and Tiled Image.  An image is just a single GumpID.  A tiled image an image that you can tile.  (Duh :p)  You can set the size of the space to tile, and the image will tile itself in that space.  If the space is bigger than the original demensions of the GumpID, it will repeat untill the space is filled.  Open Images.cs for comments/code.", true, true);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
					from.SendGump( new GTHome() );
					from.CloseGump( typeof( Images ) );
					break;
				}
				case 1:
				{
					from.SendGump( new Images() );
					from.SendGump( new GTImages() );
					break;
				}
			}
		}
	}
}