using System;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class SummonDaemonSpell : MagerySpell
    {
        public SummonDaemonSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if (Caster.Followers + 5 > Caster.FollowersMax)
            {
                Caster.SendLocalizedMessage(1049645); // You have too many followers to summon that creature.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                var duration = TimeSpan.FromSeconds(2 * Caster.Skills.Magery.Fixed / 5);

                SpellHelper.Summon(new Daemon(), Caster, 0x216, duration, false, false);
            }

            FinishSequence();
        }
    }
}