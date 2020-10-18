using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Scripts.Zulu.Engines.Classes;
using Server;
using Server.Targeting;
using Server.Spells;

namespace Scripts.Zulu.Spells.Earth
{
    public class EarthsBlessingSpell : AbstractEarthSpell, IMobileTargeted
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(2); }
        }

        public override double RequiredSkill
        {
            get { return 60.0; }
        }

        public override int RequiredMana
        {
            get { return 10; }
        }

        public EarthsBlessingSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new MobileTarget(this, 10, TargetFlags.Beneficial);
        }

        public void OnTargetFinished(Mobile from)
        {
            FinishSequence();
        }

        public void OnTarget(Mobile from, Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
                goto Return;
            }

            if (!CheckBSequence(m)) goto Return;

            SpellHelper.Turn(Caster, m);

            var effectiveness = SpellHelper.GetEffectiveness(Caster);

            var duration = Caster.Skills[SkillName.Meditation].Value * 8;
            if (Spec.GetSpec(Caster).SpecName == SpecName.Mage)
            {
                duration *= 2;
                duration *= Spec.GetSpec(Caster).Bonus;
            }

            var durr = TimeSpan.FromSeconds(duration);

            var roll = 0.8 * effectiveness + 0.2 * Utility.RandomDouble();

            var str = (int) (25 * roll);
            var inte = (int) (25 * roll);
            var dex = (int) (25 * roll);

            SpellHelper.AddStatBonus(Caster, m, StatType.Str, str, durr);
            SpellHelper.AddStatBonus(Caster, m, StatType.Int, inte, durr);
            SpellHelper.AddStatBonus(Caster, m, StatType.Dex, dex, durr);

            // TODO: Find different sounds/effects?  These are copied from Bless
            m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
            m.PlaySound(0x1EA);

            Return:
            FinishSequence();
        }
    }
}