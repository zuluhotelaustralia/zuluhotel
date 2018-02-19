using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Earth
{
    public class EarthsBlessingSpell : AbstractEarthSpell, IMobileTargeted
    {
        private static SpellInfo m_Info = new SpellInfo("Earths Blessing", "Foria Da Terra",
                203,
                9061,
                typeof( PigIron ),
                typeof( Obsidian ),
                typeof( VolcanicAsh ));

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2 ); } }

        public override double RequiredSkill{ get{ return 60.0; } }
        public override int RequiredMana{ get{ return 10; } }

        public EarthsBlessingSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            Caster.Target = new MobileTarget( this, 10, Caster, TargetFlags.Beneficial );
        }

        public void OnTargetFinished( Mobile from ) {
            FinishSequence();
        }

        public void OnTarget( Mobile from, Mobile m )
        {
            if ( ! Caster.CanSee( m ) ) {
                // Seems like this should be responsibility of the targetting system.  --daleron
                Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
                goto Return;
            }

            if ( ! CheckBSequence( m ) ) {
                goto Return;
            }

            if ( ! m.BeginAction( typeof( EarthsBlessingSpell ) ) ) {
                goto Return;
            }

            SpellHelper.Turn( Caster, m );

            double effectiveness = SpellHelper.GetEffectiveness( Caster );

            TimeSpan duration = SpellHelper.GetDuration( Caster, null );

            double roll = 0.5 * effectiveness + 0.5 * Utility.RandomDouble();

            int str = 15 * roll;
            int inte = 15 * roll;
            int dex = 15 * roll;

            SpellHelper.AddStatBonus( Caster, m, StatType.Str, str, duration);
            SpellHelper.AddStatBonus( Caster, m, StatType.Int, inte, duration);
            SpellHelper.AddStatBonus( Caster, m, StatType.Dex, dex, duration);

            // TODO: Find different sounds/effects?  These are copied from Bless
            m.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
            m.PlaySound( 0x1EA );

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
                m_Target.EndAction( typeof( EarthsBlessingSpell ) );
            }
        }
    }
}
