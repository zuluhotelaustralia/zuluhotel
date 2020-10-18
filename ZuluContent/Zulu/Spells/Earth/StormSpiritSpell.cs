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
    public class StormSpiritSpell : AbstractEarthSpell
    {
        public override TimeSpan CastDelayBase
        {
            get { return TimeSpan.FromSeconds(0); }
        }

        public override double RequiredSkill
        {
            get { return 120.0; }
        }

        public override int RequiredMana
        {
            get { return 20; }
        }

        public StormSpiritSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence()) goto Return;

            if (Caster.Followers + 2 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049611); // get l10n rekt son
                goto Return;
            }

            var duration = TimeSpan.FromSeconds(2 * Caster.Skills[DamageSkill].Fixed / 4);

            SpellHelper.Summon(new AirElementalLord(), Caster, 0x217, duration, false, false);

            Return:
            FinishSequence();
        }
    }
}