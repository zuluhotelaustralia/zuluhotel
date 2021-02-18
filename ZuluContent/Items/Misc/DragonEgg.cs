using Server.Mobiles;

namespace Server.Items
{

    public class DragonEgg : Item
    {


        public override double DefaultWeight
        {
            get { return 0.02; }
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
        public DragonEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
            Name = "DragonEgg";
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

            Consume(1);
            from.SendMessage("The egg begins to move and ");
            
            var p = new Point3D(from);
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

        private BaseCreature GetDragonByChance()
        {                 
            if (Utility.RandomMinMax(0,4) == 4)
            {
                return (Utility.RandomMinMax(1, 19)) switch
                {
                    1 => new AdamantineDragon(),
                    2 => new RockDragon(),
                    3 => new CelestialDragon(),
                    4 => new FrostDragon(),
                    5 => new InfernoDragon(),
                    6 => new PoisonDragon(),
                    7 => new RockDragon(),
                    8 => new WaterDrake(),
                    9 => new ShadowDragon(),
                    10 => new StormDragon(),
                    11 => new TidalDragon(),
                    12 => new AirDrake(),
                    13 => new EarthDrake(),
                    14 => new FireDrake(),
                    15 => new FrostDrake(),
                    16 => new HeavenlyDrake(),
                    17 => new PoisonDrake(),
                    18 => new SpectralDrake(),
                    19 => new UndeadDrake(),
                    _ => null,
                };
            }
            else
            {
                return (Utility.RandomMinMax(1, 2)) switch
                {
                    1 => new Drake(),
                    2 => new Dragon(),
                    _ => null,
                };
            }            
        }
    }
}
