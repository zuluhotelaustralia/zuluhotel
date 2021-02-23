using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class DragonEgg : Item
    {
        public override double DefaultWeight => 0.02;


        [Constructible]
        public DragonEgg() : this(1)
        {
        }


        [Constructible]
        public DragonEgg(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        
        [Constructible]
        public DragonEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
            Name = "Dragons Egg";
            Hue = 33;
        }

        [Constructible]
        public DragonEgg(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }

            var p = new Point3D(from);
            if (!SpellHelper.FindValidSpawnLocation(from.Map, ref p, true))
                return;

            Consume(1);
            from.SendMessage("The egg begins to move and ");            

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

        private static BaseCreature GetDragonByChance()
        {
            WeightedRandomType<BaseCreature> random = new WeightedRandomType<BaseCreature>();
                       
            // 25% chance to spawn color dragons
            if (Utility.RandomMinMax(0,4) == 4)
            {
                random.AddEntry<AdamantineDragon>(1);
                random.AddEntry<RockDragon>(1);
                random.AddEntry<CelestialDragon>(1);
                random.AddEntry<FrostDragon>(1);
                random.AddEntry<InfernoDragon>(1);
                random.AddEntry<PoisonDragon>(1);
                random.AddEntry<RockDragon>(1);
                random.AddEntry<WaterDrake>(1);
                random.AddEntry<ShadowDragon>(1);
                random.AddEntry<StormDragon>(1);
                random.AddEntry<TidalDragon>(1);
                random.AddEntry<AirDrake>(1);
                random.AddEntry<EarthDrake>(1);
                random.AddEntry<FireDrake>(1);
                random.AddEntry<FrostDrake>(1);
                random.AddEntry<HeavenlyDrake>(1);
                random.AddEntry<PoisonDrake>(1);
                random.AddEntry<SpectralDrake>(1);
                random.AddEntry<UndeadDrake>(1);
            }
            // 75% chance to spawn regular dragon or drake
            else
            {
                random.AddEntry<Drake>(1);
                random.AddEntry<Dragon>(1);
            }

            return random.GetRandom();
        }
    }
}
