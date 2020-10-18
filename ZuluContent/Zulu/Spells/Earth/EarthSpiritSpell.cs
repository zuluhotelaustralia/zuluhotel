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
    public class EarthSpiritSpell : AbstractEarthSpell
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

        public EarthSpiritSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }

        public override void OnCast()
        {
            if (!CheckSequence()) goto Return;

            if (Caster.Followers + 2 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049611); // just stop bro u can't even
                goto Return;
            }

            var duration = TimeSpan.FromSeconds(2 * Caster.Skills[DamageSkill].Fixed / 4);

            SpellHelper.Summon(new EarthElementalLord(), Caster, 0x217, duration, false, false);

            Return:
            FinishSequence();
        }
    }
}