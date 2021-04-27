using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class FrenziedOstardEgg : BaseEgg
    {
        public static readonly WeightedRandom<string> WeightedOstards;
        static FrenziedOstardEgg()
        {
            WeightedOstards = new WeightedRandom<string>();

            WeightedOstards.Add(1, "FrenziedOstard");
            WeightedOstards.Add(1, "GoldenFrenziedOstard");
            WeightedOstards.Add(1, "PlainsFrenziedOstard");
            WeightedOstards.Add(1, "MountainFrenziedOstard");
            WeightedOstards.Add(1, "SwampFrenziedOstard");
            WeightedOstards.Add(1, "HighlandFrenziedOstard");
            WeightedOstards.Add(1, "ShadowFrenziedOstard");
            WeightedOstards.Add(1, "ValleyFrenziedOstard");
            WeightedOstards.Add(1, "StoneFrenziedOstard");
            WeightedOstards.Add(1, "EmeraldFrenziedOstard");
            WeightedOstards.Add(1, "RubyFrenziedOstard");
            WeightedOstards.Add(1, "TropicalFrenziedOstard");
            WeightedOstards.Add(1, "SnowFrenziedOstard");
            WeightedOstards.Add(1, "IceFrenziedOstard");
            WeightedOstards.Add(1, "FireFrenziedOstard");
            WeightedOstards.Add(1, "HeavenlyFrenziedOstard");
        }
        
        [Constructible]
        public FrenziedOstardEgg() : this(1)
        {
        }


        [Constructible]
        public FrenziedOstardEgg(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }

        [Constructible]
        public FrenziedOstardEgg(int amount) : base(amount)
        {
            Name = "Frenzied Ostard Egg";
            Hue = 0x494;
        }

        [Constructible]
        public FrenziedOstardEgg(Serial serial) : base(serial)
        {
        }

        public override void SpawnCreatureFromEgg(Mobile from, Point3D p)
        {
            var ostard = GetOstardByChance();
            if (ostard == null)
            {
                from.SendMessage("The poor creature died just after hatching. ");
                return;
            }            

            if (Utility.RandomMinMax(0, 100) < 75)
            {
                ostard.Owners.Add(from);
                ostard.SetControlMaster(from);
                from.SendMessage("A baby ostard appears and accepts you as his master!");
            }
            else
                from.SendMessage("A baby ostard appears!");

            ostard.MoveToWorld(p, from.Map);
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

        static BaseCreature GetOstardByChance()
        {
            // From ZHA scripts, 50% fail chance
            if (Utility.Random(1, 2) == 1)
                return null;    

            return Creatures.Create(WeightedOstards.GetRandom());
        }
    }
}
