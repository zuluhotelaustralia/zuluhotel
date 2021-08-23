using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBFarmer : SBInfo
    {
        public override IShopSellInfo SellInfo { get; } = new InternalSellInfo();

        public override List<GenericBuyInfo> BuyInfo { get; } = new InternalBuyInfo();

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new GenericBuyInfo(typeof(CabbageSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(CarrotSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(CottonSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(FlaxSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(LettuceSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(OnionSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(PumpkinSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(TurnipSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(WheatSeed), 50, 5, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(GarlicSeed), 50, 10, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(MandrakeSeed), 50, 10, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(NightshadeSeed), 50, 10, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(GinsengSeed), 50, 10, 0xF7F, 0));
                Add(new GenericBuyInfo(typeof(Cantaloupe), 6, 20, 0xC79, 0));
                Add(new GenericBuyInfo(typeof(HoneydewMelon), 7, 20, 0xC74, 0));
                Add(new GenericBuyInfo(typeof(Squash), 3, 20, 0xC72, 0));
                Add(new GenericBuyInfo(typeof(GreenGourd), 3, 20, 0xC66, 0));
                Add(new GenericBuyInfo(typeof(YellowGourd), 3, 20, 0xC64, 0));
                //Add( new GenericBuyInfo( typeof( Turnip ), 6, 20, XXXXXX, 0 ) );
                Add(new GenericBuyInfo(typeof(Watermelon), 7, 20, 0xC5C, 0));
                //Add( new GenericBuyInfo( typeof( EarOfCorn ), 3, 20, XXXXXX, 0 ) );
                Add(new GenericBuyInfo(typeof(Eggs), 3, 20, 0x9B5, 0));
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Milk, 7, 20, 0x9AD, 0));
                Add(new GenericBuyInfo(typeof(Peach), 3, 20, 0x9D2, 0));
                Add(new GenericBuyInfo(typeof(Pear), 3, 20, 0x994, 0));
                Add(new GenericBuyInfo(typeof(Lemon), 3, 20, 0x1728, 0));
                Add(new GenericBuyInfo(typeof(Lime), 3, 20, 0x172A, 0));
                Add(new GenericBuyInfo(typeof(Grapes), 3, 20, 0x9D1, 0));
                Add(new GenericBuyInfo(typeof(Apple), 3, 20, 0x9D0, 0));
                Add(new GenericBuyInfo(typeof(SheafOfHay), 2, 20, 0xF36, 0));
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                Add(typeof(CabbageSeed), 1);
                Add(typeof(CarrotSeed), 1);
                Add(typeof(CottonSeed), 1);
                Add(typeof(FlaxSeed), 1);
                Add(typeof(LettuceSeed), 1);
                Add(typeof(OnionSeed), 1);
                Add(typeof(PumpkinSeed), 1);
                Add(typeof(TurnipSeed), 1);
                Add(typeof(WheatSeed), 1);
                Add(typeof(GarlicSeed), 1);
                Add(typeof(MandrakeSeed), 1);
                Add(typeof(NightshadeSeed), 1);
                Add(typeof(GinsengSeed), 1);
                Add(typeof(Pitcher), 5);
                Add(typeof(Eggs), 1);
                Add(typeof(Apple), 1);
                Add(typeof(Grapes), 1);
                Add(typeof(Watermelon), 3);
                Add(typeof(YellowGourd), 1);
                Add(typeof(GreenGourd), 1);
                Add(typeof(Pumpkin), 5);
                Add(typeof(Onion), 1);
                Add(typeof(Lettuce), 2);
                Add(typeof(Squash), 1);
                Add(typeof(Carrot), 1);
                Add(typeof(HoneydewMelon), 3);
                Add(typeof(Cantaloupe), 3);
                Add(typeof(Cabbage), 2);
                Add(typeof(Lemon), 1);
                Add(typeof(Lime), 1);
                Add(typeof(Peach), 1);
                Add(typeof(Pear), 1);
                Add(typeof(SheafOfHay), 1);
            }
        }
    }
}