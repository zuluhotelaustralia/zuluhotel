using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;
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
            Caster.Target = new MobileTarget( this, 10, TargetFlags.Beneficial );
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

            SpellHelper.Turn( Caster, m );

            double effectiveness = SpellHelper.GetEffectiveness( Caster );

            double duration = Caster.Skills[SkillName.Meditation].Value * 8;
	    if( Caster is PlayerMobile ){
		PlayerMobile pm = Caster as PlayerMobile;
		if( pm.Spec.SpecName == SpecName.Mage ){
		    duration *= 2;
		    duration *= pm.Spec.Bonus;
		}
	    }

	    TimeSpan durr = TimeSpan.FromSeconds( duration );

            double roll = 0.8 * effectiveness + 0.2 * Utility.RandomDouble();

            int str = (int)(25 * roll);
            int inte = (int)(25 * roll);
            int dex = (int)(25 * roll);

            SpellHelper.AddStatBonus( Caster, m, StatType.Str, str, durr);
            SpellHelper.AddStatBonus( Caster, m, StatType.Int, inte, durr);
            SpellHelper.AddStatBonus( Caster, m, StatType.Dex, dex, durr);

            // TODO: Find different sounds/effects?  These are copied from Bless
            m.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
            m.PlaySound( 0x1EA );

        Return:
            FinishSequence();
        }
    }
}
