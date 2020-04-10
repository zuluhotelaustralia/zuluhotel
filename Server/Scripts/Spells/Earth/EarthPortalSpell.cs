using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class EarthPortalSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Earth Portal", "Destraves Limites Da Natureza",
							263, 9032,
							Reagent.Brimstone,
							Reagent.ExecutionersCap,
							Reagent.EyeOfNewt
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 80.0; } }
        public override int RequiredMana{ get{ return 10; } }

        public EarthPortalSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
	    // don't bother with the rest of this shit if the seq is bad
            if( CheckSequence() ){
		if( Caster is PlayerMobile ){
		    Point3D origin = new Point3D( 0, 0, 0 );
		    PlayerMobile pmCaster = Caster as PlayerMobile;

		    // they don't have a stored recall location, so set one.
		    if( pmCaster.EarthPortalLocation == origin ){
			if( SpellHelper.CheckTravel( pmCaster, TravelCheckType.Mark ) ){
			    pmCaster.EarthPortalLocation = new Point3D( pmCaster.X, pmCaster.Y, pmCaster.Z );
			    Caster.PlaySound( 0x1FA );
			    Effects.SendLocationEffect( Caster, Caster.Map, 14201, 16 );
			    Caster.SendMessage("The spirits of the land agree to assist you, and you feel their minds touch your own.");
			}
			else{
			    Caster.SendMessage("The spirits of the land do not answer your call.");
			}
		    }
		    else{
			//if we're here then Mobile.EarthPortalLocation must be non-null, so
			if( SpellHelper.CheckTravel( pmCaster, TravelCheckType.RecallFrom ) && Caster.Map == Map.Felucca ){
			    BaseCreature.TeleportPets( Caster, pmCaster.EarthPortalLocation, Caster.Map, true );
			    Caster.PlaySound( 0x1FC );
			    Caster.MoveToWorld( pmCaster.EarthPortalLocation, Caster.Map );
			    Caster.PlaySound( 0x1FC );
			    Caster.SendMessage("You thank the spirits of the land for their assistance, and you no longer feel their touch on your mind.");
			    pmCaster.EarthPortalLocation = origin;
			}
			else{
			    Caster.SendMessage("The spirits of the land do not answer your call.");
			}
		    }
		}
	    }
	    
	    FinishSequence();
        }

        public void Target( Mobile m )
        {
            if ( ! Caster.CanSee( m ) )
            {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
                goto Return;
            }

            if ( ! CheckSequence() )
            {
                goto Return;
            }

	    SpellHelper.Turn( Caster, m );

            // TODO: Spell graphical and sound effects.

            Caster.DoHarmful( m );

            // TODO: Spell action ( buff/debuff/damage/etc. )

            new InternalTimer( m, Caster ).Start();

        Return:
            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Target;

            public InternalTimer( Mobile target, Mobile caster ) : base( TimeSpan.FromSeconds( 0 ) )
            {
                m_Target = target;

                // TODO: Compute a reasonable duration, this is stolen from ArchProtection
                double time = caster.Skills[SkillName.Magery].Value * 1.2;
                if ( time > 144 )
                    time = 144;
                Delay = TimeSpan.FromSeconds( time );
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Target.EndAction( typeof( EarthPortalSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private EarthPortalSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( EarthPortalSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
            {
                m_Owner = owner;
            }

            protected override void OnTarget( Mobile from, object o )
            {
                if ( o is Mobile )
                    m_Owner.Target( (Mobile) o );
            }

            protected override void OnTargetFinish( Mobile from )
            {
                m_Owner.FinishSequence();
            }
        }

    }
}
