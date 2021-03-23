using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
    public class SummonMammalsSpell : AbstractEarthSpell
    {
        private static Type[] m_Mammals =
        {
            typeof(GreyWolf),
            typeof(TimberWolf),
            typeof(Horse),
            typeof(Cougar),
            typeof(Panther),
            typeof(BrownBear),
            typeof(GrizzlyBear),
            typeof(ForestOstard)
        };

        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(4); }
        }

        public override double RequiredSkill
        {
            get { return 60.0; }
        }

        public override int RequiredMana
        {
            get { return 5; }
        }

        public SummonMammalsSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence()) goto Return;

            var effectiveness = SpellHelper.GetEffectiveness(Caster);

            var count = (int) (3 * effectiveness);

            // TODO: Weight higher up mammals more if skill/effectiveness is higher.

            for (var i = 0; i < count; i++)
            {
                var roll = 0.8 * Utility.RandomDouble() + 0.2 * effectiveness;
                var mammal = (int) Math.Min(m_Mammals.Length - 1,
                    Math.Floor(m_Mammals.Length * roll));

                var creature = (BaseCreature) Activator.CreateInstance(m_Mammals[mammal]);
                var duration = TimeSpan.FromSeconds((int) (5 * 60 * effectiveness));

                SpellHelper.Summon(creature, Caster, 0x215, duration, false);
            }

            Return:
            FinishSequence();
        }
    }
}