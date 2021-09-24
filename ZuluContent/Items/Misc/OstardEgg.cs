using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class OstardEgg : BaseEgg
    {        
        public static WeightedRandom<string> WeightedOstards;
        static OstardEgg()
        {
            WeightedOstards = new WeightedRandom<string>();

            // Weight 4
            WeightedOstards.Add(4, "GoldenOstard");
            WeightedOstards.Add(4, "PlainsOstard");
            WeightedOstards.Add(4, "MountainOstard");
            WeightedOstards.Add(4, "SwampOstard");
            WeightedOstards.Add(4, "HighlandOstard");
            // Weight 2
            WeightedOstards.Add(2, "ShadowOstard");
            WeightedOstards.Add(2, "ValleyOstard");
            WeightedOstards.Add(2, "StoneOstard");
            WeightedOstards.Add(2, "EmeraldOstard");
            WeightedOstards.Add(2, "RubyOstard");
            // Weight 1
            WeightedOstards.Add(1, "TropicalOstard");
            WeightedOstards.Add(1, "SnowOstard");
            WeightedOstards.Add(1, "IceOstard");
            WeightedOstards.Add(1, "FireOstard");
            WeightedOstards.Add(1, "HeavenlyOstard");
        }

        [Constructible]
        public OstardEgg() : this(1)
        {
        }


        [Constructible]
        public OstardEgg(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }


        [Constructible]
        public OstardEgg(int amount) : base(amount)
        {
            Name = "Ostard Egg";
            Hue = 1102;
        }

        [Constructible]
        public OstardEgg(Serial serial) : base(serial)
        {
        }

        public override void SpawnCreatureFromEgg(Mobile from, Point3D p)
        {
            var ostard = GetOstardByChance();

            if (from.Skills.AnimalTaming.Value > 90 && Utility.RandomMinMax(0, 100) > 70)
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
            return Creatures.Create(WeightedOstards.GetRandom());
        }
    }
}




