using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class CallLightningSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Call Lightning", "Batida Do Deus",
							236, 9031,
							Reagent.WyrmsHeart, Reagent.PigIron, Reagent.Bone
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 80.0; } }
        public override int RequiredMana{ get{ return 10; } }

        public CallLightningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

            if ( ! m.BeginAction( typeof( CallLightningSpell ) ) ) {
                goto Return;
            }

            SpellHelper.Turn( Caster, m );

	    m.BoltEffect(0); //argument is hue of the bolt
	    m.BoltEffect(0);

	    int dmg = Utility.Dice( (int)(Caster.Skills[DamageSkill].Value / 15.0), 5, 0); //caps around 24 damage at 130 skill
	    m.Damage((int)dmg, Caster, DamageType.Air);
	    
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
                m_Target.EndAction( typeof( CallLightningSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private CallLightningSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( CallLightningSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
