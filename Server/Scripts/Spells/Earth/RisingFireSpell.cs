using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Earth
{
    public class RisingFireSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Rising Fire", "Batida Do Fogo",
							233, 9012,
							Reagent.BatWing, Reagent.Brimstone, Reagent.VialOfBlood
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 100.0; } }
        public override int RequiredMana{ get{ return 15; } }

        public RisingFireSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget( this );
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

            if ( ! m.BeginAction( typeof( RisingFireSpell ) ) ) {
                goto Return;
            }

	    double range = 3.0;
	    if( Caster is PlayerMobile && ((PlayerMobile)Caster).Spec.SpecName == SpecName.Mage ){

		range *= ((PlayerMobile)Caster).Spec.Bonus;
	    }

	    List<Mobile> targets = new List<Mobile>();
	    Map map = Caster.Map;

	    if( map != null){
		foreach ( Mobile mob in Caster.GetMobilesInRange( (int)range )){
		    if ( Caster != mob &&
			 SpellHelper.ValidIndirectTarget( Caster, mob ) &&
			 Caster.CanBeHarmful( mob, false ) &&
			 Caster.InLOS( mob )){

			targets.Add(mob);
		    }
		}
	    }

	    Caster.PlaySound( 0x208 );

	    for( int i=0; i< targets.Count; i++){
		m = targets[i];

		//original spell deals something like (24*3)d5+(caster's magery / 5)*0.5 to all targets
		// for an average roll of 243 damage per anydice.com
		double dmg = Caster.Skills[DamageSkill].Value / 6.0;
		Caster.DoHarmful(m);
		m.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
		//m.Damage((int)dmg, Caster, DamageType.Fire);
		SpellHelper.Damage(this, TimeSpan.Zero, m, Caster, dmg, DamageType.Fire);
	    }

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
                m_Target.EndAction( typeof( RisingFireSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private RisingFireSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( RisingFireSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
