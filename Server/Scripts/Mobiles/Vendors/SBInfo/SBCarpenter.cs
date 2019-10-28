using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBCarpenter: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBCarpenter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Nails ), 3, 20, 0x102E, 0 ) );
				Add( new GenericBuyInfo( typeof( Axle ), 2, 20, 0x105B, 0 ) );
				Add( new GenericBuyInfo( typeof( Board ), 3, 20, 0x1BD7, 0 ) );
				Add( new GenericBuyInfo( typeof( PinetreeBoard ), 3, 20, 0x1BD7, 1132 ) );
				Add( new GenericBuyInfo( typeof( CherryBoard ), 3, 20, 0x1BD7, 716 ) );
				Add( new GenericBuyInfo( typeof( OakBoard ), 3, 20, 0x1BD7, 1045 ) );
				Add( new GenericBuyInfo( typeof( PurplePassionBoard ), 3, 20, 0x1BD7, 515 ) );
				Add( new GenericBuyInfo( typeof( GoldenReflectionBoard ), 3, 20, 0x1BD7, 48 ) );
				Add( new GenericBuyInfo( typeof( HardrangerBoard ), 3, 20, 0x1BD7, 2778 ) );
				Add( new GenericBuyInfo( typeof( JadewoodBoard ), 3, 20, 0x1BD7, 1162 ) );
				Add( new GenericBuyInfo( typeof( DarkwoodBoard ), 3, 20, 0x1BD7, 1109 ) );
				Add( new GenericBuyInfo( typeof( StonewoodBoard ), 3, 20, 0x1BD7, 1154 ) );
				Add( new GenericBuyInfo( typeof( SunwoodBoard ), 3, 20, 0x1BD7, 2766 ) );
				Add( new GenericBuyInfo( typeof( GauntletBoard ), 3, 20, 0x1BD7, 2777 ) );
				Add( new GenericBuyInfo( typeof( SwampwoodBoard ), 3, 20, 0x1BD7, 2767 ) );
				Add( new GenericBuyInfo( typeof( StardustBoard ), 3, 20, 0x1BD7, 2751 ) );
				Add( new GenericBuyInfo( typeof( SilverleafBoard ), 3, 20, 0x1BD7, 2301 ) );
				Add( new GenericBuyInfo( typeof( StormtealBoard ), 3, 20, 0x1BD7, 1346 ) );
				Add( new GenericBuyInfo( typeof( EmeraldwoodBoard ), 3, 20, 0x1BD7, 2748 ) );
				Add( new GenericBuyInfo( typeof( BloodwoodBoard ), 3, 20, 0x1BD7, 1645 ) );
				Add( new GenericBuyInfo( typeof( CrystalwoodBoard ), 3, 20, 0x1BD7, 2759 ) );
				Add( new GenericBuyInfo( typeof( BloodhorseBoard ), 3, 20, 0x1BD7, 2780 ) );
				Add( new GenericBuyInfo( typeof( DoomwoodBoard ), 3, 20, 0x1BD7, 2772 ) );
				Add( new GenericBuyInfo( typeof( ZuluBoard ), 3, 20, 0x1BD7, 2749 ) );
				Add( new GenericBuyInfo( typeof( DarknessBoard ), 3, 20, 0x1BD7, 1175 ) );
				Add( new GenericBuyInfo( typeof( ElvenBoard ), 3, 20, 0x1BD7, 1165 ) );
								
				Add( new GenericBuyInfo( typeof( DrawKnife ), 10, 20, 0x10E4, 0 ) );
				Add( new GenericBuyInfo( typeof( Froe ), 10, 20, 0x10E5, 0 ) );
				Add( new GenericBuyInfo( typeof( Scorp ), 10, 20, 0x10E7, 0 ) );
				Add( new GenericBuyInfo( typeof( Inshave ), 10, 20, 0x10E6, 0 ) );
				Add( new GenericBuyInfo( typeof( DovetailSaw ), 12, 20, 0x1028, 0 ) );
				Add( new GenericBuyInfo( typeof( Saw ), 15, 20, 0x1034, 0 ) );
				Add( new GenericBuyInfo( typeof( Hammer ), 17, 20, 0x102A, 0 ) );
				Add( new GenericBuyInfo( typeof( MouldingPlane ), 11, 20, 0x102C, 0 ) );
				Add( new GenericBuyInfo( typeof( SmoothingPlane ), 10, 20, 0x1032, 0 ) );
				Add( new GenericBuyInfo( typeof( JointingPlane ), 11, 20, 0x1030, 0 ) );
				Add( new GenericBuyInfo( typeof( Drums ), 21, 20, 0xE9C, 0 ) );
				Add( new GenericBuyInfo( typeof( Tambourine ), 21, 20, 0xE9D, 0 ) );
				Add( new GenericBuyInfo( typeof( LapHarp ), 21, 20, 0xEB2, 0 ) );
				Add( new GenericBuyInfo( typeof( Lute ), 21, 20, 0xEB3, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( SmallCrate ), 5 );
				Add( typeof( MediumCrate ), 6 );
				Add( typeof( LargeCrate ), 7 );
				Add( typeof( WoodenChest ), 15 );

				Add( typeof( LargeTable ), 10 );
				Add( typeof( Nightstand ), 7 );
				Add( typeof( YewWoodTable ), 10 );

				Add( typeof( Throne ), 24 );
				Add( typeof( WoodenThrone ), 6 );
				Add( typeof( Stool ), 6 );
				Add( typeof( FootStool ), 6 );

				Add( typeof( FancyWoodenChairCushion ), 12 );
				Add( typeof( WoodenChairCushion ), 10 );
				Add( typeof( WoodenChair ), 8 );
				Add( typeof( BambooChair ), 6 );
				Add( typeof( WoodenBench ), 6 );

				Add( typeof( Saw ), 9 );
				Add( typeof( Scorp ), 6 );
				Add( typeof( SmoothingPlane ), 6 );
				Add( typeof( DrawKnife ), 6 );
				Add( typeof( Froe ), 6 );
				Add( typeof( Hammer ), 14 );
				Add( typeof( Inshave ), 6 );
				Add( typeof( JointingPlane ), 6 );
				Add( typeof( MouldingPlane ), 6 );
				Add( typeof( DovetailSaw ), 7 );
				Add( typeof( Board ), 2 );
				Add( typeof( Axle ), 1 );

				Add( typeof( Club ), 13 );

				Add( typeof( Lute ), 10 );
				Add( typeof( LapHarp ), 10 );
				Add( typeof( Tambourine ), 10 );
				Add( typeof( Drums ), 10 );

				Add( typeof( Log ), 1 );
			}
		}
	}
}
