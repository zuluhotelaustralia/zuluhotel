using System;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class FireElementalSpell : MagerySpell, IAsyncSpell
    {
        public FireElementalSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            SpellHelper.Summon(new FireElemental(), Caster, 0x217);
        }
    }
}