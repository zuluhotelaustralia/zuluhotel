using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBCarpenter : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBCarpenter()
        {
        }

        public override IShopSellInfo SellInfo
        {
            get { return m_SellInfo; }
        }

        public override List<GenericBuyInfo> BuyInfo
        {
            get { return m_BuyInfo; }
        }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(Nails), 3, 20, 0x102E, 0));
                Add(new GenericBuyInfo(typeof(Axle), 2, 20, 0x105B, 0));
                Add(new GenericBuyInfo(typeof(DrawKnife), 10, 20, 0x10E4, 0));
                Add(new GenericBuyInfo(typeof(Froe), 10, 20, 0x10E5, 0));
                Add(new GenericBuyInfo(typeof(Scorp), 10, 20, 0x10E7, 0));
                Add(new GenericBuyInfo(typeof(Inshave), 10, 20, 0x10E6, 0));
                Add(new GenericBuyInfo(typeof(DovetailSaw), 12, 20, 0x1028, 0));
                Add(new GenericBuyInfo(typeof(Saw), 15, 20, 0x1034, 0));
                Add(new GenericBuyInfo(typeof(Hammer), 17, 20, 0x102A, 0));
                Add(new GenericBuyInfo(typeof(MouldingPlane), 11, 20, 0x102C, 0));
                Add(new GenericBuyInfo(typeof(SmoothingPlane), 10, 20, 0x1032, 0));
                Add(new GenericBuyInfo(typeof(JointingPlane), 11, 20, 0x1030, 0));
                Add(new GenericBuyInfo(typeof(Drums), 21, 20, 0xE9C, 0));
                Add(new GenericBuyInfo(typeof(Tambourine), 21, 20, 0xE9D, 0));
                Add(new GenericBuyInfo(typeof(LapHarp), 21, 20, 0xEB2, 0));
                Add(new GenericBuyInfo(typeof(Lute), 21, 20, 0xEB3, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(WoodenBox), 2);
                Add(typeof(SmallCrate), 2);
                Add(typeof(MediumCrate), 2);
                Add(typeof(LargeCrate), 2);
                Add(typeof(WoodenChest), 2);

                Add(typeof(LargeTable), 2);
                Add(typeof(Nightstand), 2);
                Add(typeof(YewWoodTable), 2);

                Add(typeof(Throne), 2);
                Add(typeof(WoodenThrone), 2);
                Add(typeof(Stool), 2);
                Add(typeof(FootStool), 2);

                Add(typeof(FancyWoodenChairCushion), 2);
                Add(typeof(WoodenChairCushion), 2);
                Add(typeof(WoodenChair), 2);
                Add(typeof(BambooChair), 2);
                Add(typeof(WoodenBench), 2);

                Add(typeof(Saw), 2);
                Add(typeof(Scorp), 2);
                Add(typeof(SmoothingPlane), 2);
                Add(typeof(DrawKnife), 2);
                Add(typeof(Froe), 2);
                Add(typeof(Hammer), 2);
                Add(typeof(Inshave), 2);
                Add(typeof(JointingPlane), 2);
                Add(typeof(MouldingPlane), 2);
                Add(typeof(DovetailSaw), 2);
                Add(typeof(Axle), 2);

                Add(typeof(Club), 2);

                Add(typeof(Lute), 2);
                Add(typeof(LapHarp), 2);
                Add(typeof(Tambourine), 2);
                Add(typeof(Drums), 2);

                Add(typeof(Log), 1);
            }
        }
    }
}