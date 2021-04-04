using System;
using System.Collections;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Utilities;

namespace Scripts.Zulu.Spells.Earth
{
    public class SummonMammalsSpell : EarthSpell, IAsyncSpell
    {
        public SummonMammalsSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task CastAsync()
        {
            var count = Utility.RandomMinMax(1, 2);
            var bonus = 0;
            
            if (Caster.GetClassModifier(SkillName.Magery) > 1.0)
            {
                count += 1;
                bonus = 1;
            }
            
            for (var i = 0; i < count; i++)
            {
                var roll = Utility.RandomMinMax(1, 8) + bonus;
                var mammal = roll switch
                {
                    1 => typeof(TimberWolf),
                    2 => typeof(GreyWolf),
                    >= 3 and < 5 => typeof(Horse),
                    5 => typeof(Cougar),
                    6 => typeof(Panther),
                    7 => typeof(BrownBear),
                    8 => typeof(GrizzlyBear),
                    9 => typeof(ForestOstard),
                    _ => typeof(TimberWolf)
                };

                var creature = mammal.CreateInstance<BaseCreature>();

                SpellHelper.Summon(creature, Caster, 0x217);
            }
        }
    }
}