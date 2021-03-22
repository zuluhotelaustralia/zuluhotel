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

            WeightedOstards.Add<FrenziedOstard>(1);
            WeightedOstards.Add<GoldenFrenziedOstard>(1);
            WeightedOstards.Add<PlainsFrenziedOstard>(1);
            WeightedOstards.Add<MountainFrenziedOstard>(1);
            WeightedOstards.Add<SwampFrenziedOstard>(1);
            WeightedOstards.Add<HighlandFrenziedOstard>(1);
            WeightedOstards.Add<ShadowFrenziedOstard>(1);
            WeightedOstards.Add<ValleyFrenziedOstard>(1);
            WeightedOstards.Add<StoneFrenziedOstard>(1);
            WeightedOstards.Add<EmeraldFrenziedOstard>(1);
            WeightedOstards.Add<RubyFrenziedOstard>(1);
            WeightedOstards.Add<TropicalFrenziedOstard>(1);
            WeightedOstards.Add<SnowFrenziedOstard>(1);
            WeightedOstards.Add<IceFrenziedOstard>(1);
            WeightedOstards.Add<FireFrenziedOstard>(1);
            WeightedOstards.Add<HeavenlyFrenziedOstard>(1);
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
