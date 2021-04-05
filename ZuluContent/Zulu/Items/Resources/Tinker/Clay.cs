using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class Clay : Item
    {
        public override string DefaultName => "block of clay";

        public override double DefaultWeight => 0.1;

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
        
        [Constructible]
        public Clay() : this(1)
        {
        }
        
        [Constructible]
        public Clay(int amount) : base(0x1BE9)
        {
            Stackable = true;
            Amount = amount;
            Hue = 0x609;
        }

        public Clay(Serial serial) : base(serial)
        {
        }
    }
}