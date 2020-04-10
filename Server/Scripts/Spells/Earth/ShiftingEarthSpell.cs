using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

//earth damage on single target, dex debuff
namespace Server.Spells.Earth
{
    public class ShiftingEarthSpell : AbstractEarthSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Shifting Earth", "Esmagamento Con Pedra",
							236, 9031,
							Reagent.FertileDirt, Reagent.Obsidian, Reagent.DeadWood
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 60.0; } }
        public override int RequiredMana{ get{ return 5; } }

        public ShiftingEarthSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

	    SpellHelper.Turn( Caster, m );

	    m.FixedParticles( 0x3709, 10, 15, 5021, EffectLayer.Waist ); //probably wrong particles ID
	    m.PlaySound(0x20e);

            Caster.DoHarmful( m );

	    //yeah lots of casting is ugly but... fuck it :^)
	    double dmg = (double)Utility.Dice( (int)(Caster.Skills[DamageSkill].Value / 15.0), 5, 0); //caps around 20 damage at 130 skill

	    if (CheckResisted(m)){
		dmg *= 0.75;

		m.SendLocalizedMessage( 501783 );
	    }

	    //m.Damage((int)dmg, Caster, DamageType.Earth);
	    SpellHelper.Damage(this, TimeSpan.Zero, m, Caster, dmg, DamageType.Earth);

	    SpellHelper.AddStatCurse( Caster, m, StatType.Dex );
	    int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, m, true )*100);
	    TimeSpan length = SpellHelper.GetDuration( Caster, m );

	    BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Clumsy, 1075831, length, m, percentage.ToString() ) );

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
                m_Target.EndAction( typeof( ShiftingEarthSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private ShiftingEarthSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( ShiftingEarthSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
