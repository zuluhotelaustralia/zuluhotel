using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Spells;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class SummonSpiritSpell : NecromancerSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(4); }
        }

        public override double RequiredSkill
        {
            get { return 120.0; }
        }

        public override int RequiredMana
        {
            get { return 100; }
        }

        public SummonSpiritSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence()) goto Return;

            var bonus = 0;

            if (Spec.GetSpec(Caster).SpecName == SpecName.Mage) bonus = 1;

            var amount = Utility.Dice(2, 2, bonus);
            Type toSummon;

            while (amount > 0)
            {
                var choice = Utility.Dice(1, 8, bonus);

                if (choice <= 4)
                    toSummon = typeof(Shade);
                else if (choice <= 7)
                    toSummon = typeof(Liche);
                else if (choice <= 9)
                    toSummon = typeof(LicheLord);
                else
                    toSummon = typeof(Dracoliche);

                var duration = TimeSpan.FromSeconds((int) Caster.Skills[DamageSkill].Value);
                var creature = (BaseCreature) Activator.CreateInstance(toSummon);
                SpellHelper.Summon(creature, Caster, 0x215, duration, false, false);

                amount--;
            }

            Return:
            FinishSequence();
        }
    }
}