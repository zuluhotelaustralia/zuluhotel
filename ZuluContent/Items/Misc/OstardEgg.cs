using Server.Mobiles;
using Server.Spells;
using System;

namespace Server.Items
{

    public class OstardEgg : Item
    {


        public override double DefaultWeight => 0.02;


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

        private static BaseCreature GetOstardByChance()
        {
            WeightedRandomType<BaseCreature> random = new WeightedRandomType<BaseCreature>();

            // Weight 4
            random.AddEntry<GoldenOstard>(4);
            random.AddEntry<PlainsOstard>(4);
            random.AddEntry<MountainOstard>(4);
            random.AddEntry<SwampOstard>(4);
            random.AddEntry<HighlandOstard>(4);
            // Weight 2
            random.AddEntry<ShadowOstard>(2);
            random.AddEntry<ValleyOstard>(2);
            random.AddEntry<StoneOstard>(2);
            random.AddEntry<EmeraldOstard>(2);
            random.AddEntry<RubyOstard>(2);
            // Weight 1
            random.AddEntry<TropicalOstard>(1);
            random.AddEntry<SnowOstard>(1);
            random.AddEntry<IceOstard>(1);
            random.AddEntry<FireOstard>(1);
            random.AddEntry<HeavenlyOstard>(1);

            return random.GetRandom();
        }
    }
}




