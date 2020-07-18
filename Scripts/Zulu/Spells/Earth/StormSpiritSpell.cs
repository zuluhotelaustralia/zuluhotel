using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;

namespace RunZH.Scripts.Zulu.Spells.Earth
{
    public class StormSpiritSpell : AbstractEarthSpell
    {
        public override SpellInfo GetSpellInfo() => m_Info;

        private static SpellInfo m_Info = new SpellInfo(
                            "Storm Spirit", "Chame O Fogo Elemental",
                            269, 9010,
                            Reagent.FertileDirt, Reagent.VolcanicAsh, Reagent.BatWing
                            );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0); } }

        public override double RequiredSkill { get { return 120.0; } }
        public override int RequiredMana { get { return 20; } }

        public StormSpiritSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence())
            {
                goto Return;
            }

            if (Caster.Followers + 2 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049611); // get l10n rekt son
                goto Return;
            }

            TimeSpan duration = TimeSpan.FromSeconds(2 * Caster.Skills[DamageSkill].Fixed / 4);

            SpellHelper.Summon(new AirElementalLord(), Caster, 0x217, duration, false, false);

        Return:
            FinishSequence();
        }

    }
}
