using System;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Spells.Fifth
{
    public class SummonCreatureSpell : MagerySpell, IAsyncSpell
    {
        // NOTE: Creature list based on 1hr of summon/release on OSI.
        private static readonly string[] Creatures =
        {
            "PolarBear",
            "GrizzlyBear",
            "BlackBear",
            "Horse",
            "Walrus",
            "Chicken",
            "GiantScorpion",
            "GiantSerpent",
            "Llama",
            "Alligator",
            "GreyWolf",
            "Slime",
            "Eagle",
            "Gorilla",
            "SnowLeopard",
            "Pig",
            "Hind",
            "Rabbit"
        };

        public SummonCreatureSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task CastAsync()
        {
            BaseCreature creature = Creatures[Utility.Random(Creatures.Length)];
            
            if (creature != null) 
                SpellHelper.Summon(creature, Caster, 0x215);
        }
    }
}