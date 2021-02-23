using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{

    public class FrenziedOstardEgg : Item
    {
        public static WeightedRandomType<BaseCreature> WeightedOstards;
        public override double DefaultWeight => 0.02;

        [Constructible]
        public FrenziedOstardEgg() : this(1)
        {
        }


        [Constructible]
        public FrenziedOstardEgg(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }


        [Constructible]
        public FrenziedOstardEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
            Name = "Frenzied Ostard Egg";
            Hue = 0x494;
        }

        [Constructible]
        public FrenziedOstardEgg(Serial serial) : base(serial)
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

            var ostard = GetOstardByChance();
            if (ostard == null)
            {
                from.SendMessage("The poor creature died just after hatching. ");
                return;
            }
            
            from.SendMessage("The egg begins to move and ");

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
            if (Utility.Random(0, 1) == 1)
                return null;

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

            return WeightedOstards.GetRandom();
        }
    }
}
