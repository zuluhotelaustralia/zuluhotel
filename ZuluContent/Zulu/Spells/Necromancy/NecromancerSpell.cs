using System;
using Server;
using Server.Engines.Magic;
using Server.Items;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
    public abstract class NecromancerSpell : Spell
    {
        public NecromancerSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
    }
}