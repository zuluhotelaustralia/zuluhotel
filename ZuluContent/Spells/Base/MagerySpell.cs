using System;
using Server.Items;

namespace Server.Spells
{
    public abstract class MagerySpell : Spell
    {
        private const double ChanceOffset = 20.0, ChanceLength = 100.0 / 7.0;

        private static readonly int[] ManaTable = {4, 6, 9, 11, 14, 20, 40, 50};

        public MagerySpell(Mobile caster, Item spellItem = null) : base(caster, spellItem)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds((3 + (int) Circle) * CastDelaySecondsPerTick); }
        }

        public override int GetMana()
        {
            return SpellItem is BaseWand ? 0 : ManaTable[(int) Circle];
        }
        
        public override TimeSpan GetCastDelay()
        {
            if (SpellItem is BaseWand)
                return TimeSpan.Zero;

            return TimeSpan.FromSeconds(0.5 + 0.25 * (int) Circle);
        }
    }
}