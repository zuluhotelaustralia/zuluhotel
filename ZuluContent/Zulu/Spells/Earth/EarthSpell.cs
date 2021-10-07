using System;
using Server;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
    public abstract class EarthSpell : Spell
    {
        public EarthSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
    }
}