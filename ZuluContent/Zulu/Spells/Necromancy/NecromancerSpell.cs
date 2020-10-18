using System;
using Server;
using Server.Engines.Magic;
using Server.Items;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
    public abstract class NecromancerSpell : Spell
    {
        public abstract double RequiredSkill { get; }
        public abstract int RequiredMana { get; }

        public override TimeSpan GetCastDelay()
        {
            return TimeSpan.FromSeconds(2.0);
        }

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(5); }
        }

        public override SkillName CastSkill
        {
            get { return SkillName.Magery; }
        }

        public override SkillName DamageSkill
        {
            get { return SkillName.SpiritSpeak; }
        }

        //public override int CastDelayBase{ get{ return base.CastDelayBase; } } // Reference, 3

        public override bool ClearHandsOnCast
        {
            get { return false; }
        }

        public override double CastDelayFastScalar
        {
            get { return 0; }
        } // Necromancer spells are not affected by fast cast items, though they are by fast cast recovery

        public NecromancerSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override int ComputeKarmaAward()
        {
            //TODO: Verify this formula being that Necro spells don't HAVE a circle.
            //int karma = -(70 + (10 * (int)Circle));
            var karma = -(40 + (int) (10 * (CastDelayBase.TotalSeconds / CastDelaySecondsPerTick)));


            return karma;
        }

        public override void GetCastSkills(out double min, out double max)
        {
            min = RequiredSkill;
            max = Scroll != null ? min : RequiredSkill + 40.0;

            if (max > 150) max = 150;
        }

        public override int GetMana()
        {
            return RequiredMana;
        }
    }
}