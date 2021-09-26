using System;
using Server.Spells;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class BagOfAllReagents : Bag
    {
        [Constructible]
        public BagOfAllReagents() : this(50)
        {
        }


        [Constructible]
        public BagOfAllReagents(int amount)
        {
            foreach (var t in Reagent.AllReagents)
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