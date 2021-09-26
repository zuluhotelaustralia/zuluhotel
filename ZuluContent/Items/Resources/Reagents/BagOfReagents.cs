using System;
using Server.Spells;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class BagOfReagents : Bag
    {
        [Constructible]
        public BagOfReagents() : this(50)
        {
        }


        [Constructible]
        public BagOfReagents(int amount)
        {
            foreach (var t in Reagent.NormalReagents)
            {
                var reg = (BaseReagent) Activator.CreateInstance(t);
                if (reg != null)
                {
                    reg.Amount = amount;
                    DropItem(reg);
                }
            }
        }
    }
}