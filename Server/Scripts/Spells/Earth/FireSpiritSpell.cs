using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Earth
{
    public class FireSpiritSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                            "Fire Spirit", "Chame O Fogo Elemental",
                            269, 9010,
                            Reagent.EyeOfNewt, Reagent.Blackmoor, Reagent.Obsidian
                            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0); } }

        public override double RequiredSkill { get { return 120.0; } }
        public override int RequiredMana { get { return 20; } }

        public FireSpiritSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence())
            {
                goto Return;
            }

            if (Caster.Followers + 4 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049611); // stfu son
                goto Return;
            }

            TimeSpan duration = TimeSpan.FromSeconds((2 * Caster.Skills[DamageSkill].Fixed) / 4);

            SpellHelper.Summon(new FireElementalLord(), Caster, 0x217, duration, false, false);

        Return:
            FinishSequence();
        }

    }
}
