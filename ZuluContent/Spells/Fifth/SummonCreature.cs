using System;
using Server.Mobiles;

namespace Server.Spells.Fifth
{
    public class SummonCreatureSpell : MagerySpell
    {
        // NOTE: Creature list based on 1hr of summon/release on OSI.

        private static readonly Type[] m_Types =
        {
            typeof(PolarBear),
            typeof(GrizzlyBear),
            typeof(BlackBear),
            typeof(Horse),
            typeof(Walrus),
            typeof(Chicken),
            typeof(GiantScorpion),
            typeof(GiantSerpent),
            typeof(Llama),
            typeof(Alligator),
            typeof(GreyWolf),
            typeof(Slime),
            typeof(Eagle),
            typeof(Gorilla),
            typeof(SnowLeopard),
            typeof(Pig),
            typeof(Hind),
            typeof(Rabbit)
        };

        public SummonCreatureSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }
        
        public override void OnCast()
        {
            if (CheckSequence())
                try
                {
                    var creature = (BaseCreature) Activator.CreateInstance(m_Types[Utility.Random(m_Types.Length)]);

                    var duration = TimeSpan.FromSeconds(4.0 * Caster.Skills[SkillName.Magery].Value);

                    SpellHelper.Summon(creature, Caster, 0x215, duration, false, false);
                }
                catch
                {
                }

            FinishSequence();
        }

        public override TimeSpan GetCastDelay()
        {
            return base.GetCastDelay() + TimeSpan.FromSeconds(6.0);
        }
    }
}