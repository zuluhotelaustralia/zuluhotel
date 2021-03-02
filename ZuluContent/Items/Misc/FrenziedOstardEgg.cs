using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class FrenziedOstardEgg : BaseEgg
    {
        public static WeightedRandomType<BaseCreature> WeightedOstards;
        static FrenziedOstardEgg()
        {
            WeightedOstards = new WeightedRandomType<BaseCreature>();

            WeightedOstards.AddEntry<FrenziedOstard>(1);
            WeightedOstards.AddEntry<GoldenFrenziedOstard>(1);
            WeightedOstards.AddEntry<PlainsFrenziedOstard>(1);
            WeightedOstards.AddEntry<MountainFrenziedOstard>(1);
            WeightedOstards.AddEntry<SwampFrenziedOstard>(1);
            WeightedOstards.AddEntry<HighlandFrenziedOstard>(1);
            WeightedOstards.AddEntry<ShadowFrenziedOstard>(1);
            WeightedOstards.AddEntry<ValleyFrenziedOstard>(1);
            WeightedOstards.AddEntry<StoneFrenziedOstard>(1);
            WeightedOstards.AddEntry<EmeraldFrenziedOstard>(1);
            WeightedOstards.AddEntry<RubyFrenziedOstard>(1);
            WeightedOstards.AddEntry<TropicalFrenziedOstard>(1);
            WeightedOstards.AddEntry<SnowFrenziedOstard>(1);
            WeightedOstards.AddEntry<IceFrenziedOstard>(1);
            WeightedOstards.AddEntry<FireFrenziedOstard>(1);
            WeightedOstards.AddEntry<HeavenlyFrenziedOstard>(1);
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

            return WeightedOstards.GetRandom();
        }
    }
}
