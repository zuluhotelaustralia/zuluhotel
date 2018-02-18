using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class GustOfAirSpell : AbstractEarthSpell, IMobileTargeted
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Gust Of Air", "Gusto Do Ar",
                230,
                9022,
                typeof( Batwing ),
                typeof( Brimstone ),
                typeof( VialOfBlood ));


        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 100.0; } }
        public override int RequiredMana{ get{ return 15; } }

        public GustOfAirSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            Caster.Target = new MobileTarget( this, 10, Caster, TargetFlags.Harmful );
        }

        public override void OnTargetFinished() {
            FinishSequence();
        }

        public override void OnTarget( Mobile from, Mobile m )
        {
            if ( ! Caster.CanSee( m ) )
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
                goto Return;
            }

            if ( ! CheckHSequence( m ) )
            {
                goto Return;
            }

            if ( ! m.BeginAction( typeof( GustOfAirSpell ) ) ) {
                goto Return;
            }

            SpellHelper.Turn( Caster, m );

            // TODO: Push player in random direction
            // TODO: Damage.

        Return:
            FinishSequence();
        }
    }
}
