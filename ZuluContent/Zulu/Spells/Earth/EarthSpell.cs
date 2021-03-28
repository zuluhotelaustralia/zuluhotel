using System;
using Server;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
    public abstract class EarthSpell : Spell
    {
        public abstract double RequiredSkill { get; }
        public abstract int RequiredMana { get; }


        public override SkillName CastSkill
        {
            get { return SkillName.Magery; }
        }

        public override SkillName DamageSkill
        {
            get { return SkillName.Magery; }
        }

        public override TimeSpan GetCastDelay()
        {
            return TimeSpan.FromSeconds(2.0);
        }

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(5); }
        }

        public EarthSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override int GetMana()
        {
            return RequiredMana;
        }

        public override void GetCastSkills(out double min, out double max)
        {
            // TODO: Pick a good max skill value
            min = RequiredSkill;
            max = RequiredSkill + 40;

            if (max > 150) max = 150;
        }
    }
}