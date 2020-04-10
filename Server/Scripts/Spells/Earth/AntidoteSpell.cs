using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class AntidoteSpell : AbstractEarthSpell, IMobileTargeted
    {
        // Original zuluhotel functionality
        // cure poisons no matter what
        // leave target with poison immunity for duration based on skill

        // Current functionality:
        // Cures poison more or less 100% chance
        // Planned to add poison resistance when we add a zulu based prots system

        private static SpellInfo m_Info = new SpellInfo(
                "Antidote", "Puissante Terre Traite Ce Patient",
                212, 9061,
                typeof ( DeadWood ),
                typeof ( FertileDirt ),
                typeof ( ExecutionersCap ));

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 60.0; } }
        public override int RequiredMana{ get{ return 5; } }

        public AntidoteSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            Caster.Target = new MobileTarget( this, 10, TargetFlags.Beneficial );
        }

        public void OnTargetFinished( Mobile from ) {
            FinishSequence();
        }

        public void OnTarget( Mobile from, Mobile m ) {
            if ( ! Caster.CanSee( m ) ) {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
                goto Return;
            }

            if ( ! CheckBSequence( m ) ) {
                goto Return;
            }

	    SpellHelper.Turn( Caster, m );

            Poison p = m.Poison;

            if ( p != null )
            {
                if ( m.CurePoison( Caster ) )
                {
                    if ( Caster != m )
                        Caster.SendLocalizedMessage( 1010058 ); // You have cured the target of all poisons!

                    m.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
                }
            }


            // TODO: Effects stolen from Cure spell, we may want
            // different ones, or at least a different sound
            // effect,
            m.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
            m.PlaySound( 0x1E0 );

        Return:
            FinishSequence();
        }
    }
}
