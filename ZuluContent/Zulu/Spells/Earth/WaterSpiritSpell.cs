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
    public class WaterSpiritSpell : EarthSpell, IAsyncSpell
    {
        public WaterSpiritSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            SpellHelper.Summon(Creatures.Create("WaterElementalLord"), Caster, 0x217);
        }
    }
}