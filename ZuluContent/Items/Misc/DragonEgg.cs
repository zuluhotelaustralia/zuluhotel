using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class DragonEgg : BaseEgg
    {        
        public static readonly WeightedRandom<string> WeightedStandardDragons;
        public static readonly WeightedRandom<string> WeightedDragons;
        
        static DragonEgg()
        {
            WeightedDragons = new WeightedRandom<string>();

            WeightedDragons.Add(1, "AdamantineDragon");
            WeightedDragons.Add(1, "RockDragon");
            WeightedDragons.Add(1, "CelestialDragon");
            WeightedDragons.Add(1, "FrostDragon");
            WeightedDragons.Add(1, "InfernoDragon");
            WeightedDragons.Add(1, "PoisonDragon");
            WeightedDragons.Add(1, "RockDragon");
            WeightedDragons.Add(1, "WaterDrake");
            WeightedDragons.Add(1, "ShadowDragon");
            WeightedDragons.Add(1, "StormDragon");
            WeightedDragons.Add(1, "TidalDragon");
            WeightedDragons.Add(1, "AirDrake");
            WeightedDragons.Add(1, "EarthDrake");
            WeightedDragons.Add(1, "FireDrake");
            WeightedDragons.Add(1, "FrostDrake");
            WeightedDragons.Add(1, "HeavenlyDrake");
            WeightedDragons.Add(1, "PoisonDrake");
            WeightedDragons.Add(1, "SpectralDrake");
            WeightedDragons.Add(1, "UndeadDrake");

            WeightedStandardDragons = new WeightedRandom<string>();
            WeightedStandardDragons.Add(1, "Drake");
            WeightedStandardDragons.Add(1, "Dragon");
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
                return Creatures.Create(WeightedDragons.GetRandom());            
            // 75% chance to spawn regular dragon or drake
            else            
                return Creatures.Create(WeightedStandardDragons.GetRandom());                        
        }
    }
}
