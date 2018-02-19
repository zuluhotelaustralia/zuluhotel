using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class EarthSpiritSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Earth Spirit", "Chame O Terra Elemental",
							269, 9010,
							Reagent.DragonsBlood, Reagent.FertileDirt, Reagent.VolcanicAsh
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 120.0; } }
        public override int RequiredMana{ get{ return 20; } }

        public EarthSpiritSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
	    if ( ! CheckSequence() )
            {
                goto Return;
            }

            TimeSpan duration = TimeSpan.FromSeconds( (2 * Caster.Skills[DamageSkill].Fixed) / 4 );

	    SpellHelper.Summon( new EarthElementalLord(), Caster, 0x217, duration, false, false );

        Return:
            FinishSequence();
        }

    }
}
