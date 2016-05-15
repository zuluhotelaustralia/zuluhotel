// Ore resources

new HarvestResource[]
				{
					new HarvestResource( 00.0, 00.0, 100.0, 1007072, typeof( IronOre ),			typeof( Granite ) ),
					new HarvestResource( 65.0, 25.0, 105.0, 1007073, typeof( DullCopperOre ),	typeof( DullCopperGranite ),	typeof( DullCopperElemental ) ),
					new HarvestResource( 70.0, 30.0, 110.0, 1007074, typeof( ShadowIronOre ),	typeof( ShadowIronGranite ),	typeof( ShadowIronElemental ) ),
					new HarvestResource( 75.0, 35.0, 115.0, 1007075, typeof( CopperOre ),		typeof( CopperGranite ),		typeof( CopperElemental ) ),
					new HarvestResource( 80.0, 40.0, 120.0, 1007076, typeof( BronzeOre ),		typeof( BronzeGranite ),		typeof( BronzeElemental ) ),
					new HarvestResource( 85.0, 45.0, 125.0, 1007077, typeof( GoldOre ),			typeof( GoldGranite ),			typeof( GoldenElemental ) ),
					new HarvestResource( 90.0, 50.0, 130.0, 1007078, typeof( AgapiteOre ),		typeof( AgapiteGranite ),		typeof( AgapiteElemental ) ),
					new HarvestResource( 95.0, 55.0, 135.0, 1007079, typeof( VeriteOre ),		typeof( VeriteGranite ),		typeof( VeriteElemental ) ),
					new HarvestResource( 99.0, 59.0, 139.0, 1007080, typeof( ValoriteOre ),		typeof( ValoriteGranite ),		typeof( ValoriteElemental ) )
				};


// ORe veings
new HarvestVein[]
				{
					new HarvestVein( 49.6, 0.0, res[0], null   ), // Iron
					new HarvestVein( 11.2, 0.5, res[1], res[0] ), // Dull Copper
					new HarvestVein( 09.8, 0.5, res[2], res[0] ), // Shadow Iron
					new HarvestVein( 08.4, 0.5, res[3], res[0] ), // Copper
					new HarvestVein( 07.0, 0.5, res[4], res[0] ), // Bronze
					new HarvestVein( 05.6, 0.5, res[5], res[0] ), // Gold
					new HarvestVein( 04.2, 0.5, res[6], res[0] ), // Agapite
					new HarvestVein( 02.8, 0.5, res[7], res[0] ), // Verite
					new HarvestVein( 01.4, 0.5, res[8], res[0] )  // Valorite
				};
