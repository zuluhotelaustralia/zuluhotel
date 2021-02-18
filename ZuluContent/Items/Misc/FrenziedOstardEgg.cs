using Server.Mobiles;
using System;

namespace Server.Items
{

    public class FrenziedOstardEgg : Item
    {


        public override double DefaultWeight
        {
            get { return 0.02; }
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
        public FrenziedOstardEgg(int amount) : base(0x1725)
        {
            Stackable = true;
            Amount = amount;
            Name = "FrenziedOstardEgg";
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

            Consume(1);

            var ostard = GetOstardByChance();
            if (ostard == null)
            {
                from.SendMessage("The poor creature died just after hatching. ");
                return;
            }

            var p = new Point3D(from);

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

        private BaseCreature GetOstardByChance()
        {

            return (Utility.RandomMinMax(0, 34)) switch
            {
                0 or 1 or 2 or 3 => null,
                4 => new FrenziedOstard(),
                5 or 6 => null,
                7 => new GoldenFrenziedOstard(),
                8 or 9 => null,
                10 => new PlainsFrenziedOstard(),
                11 or 12 => null,
                13 => new MountainFrenziedOstard(),
                14 or 15 => null,
                16 => new SwampFrenziedOstard(),
                17 => null,
                18 => new HighlandFrenziedOstard(),
                19 => null,
                20 => new ShadowFrenziedOstard(),
                21 => null,
                22 => new ValleyFrenziedOstard(),
                23 => null,
                24 => new StoneFrenziedOstard(),
                25 => null,
                26 => new EmeraldFrenziedOstard(),
                27 => null,
                28 => new RubyFrenziedOstard(),
                29 => null,
                30 => new TropicalFrenziedOstard(),
                31 => new SnowFrenziedOstard(),
                32 => new IceFrenziedOstard(),
                33 => new FireFrenziedOstard(),
                34 => new HeavenlyFrenziedOstard(),
                _ => null,
            };
        }
    }
}
