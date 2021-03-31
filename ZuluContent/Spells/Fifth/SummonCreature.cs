using System;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Spells.Fifth
{
    public class SummonCreatureSpell : MagerySpell, IAsyncSpell
    {
        // NOTE: Creature list based on 1hr of summon/release on OSI.
        private static readonly Type[] Types =
        {
            typeof(PolarBear),
            typeof(GrizzlyBear),
            typeof(BlackBear),
            typeof(Horse),
            typeof(Walrus),
            typeof(Chicken),
            typeof(GiantScorpion),
            typeof(GiantSerpent),
            typeof(Llama),
            typeof(Alligator),
            typeof(GreyWolf),
            typeof(Slime),
            typeof(Eagle),
            typeof(Gorilla),
            typeof(SnowLeopard),
            typeof(Pig),
            typeof(Hind),
            typeof(Rabbit)
        };

        public SummonCreatureSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task CastAsync()
        {
            var creature = (BaseCreature) Activator.CreateInstance(Types[Utility.Random(Types.Length)]);
            
            if (creature != null) 
                SpellHelper.Summon(creature, Caster, 0x215);
        }
    }
}