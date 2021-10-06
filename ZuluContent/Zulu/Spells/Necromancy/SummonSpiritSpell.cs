using System;
using System.Threading.Tasks;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SummonSpiritSpell : NecromancerSpell, IAsyncSpell
    {
        public SummonSpiritSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task CastAsync()
        {
            var bonus = Caster.GetClassModifier(SkillName.Magery) > 1.0 ? 2 : 0;
            var amount = Utility.Dice(2, 2, bonus);

            for (var i = 0; i < amount; i++)
            {
                var choice = Utility.Dice(1, 8, bonus);

                BaseCreature creature = choice switch
                {
                    <= 4 => "Shade",
                    <= 7 => "Liche",
                    <= 9 => "LicheLord",
                    _ => "Dracoliche",
                };

                SpellHelper.Summon(creature, Caster, 0x22A, null, false);
            }
        }
    }
}