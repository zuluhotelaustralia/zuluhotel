using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
    public class SBTailor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTailor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( SewingKit ), 3, 20, 0xF9D, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Scissors ), 11, 20, 0xF9F, 0 ) );
				Add( new GenericBuyInfo( typeof( DyeTub ), 8, 20, 0xFAB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Dyes ), 8, 20, 0xFA9, 0 ) ); 

				Add( new GenericBuyInfo( typeof( Shirt ), 12, 20, 0x1517, 0 ) );
				Add( new GenericBuyInfo( typeof( ShortPants ), 7, 20, 0x152E, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyShirt ), 21, 20, 0x1EFD, 0 ) );
				Add( new GenericBuyInfo( typeof( LongPants ), 10, 20, 0x1539, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyDress ), 26, 20, 0x1EFF, 0 ) );
				Add( new GenericBuyInfo( typeof( PlainDress ), 13, 20, 0x1F01, 0 ) );
				Add( new GenericBuyInfo( typeof( Kilt ), 11, 20, 0x1537, 0 ) );
				Add( new GenericBuyInfo( typeof( Kilt ), 11, 20, 0x1537, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( HalfApron ), 10, 20, 0x153b, 0 ) );
				Add( new GenericBuyInfo( typeof( Robe ), 18, 20, 0x1F03, 0 ) );
				Add( new GenericBuyInfo( typeof( Cloak ), 8, 20, 0x1515, 0 ) );
				Add( new GenericBuyInfo( typeof( Cloak ), 8, 20, 0x1515, 0 ) );
				Add( new GenericBuyInfo( typeof( Doublet ), 13, 20, 0x1F7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Tunic ), 18, 20, 0x1FA1, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterSuit ), 26, 20, 0x1F9F, 0 ) );

				Add( new GenericBuyInfo( typeof( JesterHat ), 12, 20, 0x171C, 0 ) );
				Add( new GenericBuyInfo( typeof( FloppyHat ), 7, 20, 0x1713, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WideBrimHat ), 8, 20, 0x1714, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Cap ), 10, 20, 0x1715, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( TallStrawHat ), 8, 20, 0x1716, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( StrawHat ), 7, 20, 0x1717, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( WizardsHat ), 11, 20, 0x1718, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( LeatherCap ), 10, 20, 0x1DB9, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( FeatheredHat ), 10, 20, 0x171A, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( TricorneHat ), 8, 20, 0x171B, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( Bandana ), 6, 20, 0x1540, Utility.RandomDyedHue() ) );
				Add( new GenericBuyInfo( typeof( SkullCap ), 7, 20, 0x1544, Utility.RandomDyedHue() ) );

				Add( new GenericBuyInfo( typeof( BoltOfCloth ), 100, 20, 0xf95, Utility.RandomDyedHue() ) ); 

				Add( new GenericBuyInfo( typeof( Cloth ), 2, 20, 0x1766, Utility.RandomDyedHue() ) ); 
				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, 20, 0x1767, Utility.RandomDyedHue() ) ); 

				Add( new GenericBuyInfo( typeof( Cotton ), 102, 20, 0xDF9, 0 ) );
				Add( new GenericBuyInfo( typeof( Wool ), 62, 20, 0xDF8, 0 ) );
				Add( new GenericBuyInfo( typeof( Flax ), 102, 20, 0x1A9C, 0 ) );
				Add( new GenericBuyInfo( typeof( SpoolOfThread ), 18, 20, 0xFA0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Scissors ), 2 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Dyes ), 2 );
				Add( typeof( DyeTub ), 2 );

				Add( typeof( BoltOfCloth ), 1 );

				Add( typeof( FancyShirt ), 2 );
				Add( typeof( Shirt ), 2 );

				Add( typeof( ShortPants ), 2 );
				Add( typeof( LongPants ), 2 );

				Add( typeof( Cloak ), 2 );
				Add( typeof( FancyDress ), 2 );
				Add( typeof( Robe ), 2 );
				Add( typeof( PlainDress ), 2 );

				Add( typeof( Skirt ), 2 );
				Add( typeof( Kilt ), 2 );

				Add( typeof( Doublet ), 2 );
				Add( typeof( Tunic ), 2 );
				Add( typeof( JesterSuit ), 2 );

				Add( typeof( FullApron ), 2 );
				Add( typeof( HalfApron ), 2 );

				Add( typeof( JesterHat ), 2 );
				Add( typeof( FloppyHat ), 2 );
				Add( typeof( WideBrimHat ), 2 );
				Add( typeof( Cap ), 2 );
				Add( typeof( SkullCap ), 2 );
				Add( typeof( Bandana ), 2 );
				Add( typeof( TallStrawHat ), 2 );
				Add( typeof( StrawHat ), 2 );
				Add( typeof( WizardsHat ), 2 );
				Add( typeof( Bonnet ), 2 );
				Add( typeof( FeatheredHat ), 2 );
				Add( typeof( TricorneHat ), 2 );

				Add( typeof( SpoolOfThread ), 1 );

				Add( typeof( Flax ), 1 );
				Add( typeof( Cotton ), 1 );
				Add( typeof( Wool ), 1 );
			}
		}
	}
}
