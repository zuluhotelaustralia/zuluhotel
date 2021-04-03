using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class RawGlass : Item
    {
        public override string DefaultName => "raw glass";

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
        public RawGlass() : this(1)
        {
        }
        
        [Constructible]
        public RawGlass(int amount) : base(0x1BF5)
        {
            Stackable = true;
            Amount = amount;
            Hue = 0x973;
        }

        public RawGlass(Serial serial) : base(serial)
        {
        }
    }
}