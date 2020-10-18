using System;
using Server.Items;

namespace Server.Spells
{
    public abstract class MagerySpell : Spell
    {
        private const double ChanceOffset = 20.0, ChanceLength = 100.0 / 7.0;

        private static readonly int[] ManaTable = {4, 6, 9, 11, 14, 20, 40, 50};

        public MagerySpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds((3 + (int) Circle) * CastDelaySecondsPerTick); }
        }


        public override void GetCastSkills(out double min, out double max)
        {
            var circle = (int) Circle;

            if (Scroll != null)
                circle -= 2;

            var avg = ChanceLength * circle;

            min = avg - ChanceOffset;
            max = avg + ChanceOffset;
        }

        public override int GetMana()
        {
            return Scroll is BaseWand ? 0 : ManaTable[(int) Circle];
        }

        public override double GetResistSkill(Mobile m)
        {
            var maxSkill = (1 + (int) Circle) * 10;
            maxSkill += (1 + (int) Circle / 6) * 25;

            if (m.Skills[SkillName.MagicResist].Value < maxSkill)
                m.CheckSkill(SkillName.MagicResist, 0.0, m.Skills[SkillName.MagicResist].Cap);

            return m.Skills[SkillName.MagicResist].Value;
        }

        public override TimeSpan GetCastDelay()
        {
            if (Scroll is BaseWand)
                return TimeSpan.Zero;

            return TimeSpan.FromSeconds(0.5 + 0.25 * (int) Circle);
        }
    }
}