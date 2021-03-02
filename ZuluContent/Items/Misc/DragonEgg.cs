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

            WeightedDragons.AddEntry<AdamantineDragon>(1);
            WeightedDragons.AddEntry<RockDragon>(1);
            WeightedDragons.AddEntry<CelestialDragon>(1);
            WeightedDragons.AddEntry<FrostDragon>(1);
            WeightedDragons.AddEntry<InfernoDragon>(1);
            WeightedDragons.AddEntry<PoisonDragon>(1);
            WeightedDragons.AddEntry<RockDragon>(1);
            WeightedDragons.AddEntry<WaterDrake>(1);
            WeightedDragons.AddEntry<ShadowDragon>(1);
            WeightedDragons.AddEntry<StormDragon>(1);
            WeightedDragons.AddEntry<TidalDragon>(1);
            WeightedDragons.AddEntry<AirDrake>(1);
            WeightedDragons.AddEntry<EarthDrake>(1);
            WeightedDragons.AddEntry<FireDrake>(1);
            WeightedDragons.AddEntry<FrostDrake>(1);
            WeightedDragons.AddEntry<HeavenlyDrake>(1);
            WeightedDragons.AddEntry<PoisonDrake>(1);
            WeightedDragons.AddEntry<SpectralDrake>(1);
            WeightedDragons.AddEntry<UndeadDrake>(1);

            WeightedStandardDragons = new WeightedRandomType<BaseCreature>();
            WeightedStandardDragons.AddEntry<Drake>(1);
            WeightedStandardDragons.AddEntry<Dragon>(1);
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
