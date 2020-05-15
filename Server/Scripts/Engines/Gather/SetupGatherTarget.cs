using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Multis;
using Server.Mobiles;

namespace Server.Engines.Gather
{
    public class SetupGatherTarget : Target
    {
        private Item m_Tool;
        private GatherSystem m_System;

        public SetupGatherTarget() : base(-1, true, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (targeted is GatherSystemController)
            {
                if (((GatherSystemController)targeted).SystemType != ControlledSystem.None)
                {
                    ControlledSystem sys = ((GatherSystemController)targeted).SystemType;

                    switch (sys)
                    {
                        case ControlledSystem.Mining:
                            {
                                SpawnMiningNodes((GatherSystemController)targeted);
                                from.SendMessage("Mining set up complete.");
                                break;
                            }
                        case ControlledSystem.Lumberjacking:
                            {
                                SpawnLumberjackingNodes((GatherSystemController)targeted);
                                from.SendMessage("Lumberjacking set up complete.");
                                break;
                            }
                        case ControlledSystem.Fishing:
                            {
                                SpawnFishingNodes((GatherSystemController)targeted);
                                from.SendMessage("Fishing set up complete.");
                                break;
                            }
                    }
                }
                else
                {
                    from.SendMessage("You must set the stone's controlled system reference first.");
                    return;
                }
            }
        }

        //GatherNode( int initialX, int initialY, int dirX, int dirY, double a, double d, double minskill, double maxskill, Type res )
        // iron d = 250, nimbus ~10
        // a = [0,1]
        private void SpawnMiningNodes(GatherSystemController stone)
        {

            int thronex = 1323; //LB's throne
            int throney = 1624;

            stone.System.ClearNodes();

            //yup, this is happening
            stone.System.AddNode(new GatherNode(thronex, throney, 4, 9, 1.0, 243.0, 10.0, 100.0, typeof(IronOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 8, 4, 1.0, 236.0, 20.0, 100.0, typeof(SpikeOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 9, 8, 1.0, 229.0, 30.0, 100.0, typeof(FruityOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 9, 1, 0.9, 222.0, 40.0, 100.0, typeof(BronzeOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 1, 0.9, 215.0, 50.0, 100.0, typeof(IceRockOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 1, 0.9, 208.0, 60.0, 90.0, typeof(BlackDwarfOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 9, 0.9, 201.0, 70.0, 100.0, typeof(DullCopperOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 3, 0.8, 194.0, 80.0, 110.0, typeof(PlatinumOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 8, 4, 0.8, 187.0, 85.0, 115.0, typeof(SilverRockOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 8, 0.8, 180.0, 90.0, 120.0, typeof(DarkPaganOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 2, 7, 0.8, 173.0, 95.0, 125.0, typeof(CopperOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 9, 1, 0.7, 166.0, 100.0, 130.0, typeof(MysticOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 1, 1, 0.7, 159.0, 105.0, 135.0, typeof(SpectralOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 4, 1, 0.7, 152.0, 110.0, 140.0, typeof(OldBritainOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 8, 3, 0.7, 145.0, 111.0, 141.0, typeof(OnyxOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 1, 4, 0.6, 138.0, 112.0, 142.0, typeof(RedElvenOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 10, 7, 0.6, 131.0, 113.0, 143.0, typeof(UndeadOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 8, 7, 0.6, 124.0, 114.0, 144.0, typeof(PyriteOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 4, 0.6, 117.0, 115.0, 145.0, typeof(VirginityOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 4, 8, 0.5, 110.0, 116.0, 146.0, typeof(MalachiteOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 2, 0.5, 103.0, 117.0, 147.0, typeof(LavarockOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 2, 0.5, 96.0, 118.0, 148.0, typeof(AzuriteOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 4, 0.5, 89.0, 119.0, 149.0, typeof(DripstoneOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 1, 0.4, 82.0, 120.0, 150.0, typeof(ExecutorOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 3, 2, 0.4, 75.0, 120.0, 150.0, typeof(PeachblueOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 4, 0.4, 68.0, 120.0, 150.0, typeof(DestructionOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 10, 4, 0.4, 61.0, 120.0, 150.0, typeof(AnraOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 3, 9, 0.3, 54.0, 120.0, 150.0, typeof(CrystalOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 2, 1, 0.3, 53.0, 120.0, 150.0, typeof(DoomOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 2, 0.3, 52.0, 120.0, 150.0, typeof(GoddessOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 1, 7, 0.3, 51.0, 120.0, 150.0, typeof(NewZuluOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 1, 0.1, 30.0, 130.0, 150.0, typeof(EbonTwilightSapphireOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 4, 7, 0.1, 20.0, 140.0, 150.0, typeof(DarkSableRubyOre)));
            stone.System.AddNode(new GatherNode(thronex, throney, 8, 9, 0.1, 10.0, 149.0, 150.0, typeof(RadiantNimbusDiamondOre)));
        }

        private void SpawnLumberjackingNodes(GatherSystemController stone)
        {

            int thronex = 1323; //LB's throne
            int throney = 1624;

            stone.System.ClearNodes();

            stone.System.AddNode(new GatherNode(thronex, throney, 3, 7, 1.0, 240.0, 10.0, 100.0, typeof(Log)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 5, 1, 230.0, 20.0, 100.0, typeof(PinetreeLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 10, 1, 220.0, 30.0, 100.0, typeof(CherryLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 9, 5, 0.9, 210.0, 40.0, 100.0, typeof(OakLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 4, 0.9, 200.0, 50.0, 100.0, typeof(PurplePassionLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 10, 0.9, 190.0, 60.0, 90.0, typeof(GoldenReflectionLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 8, 5, 0.9, 180.0, 70.0, 100.0, typeof(HardrangerLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 8, 0.8, 170.0, 80.0, 110.0, typeof(JadewoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 3, 5, 0.8, 160.0, 85.0, 115.0, typeof(DarkwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 9, 2, 0.8, 150.0, 90.0, 120.0, typeof(StonewoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 3, 7, 0.8, 140.0, 95.0, 125.0, typeof(SunwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 6, 1, 0.7, 130.0, 100.0, 130.0, typeof(GauntletLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 10, 0.7, 120.0, 105.0, 135.0, typeof(SwampwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 2, 9, 0.7, 110.0, 110.0, 140.0, typeof(StardustLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 3, 0.7, 100.0, 111.0, 141.0, typeof(SilverleafLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 1, 2, 0.6, 90.0, 112.0, 142.0, typeof(StormtealLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 7, 6, 0.6, 80.0, 113.0, 143.0, typeof(EmeraldwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 4, 1, 0.6, 79.0, 114.0, 144.0, typeof(BloodwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 2, 5, 0.6, 78.0, 115.0, 145.0, typeof(CrystalwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 10, 7, 0.5, 77.0, 116.0, 146.0, typeof(BloodhorseLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 9, 0.5, 76.0, 117.0, 147.0, typeof(DoomwoodLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 7, 0.5, 75.0, 118.0, 148.0, typeof(ZuluLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 5, 8, 0.5, 74.0, 119.0, 149.0, typeof(DarknessLog)));
            stone.System.AddNode(new GatherNode(thronex, throney, 10, 5, 0.4, 73.0, 120.0, 150.0, typeof(ElvenLog)));
        }

        private void SpawnFishingNodes(GatherSystemController stone)
        {
            int thronex = 1323;
            int throney = 1624;

            stone.System.ClearNodes();

            stone.System.AddNode(new GatherNode(thronex, throney, 10, 5, 1.0, 240.0, 0.0, 150.0, typeof(Fish)));
            stone.System.AddNode(new GatherNode(thronex, throney, 10, 5, 1.0, 50.0, 100.0, 150.0, typeof(BigFish)));
            // tmaps, special nets, etc. go here

        }

    }
}
