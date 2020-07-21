using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
    public class BagOfReagents : Bag
    {

        [Constructible]
public BagOfReagents() : this(50)
        {
        }


        [Constructible]
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

        [Constructible]
public BagOfReagents(Serial serial) : base(serial)
        {
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
    }
}
