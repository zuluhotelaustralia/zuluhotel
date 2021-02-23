using Server.Mobiles;
using Server.Spells;
using System;

namespace Server.Items
{

    public class OstardEgg : Item
    {
        public override double DefaultWeight => 0.02;
        public static WeightedRandomType<BaseCreature> WeightedOstards;

        [Constructible]
        public OstardEgg() : this(1)
        {
        }


        [Constructible]
        public OstardEgg(int amountFrom, int amountTo) : this(Utility.RandomMinMax(amountFrom, amountTo))
        {
        }


        [Constructible]
        public OstardEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
            Name = "Ostard Egg";
            Hue = 1102;
        }

        [Constructible]
        public OstardEgg(Serial serial) : base(serial)
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

            return WeightedOstards.GetRandom();
        }
    }
}




