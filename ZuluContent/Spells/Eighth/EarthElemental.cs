using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class EarthElementalSpell : MagerySpell, IAsyncSpell
    {
        public EarthElementalSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            SpellHelper.Summon("EarthElemental", Caster, 0x217);
        }
    }
}