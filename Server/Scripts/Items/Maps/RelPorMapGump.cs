using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Server.Gumps
{
    public class RelPorMapGump : Gump
    {
        Mobile caller;
        MapCutout mapCutout;

        public RelPorMapGump(Mobile from, MapCutout cutout, List<Point2D> pinLocations)
            : this()
        {
            caller = from;
            mapCutout = cutout;
            AddPage(0);
            AddBackground(0, 0, 250, 260, 9380);

            if (mapCutout != null)
                AddImage(25, 30, mapCutout.GumpID);
            else
                from.CloseGump(typeof(RelPorMapGump));

            foreach (Point2D pin in pinLocations)
            {
                AddImage(pin.X + 25, pin.Y + 13, 0x2331);
            }

        }

        public RelPorMapGump()
            : base(100, 100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
        }
    }
}
