using System;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class SummonDaemonSpell : MagerySpell, IAsyncSpell
    {
        public SummonDaemonSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            SpellHelper.Summon(new Daemon(), Caster, 0x216);
        }
    }
}