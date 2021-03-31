using System;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class WaterElementalSpell : MagerySpell, IAsyncSpell
    {
        public WaterElementalSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            SpellHelper.Summon(new WaterElemental(), Caster, 0x217);
        }
    }
}