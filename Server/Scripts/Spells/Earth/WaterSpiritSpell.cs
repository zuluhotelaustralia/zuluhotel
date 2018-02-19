using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class WaterSpiritSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Water Spirit", "Chame O Agua Elemental"
		269, 9010,
		Reagent.WyrmsHeart, Reagent.SerpentsScales, Reagent.EyeOfNewt
                );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 120.0; } }
        public override int RequiredMana{ get{ return 20; } }

        public WaterSpiritSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
	    if ( ! CheckSequence() )
            {
                goto Return;
            }

            TimeSpan duration = TimeSpan.FromSeconds( (2 * Caster.Skills[DamageSkill].Fixed) / 4 );

	    SpellHelper.Summon( new WaterElementalLord(), Caster, 0x217, duration, false, false );

        Return:
            FinishSequence();
        }

    }
}
