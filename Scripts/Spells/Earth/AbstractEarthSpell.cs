using System;

namespace Server.Spells.Earth
{
    public abstract class AbstractEarthSpell : Spell
    {
        public abstract double RequiredSkill{ get; }
        public abstract int RequiredMana{ get; }

	public override SpellCircle Circle { get { return SpellCircle.Earth; } }


        public AbstractEarthSpell(Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
        {
        }

        public override int GetMana()
        {
            return RequiredMana;
        }
    }
}
