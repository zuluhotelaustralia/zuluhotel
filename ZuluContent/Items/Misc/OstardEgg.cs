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
            WeightedOstards.Add<GoldenOstard>(4);
            WeightedOstards.Add<PlainsOstard>(4);
            WeightedOstards.Add<MountainOstard>(4);
            WeightedOstards.Add<SwampOstard>(4);
            WeightedOstards.Add<HighlandOstard>(4);
            // Weight 2
            WeightedOstards.Add<ShadowOstard>(2);
            WeightedOstards.Add<ValleyOstard>(2);
            WeightedOstards.Add<StoneOstard>(2);
            WeightedOstards.Add<EmeraldOstard>(2);
            WeightedOstards.Add<RubyOstard>(2);
            // Weight 1
            WeightedOstards.Add<TropicalOstard>(1);
            WeightedOstards.Add<SnowOstard>(1);
            WeightedOstards.Add<IceOstard>(1);
            WeightedOstards.Add<FireOstard>(1);
            WeightedOstards.Add<HeavenlyOstard>(1);
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




