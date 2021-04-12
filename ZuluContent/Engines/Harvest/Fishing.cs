using System;
using Server.Items;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Regions;
using Server.Utilities;

namespace Server.Engines.Harvest
{
    public class Fishing : HarvestSystem
    {
        private static Fishing m_System;
        
        public static Fishing System => m_System ??= new Fishing();

        private Fishing()
        {
            #region Fishing
            
            var defaultFish = ZhConfig.Resources.Fish.Entries[0];
            var defaultVein = new HarvestVein(defaultFish.VeinChance, new HarvestResource(
                defaultFish.HarvestSkillRequired,
                defaultFish.Name, defaultFish.ResourceType));

            var fish = new HarvestDefinition
            {
                BankWidth = ZhConfig.Resources.Fish.BankWidth,
                BankHeight = ZhConfig.Resources.Fish.BankHeight,
                
                MinTotal = ZhConfig.Resources.Fish.MinTotal,
                MaxTotal = ZhConfig.Resources.Fish.MaxTotal,

                MinRespawn = TimeSpan.FromMinutes(ZhConfig.Resources.Fish.MinRespawn),
                MaxRespawn = TimeSpan.FromMinutes(ZhConfig.Resources.Fish.MaxRespawn),
                
                Skill = ZhConfig.Resources.Fish.Skill,
                
                Tiles = m_WaterTiles,
                RangedTiles = true,
                
                MaxRange = ZhConfig.Resources.Fish.MaxRange,
                
                ConsumedPerHarvest = skillValue => 1,
                
                EffectActions = ZhConfig.Resources.Fish.FishEffect.Actions,
                EffectSounds = ZhConfig.Resources.Fish.FishEffect.Sounds,
                EffectCounts = ZhConfig.Resources.Fish.FishEffect.Counts,
                EffectDelay = TimeSpan.FromSeconds(ZhConfig.Resources.Fish.FishEffect.Delay),
                EffectSoundDelay = TimeSpan.FromSeconds(ZhConfig.Resources.Fish.FishEffect.SoundDelay),

                NoResourcesMessage = ZhConfig.Resources.Fish.Messages.NoResourcesMessage,
                TimedOutOfRangeMessage = ZhConfig.Resources.Fish.Messages.TimedOutOfRangeMessage,
                OutOfRangeMessage = ZhConfig.Resources.Fish.Messages.OutOfRangeMessage,
                FailMessage = ZhConfig.Resources.Fish.Messages.FailMessage,
                PackFullMessage = ZhConfig.Resources.Fish.Messages.PackFullMessage,
                ToolBrokeMessage = ZhConfig.Resources.Fish.Messages.ToolBrokeMessage,

                DefaultVein = defaultVein,
                
                BonusEffect = HarvestBonusEffect
            };
            
            Definitions.Add(fish);

            #endregion
        }

        private static void SpawnCreature(Mobile harvester, Type creatureType)
        {
            var creature = creatureType.CreateInstance<BaseCreature>();
            var location = harvester.Location;
            location.X -= 1;
            creature.MoveToWorld(location, harvester.Map);
        }
        
        private static BaseShell GetRandomShell(Mobile harvester)
        {
            var chance = Utility.Random(5);

            BaseShell shell;
            
            switch (chance)
            {
                case 0:
                    return new AquariaShell();
                case 1:
                    return new CapricorniaShell();
                case 2:
                    return new SeaNymphShell();
                case 3:
                case 4:
                {
                    if (harvester.Skills[SkillName.Fishing].Value > 100.0)
                    {
                        if (Utility.Random(3) == 2)
                            return new NeptunesNautilusShell();
                        
                        return new SeaShoreSandDollarShell();
                    }
                    
                    return new SeaNymphShell();
                }
                default:
                    return new AquariaShell();
            }
        }
        
        private static void HarvestBonusEffect(Mobile harvester, Item tool)
        {
            if (Utility.Random(70) >= 5)
                return;

            if (Utility.Random(100) < 2 && harvester.ShilCheckSkill(SkillName.Fishing, 110, 200))
            {
                // Create SOS Bottle
                return;
            }
           
            var chance = Utility.Random(1, 10);

            Item item = null;
            var message = "";

            switch (chance)
            {
                case 1:
                {
                    item = new SpecialFishingNet();
                    message = "You just found a special fishing net.";
                    break;
                }
                case 2:
                {
                    if (Utility.Random(10) < 4 && !Region.Find(harvester.Location, harvester.Map)
                        .GetRegion<GuardedRegion>()?.IsDisabled() == false)
                    {
                        SpawnCreature(harvester, typeof(WaterElemental));
                        message = "You attract the attention of a water elemental";
                    }
                    break;
                }
                case 3:
                {
                    if (Utility.Random(10) < 4)
                    {
                        SpawnCreature(harvester, typeof(Walrus));
                        message = "A walrus comes by to see what you're doing";
                    }
                    break;
                }
                case 4:
                {
                    item = new TreasureMap(2, Map.Felucca);
                    message = "You find a tattered old map!";
                    break;
                }
                case 5:
                {
                    item = new Backpack();
                    item.AddItem(new Gold(Utility.Random(300) + 100));
                    message = "You find an old backpack!";
                    break;
                }
                case 6:
                {
                    item = new Seaweed();
                    message = "You find some seaweed!";
                    break;
                }
                case 8:
                {
                    item = GetRandomShell(harvester);
                    message = "You find something valuable!";
                    break;
                }
            }

            if (item != null)
            {
                var cont = harvester.Backpack;
                if (cont.TryDropItem(harvester, item, false))
                {
                    if (message.Length > 0)
                        harvester.SendSuccessMessage(message);
                }
                else if (message.Length > 0)
                    harvester.SendFailureMessage(message);
            }
            else if (message.Length > 0)
                harvester.SendSuccessMessage(message);
        }

        public override void OnConcurrentHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            from.SendLocalizedMessage(500972); // You are already fishing.
        }

        public override void OnHarvestStarted(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            base.OnHarvestStarted(from, tool, def, toHarvest);

            int tileID;
            Map map;
            Point3D loc;

            if (GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
                Timer.DelayCall(TimeSpan.FromSeconds(1.5),
                    delegate
                    {
                        Effects.SendLocationEffect(loc, map, 0x352D, 16, 4);
                        Effects.PlaySound(loc, map, 0x364);
                    });
        }

        public override object GetLock(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            return this;
        }

        public override bool BeginHarvesting(Mobile from, Item tool)
        {
            if (!base.BeginHarvesting(from, tool))
                return false;

            from.SendSuccessMessage(500974); // What water do you want to fish in?
            return true;
        }

        public override bool CheckHarvest(Mobile from, Item tool)
        {
            if (!base.CheckHarvest(from, tool))
                return false;

            return true;
        }

        public override bool CheckHarvest(Mobile from, Item tool, HarvestDefinition def, object toHarvest)
        {
            if (!base.CheckHarvest(from, tool, def, toHarvest))
                return false;

            return true;
        }

        private static int[] m_WaterTiles = new[]
        {
            0x00A8, 0x00AB,
            0x0136, 0x0137,
            0x5797, 0x579C,
            0x746E, 0x7485,
            0x7490, 0x74AB,
            0x74B5, 0x75D5
        };
    }
}