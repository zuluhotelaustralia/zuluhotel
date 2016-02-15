using System;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
    public class SpectresTouchSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Spectres Touch", "Enevare"
		227, 9031,
		Reagent.ExecutionersCap, Reagent.Brimstone, Reagent.DaemonBone
                );

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 0.0; } }
        public override int RequiredMana{ get{ return 0; } }

        public SpectresTouchSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            if ( ! CheckSequence() )
            {
                goto Return;
            }   
            List<Mobile> targets = new List<Mobile>();
	    Map map = Caster.Map;
	    if( map != null ){
		foreach( Mobile m in Caster.GetMobilesInRange( 1 + (int)(Caster.Skills[CastSkill].Value / 15.0)) ) {
		    if( Caster != m &&
			SpellHelper.ValidIndirectTarget(Caster, m) &&
			Caster.CanBeHarmful(m, false) &&
			Caster.InLOS(m)
			) {
			targets.Add(m);
		    }
		}
	    }
	    
	    Caster.PlaySound( 0x1F1 );
	    for(int i=0; i<targets.Count; i++) {
		Mobile m = targets[i];
		int dmg = 
            Caster.DoHarmful( m );

	    m.FixedParticles( 0x374A, 10, 15, 5013, EffectLayer.Waist );
	    
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
                m_Target.EndAction( typeof( SpectresTouchSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private SpectresTouchSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( SpectresTouchSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
