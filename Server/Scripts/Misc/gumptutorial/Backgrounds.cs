using System;
using Server;
using Server.Gumps;

namespace Server.GumpTutorial
{
	public class Backgrounds : Gump
	{
		public Backgrounds()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=false;
			Resizable=false;
			AddPage(0);
/* AddBackground( X, Y, SizeX, SizeY, GumpID ); */
/* GumpID is the start of 9 consequtive ID's that form the Background */
/* Look in Gump Studio and see how it looks */
			AddBackground(300, 10, 100, 100, 83);
			AddLabel(339, 52, 0, "83");
			AddBackground(400, 10, 100, 100, 2600);
			AddLabel(431, 52, 0, "2600");
			AddBackground(300, 110, 100, 100, 2620);
			AddLabel(331, 152, 1153, "2620");
			AddBackground(400, 110, 100, 100, 3000);
			AddLabel(431, 152, 0, "3000");
			AddBackground(0, 210, 100, 100, 3500);
			AddLabel(31, 252, 0, "3500");
			AddBackground(100, 210, 100, 100, 3600);
			AddLabel(131, 252, 1153, "3600");
			AddBackground(200, 210, 100, 100, 5054);
			AddLabel(231, 252, 0, "5054");
			AddBackground(300, 210, 100, 100, 5100);
			AddLabel(331, 252, 0, "5100");
			AddBackground(400, 210, 100, 100, 5120);
			AddLabel(431, 252, 1153, "5120");
			AddBackground(0, 310, 100, 100, 5150);
			AddLabel(31, 352, 0, "5150");
			AddBackground(100, 310, 100, 100, 5170);
			AddLabel(131, 352, 0, "5170");
			AddBackground(200, 310, 100, 100, 9200);
			AddLabel(231, 352, 0, "9200");
			AddBackground(300, 310, 100, 100, 9250);
			AddLabel(331, 352, 0, "9250");
			AddBackground(400, 310, 100, 100, 9260);
			AddLabel(431, 352, 0, "9260");
			AddBackground(0, 410, 100, 100, 9270);
			AddLabel(31, 452, 1153, "9270");
			AddBackground(100, 410, 100, 100, 9300);
			AddLabel(131, 452, 0, "9300");
			AddBackground(200, 410, 100, 100, 9350);
			AddLabel(231, 452, 0, "9350");
		}
	}
}