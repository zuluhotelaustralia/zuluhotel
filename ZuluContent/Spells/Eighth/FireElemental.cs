using System;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
    public class FireElementalSpell : MagerySpell
    {
        public FireElementalSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if (Caster.Followers + 4 > Caster.FollowersMax)
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

                SpellHelper.Summon(new FireElemental(), Caster, 0x217, duration, false, false);
            }

            FinishSequence();
        }
    }
}