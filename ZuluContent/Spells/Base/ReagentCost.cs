using System;

namespace Server.Spells
{
    public sealed class ReagentCost
    {
        public Type Reagent { get; init; }
        public int Amount { get; init; }
        
        public void Deconstruct(out Type reagent, out int amount)
        {
            reagent = Reagent;
            amount = Amount;
        }

        public ReagentCost(Type reagent, int amount)
        {
            Reagent = reagent;
            Amount = amount;
        }
    }
}