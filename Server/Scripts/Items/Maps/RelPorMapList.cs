using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class RelPorMapList
    {
        public static readonly Point2D mOffset = new Point2D(400, 500);
        private static MapCutout[] mMapCutouts;
        public static MapCutout[] MapCutouts
        {
            get
            {
                if (mMapCutouts == null)
                    Init();

                return mMapCutouts;
            }

        }

        private static MapCutout[] mCityMapCutouts;
        public static MapCutout[] CityMapCutouts
        {
            get
            {
                if (mCityMapCutouts == null)
                    Init();

                return mCityMapCutouts;
            }

        }

        private static MapCutout mWorldMapCutout;
        public static MapCutout WorldMapCutout
        {
            get { return mWorldMapCutout; }
        }
        public static void Init()
        {
            mMapCutouts = new MapCutout[64];
            int startGumpID = 0x1200;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Point2D start = new Point2D(mOffset.X + j * 200, mOffset.Y + i * 200);
                    mMapCutouts[j + i * 8] = new MapCutout(startGumpID + (j + i * 8), j + i * 8, new Rectangle2D(start.X, start.Y, 200, 200));

                }
            }

            mCityMapCutouts = new MapCutout[9];

            mCityMapCutouts[0] = new MapCutout(4672, 0, new Rectangle2D(761, 772, 200, 200)); //pedran
            mCityMapCutouts[1] = new MapCutout(4673, 1, new Rectangle2D(516, 1263, 200, 200)); //lillano
            mCityMapCutouts[2] = new MapCutout(4674, 2, new Rectangle2D(700, 1700, 200, 200)); //bowan
            mCityMapCutouts[3] = new MapCutout(4675, 3, new Rectangle2D(900, 1800, 200, 200)); //calor
            mCityMapCutouts[4] = new MapCutout(4676, 4, new Rectangle2D(1300, 1829, 200, 200)); //roache
            mCityMapCutouts[5] = new MapCutout(4677, 5, new Rectangle2D(1600, 1533, 200, 200)); //vermell
            mCityMapCutouts[6] = new MapCutout(4678, 6, new Rectangle2D(1700, 949, 200, 200)); //arbor
            mCityMapCutouts[7] = new MapCutout(4679, 7, new Rectangle2D(1100, 772, 200, 200)); //albus
            mCityMapCutouts[8] = new MapCutout(4680, 8, new Rectangle2D(1200, 1300, 200, 200)); //galven

            mWorldMapCutout = new MapCutout(4681, 0, new Rectangle2D(0, 0, 0, 0));
        }
    }
    public class MapCutout
    {
        private int mIndex;
        private int mGumpID;
        private Rectangle2D mRect;

        public int GumpID { get { return mGumpID; } set { mGumpID = value; } }
        public Rectangle2D Rect { get { return mRect; } set { mRect = value; } }
        public int Index { get { return mIndex; } set { mIndex = value; } }

        public MapCutout(int gumpID, int index, Rectangle2D rect)
        {
            mGumpID = gumpID;
            mIndex = index;
            mRect = rect;
        }
    }
}
