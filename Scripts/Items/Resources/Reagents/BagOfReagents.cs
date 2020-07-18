using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
    public class BagOfReagents : Bag
    {
        [Constructable]
        public BagOfReagents() : this(50)
        {
        }

        [Constructable]
        public BagOfReagents(int amount)
        {
            foreach(var t in Reagent.Types)
            {
                var reg = (BaseReagent) Activator.CreateInstance(t);
                if (reg != null)
                {
                    reg.Amount = amount;
                    DropItem(reg);
                }
            }
        }

        public BagOfReagents(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}