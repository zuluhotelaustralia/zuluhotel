using System;
using Server;
using Server.Engines.Magic;
using Server.Items;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
    public abstract class NecromancerSpell : Spell
    {
        public virtual double RequiredSkill { get; }
        public virtual int RequiredMana { get; }

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
            get { return SkillName.Magery; }
        }
        
        public override bool ClearHandsOnCast
        {
            get { return false; }
        }

        public override double CastDelayFastScalar
        {
            get { return 0; }
        } // Necromancer spells are not affected by fast cast items, though they are by fast cast recovery

        public NecromancerSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }
        
        public override int GetMana()
        {
            return RequiredMana;
        }
    }
}