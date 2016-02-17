using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
    public class PlagueSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Plague", "Fluctus Puter Se Aresceret",
							227, 9031,
							Reagent.VolcanicAsh, Reagent.BatWing, Reagent.DaemonBone, Reagent.DragonsBlood,
							Reagent.Bloodspawn, Reagent.Pumice, Reagent.SerpentsScales
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 0.0; } }
        public override int RequiredMana{ get{ return 0; } }

        public PlagueSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

            if ( ! m.BeginAction( typeof( PlagueSpell ) ) ) {
                goto Return;
            }

            SpellHelper.Turn( Caster, m );

            // TODO: Spell graphical and sound effects.

            Caster.DoHarmful( m );

            // TODO: Spell action ( buff/debuff/damage/etc. )
	    //see Server/Poison.cs and Scripts/Misc/Poison.cs
	    // instantiate a new poisontimer
	    // probably use level 4 (lethal) irrespective of range
	    // "level 5" non existent will surely cause fucked-upness due to
	    // shitbirds doing arithmetic with cliloc indices

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
                m_Target.EndAction( typeof( PlagueSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private PlagueSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( PlagueSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
