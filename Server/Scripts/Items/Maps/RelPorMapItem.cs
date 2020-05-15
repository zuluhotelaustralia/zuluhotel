using System;
using System.Collections.Generic;
using System.Text;
using Server.Gumps;

namespace Server.Items
{
    public class RelPorMapItem : MapItem
    {
        protected MapCutout mMapCutout;
        public MapCutout MyMapCutout { get { return mMapCutout; } set { mMapCutout = value; } }

        public RelPorMapItem(int index)
            : base()
        {
            if (index != -1)
            {
                mMapCutout = RelPorMapList.MapCutouts[index];
                Bounds = mMapCutout.Rect;
            }
            else
                mMapCutout = null;
        }
        public RelPorMapItem(Serial serial) : base(serial) { }
        public override void SetDisplay(int x1, int y1, int x2, int y2, int w, int h)
        {
            //We're overriding this to set the correct map cutout instead of new bounds
            //we take the center point of this display area and find the corresponding mapcutout
            int x = x1 + w / 2;
            int y = y1 + h / 2;
            int i = 0;
            foreach (MapCutout cutout in RelPorMapList.MapCutouts)
            {
                if (cutout.Rect.Contains(new Point2D(x, y)))
                {
                    break;
                }
                i++;
            }
            if (i < RelPorMapList.MapCutouts.Length)
            {
                mMapCutout = RelPorMapList.MapCutouts[i];
                Bounds = mMapCutout.Rect;
            }
        }
        //this is the brute force way, i'm too high to formulate it properly otherwise
        public override void SetDisplay(int x, int y)
        {
            int i = 0;
            foreach (MapCutout cutout in RelPorMapList.MapCutouts)
            {
                if (cutout.Rect.Contains(new Point2D(x, y)))
                {
                    break;
                }
                i++;
            }
            if (i < RelPorMapList.MapCutouts.Length)
            {
                mMapCutout = RelPorMapList.MapCutouts[i];
                Bounds = mMapCutout.Rect;
            }

        }
        public override void DisplayTo(Mobile from)
        {
            from.SendGump(new RelPorMapGump(from, mMapCutout, Pins));
        }

        public override void OnAddPin(Mobile from, int x, int y)
        {

        }
        public override void OnRemovePin(Mobile from, int number)
        {

        }
        public override void OnChangePin(Mobile from, int number, int x, int y)
        {

        }
        public override void OnClearPins(Mobile from)
        {

        }
        public override void OnToggleEditable(Mobile from)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            if (mMapCutout != null)
                writer.Write((int)mMapCutout.Index);
            else
                writer.Write((int)-1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            int index = reader.ReadInt();

            if (index != -1)
                mMapCutout = RelPorMapList.MapCutouts[index];
            else
                mMapCutout = null;
        }
    }
}
