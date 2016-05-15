using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Harvest
{
        public class MiningResources
        {
                public static HarvestResource[] OreResources = new HarvestResource[]
                {
                        new HarvestResource( 0.00, 0.00, 100.00, 1007072, typeof( IronOre ), typeof( Granite ) ),
                        new HarvestResource( 10.00, 10.00, 100.00, 1007072, typeof( DullCopperOre ), typeof( Granite ) ),
                };

                public static HarvestVein[] OreVeins = new HarvestVein[]
                {                
                        new HarvestVein( 69.93, 0.00, OreResources[0], OreResources[0] ),                
                        new HarvestVein( 30.07, 0.00, OreResources[1], OreResources[0] ),
                };
        }
};
