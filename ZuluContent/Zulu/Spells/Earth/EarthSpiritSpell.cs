using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
    public class EarthSpiritSpell : EarthSpell, IAsyncSpell
    {
        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(0);
    
        public override double RequiredSkill => 60.0;
    
        public override int RequiredMana => 5;

        public EarthSpiritSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public async Task CastAsync()
        {
            SpellHelper.Summon(new EarthElementalLord(), Caster, 0x217);
        }
    }
}