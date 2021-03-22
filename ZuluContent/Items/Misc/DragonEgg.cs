using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class DragonEgg : BaseEgg
    {        
        public static WeightedRandomType<BaseCreature> WeightedStandardDragons;
        public static WeightedRandomType<BaseCreature> WeightedDragons;
        
        static DragonEgg()
        {
            WeightedDragons = new WeightedRandomType<BaseCreature>();

            WeightedDragons.Add<AdamantineDragon>(1);
            WeightedDragons.Add<RockDragon>(1);
            WeightedDragons.Add<CelestialDragon>(1);
            WeightedDragons.Add<FrostDragon>(1);
            WeightedDragons.Add<InfernoDragon>(1);
            WeightedDragons.Add<PoisonDragon>(1);
            WeightedDragons.Add<RockDragon>(1);
            WeightedDragons.Add<WaterDrake>(1);
            WeightedDragons.Add<ShadowDragon>(1);
            WeightedDragons.Add<StormDragon>(1);
            WeightedDragons.Add<TidalDragon>(1);
            WeightedDragons.Add<AirDrake>(1);
            WeightedDragons.Add<EarthDrake>(1);
            WeightedDragons.Add<FireDrake>(1);
            WeightedDragons.Add<FrostDrake>(1);
            WeightedDragons.Add<HeavenlyDrake>(1);
            WeightedDragons.Add<PoisonDrake>(1);
            WeightedDragons.Add<SpectralDrake>(1);
            WeightedDragons.Add<UndeadDrake>(1);

            WeightedStandardDragons = new WeightedRandomType<BaseCreature>();
            WeightedStandardDragons.Add<Drake>(1);
            WeightedStandardDragons.Add<Dragon>(1);
        }

        [Constructible]
        public DragonEgg() : this(1)
        {
        }


        [Constructible]
        public DragonEgg(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        
        [Constructible]
        public DragonEgg(int amount) : base(amount)
        {
            Name = "Dragons Egg";
            Hue = 33;
        }

        [Constructible]
        public DragonEgg(Serial serial) : base(serial)
        {
        }

        public override void SpawnCreatureFromEgg(Mobile from, Point3D p)
        {
            var dragon = GetDragonByChance();

            if (from.Skills.AnimalTaming.Value > 90 && Utility.RandomMinMax(0, 100) > 70)
            {
                dragon.Owners.Add(from);
                dragon.SetControlMaster(from);
                from.SendMessage("A baby dragon appears and accepts you as his master!");
            }
            else
                from.SendMessage("A baby dragon appears!");

            dragon.MoveToWorld(p, from.Map);
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            
            int version = reader.ReadInt();
        }

        static BaseCreature GetDragonByChance()
        {                                   
            // 25% chance to spawn color dragons
            if (Utility.RandomMinMax(1,5) == 5)            
                return WeightedDragons.GetRandom();            
            // 75% chance to spawn regular dragon or drake
            else            
                return WeightedStandardDragons.GetRandom();                        
        }
    }
}
