using System;
using Server.Items;

namespace Server.Spells
{
    public abstract class MagerySpell : Spell
    {
        public MagerySpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
    }
}