using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class OstardEgg : BaseEgg
    {        
        public static WeightedRandomType<BaseCreature> WeightedOstards;
        static OstardEgg()
        {
            WeightedOstards = new WeightedRandomType<BaseCreature>();

            // Weight 4
            WeightedOstards.AddEntry<GoldenOstard>(4);
            WeightedOstards.AddEntry<PlainsOstard>(4);
            WeightedOstards.AddEntry<MountainOstard>(4);
            WeightedOstards.AddEntry<SwampOstard>(4);
            WeightedOstards.AddEntry<HighlandOstard>(4);
            // Weight 2
            WeightedOstards.AddEntry<ShadowOstard>(2);
            WeightedOstards.AddEntry<ValleyOstard>(2);
            WeightedOstards.AddEntry<StoneOstard>(2);
            WeightedOstards.AddEntry<EmeraldOstard>(2);
            WeightedOstards.AddEntry<RubyOstard>(2);
            // Weight 1
            WeightedOstards.AddEntry<TropicalOstard>(1);
            WeightedOstards.AddEntry<SnowOstard>(1);
            WeightedOstards.AddEntry<IceOstard>(1);
            WeightedOstards.AddEntry<FireOstard>(1);
            WeightedOstards.AddEntry<HeavenlyOstard>(1);
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
            return WeightedOstards.GetRandom();
        }
    }
}




