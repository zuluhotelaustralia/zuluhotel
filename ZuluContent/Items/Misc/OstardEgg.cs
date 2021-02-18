using Server.Mobiles;
using System;

namespace Server.Items
{

    public class OstardEgg : Item
    {


        public override double DefaultWeight
        {
            get { return 0.02; }
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
        public OstardEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
            Name = "OstardEgg";
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
            Consume(1);
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

        private BaseCreature GetOstardByChance()
        {
            object ostard = Utility.Random(0, 33) switch
            {
                < 4 => new GoldenOstard(),
                < 7 => new PlainsOstard(),
                < 11 => new MountainOstard(),
                < 15 => new SwampOstard(),
                < 19 => new HighlandOstard(),
                < 21 => new ShadowOstard(),
                < 23 => new ValleyOstard(),
                < 25 => new StoneOstard(),
                < 27 => new EmeraldOstard(),
                < 29 => new RubyOstard(),
                < 30 => new TropicalOstard(),
                < 31 => new SnowOstard(),
                < 32 => new IceOstard(),
                < 33 => new FireOstard(),
                >= 33 => new HeavenlyOstard()                
            };

            return (BaseCreature)ostard;
        }
    }
}




