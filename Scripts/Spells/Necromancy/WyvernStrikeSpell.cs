using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
    public class WyvernStrikeSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Wyvern Strike", "Ubrae Tenebrae Venarent"
							227, 9031,
							Reagent.DragonsBlood, Reagent.SerpentsScales,
							Reagent.Blackmoor, Reagent.Bloodspawn, Reagent.VolcanicAsh);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 0.0; } }
        public override int RequiredMana{ get{ return 0; } }

        public WyvernStrikeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

            m.FixedParticles( 0x3709, 10, 15, 5021, EffectLayer.Waist );
	    m.PlaySound(0x1e2);

            Caster.DoHarmful( m );

	    int level = 0;
	    double pStr = Caster.Skills[CastSkill].Value;
	   
	    if (pStr > 100){
		level = 1;
	    }
	    else if (pStr > 110){
		level = 2;
	    }
	    else if (pStr > 130){
		level = 3;
	    }
	    else if (pStr > 140){
		level = 4;
	    }
	    else {
		level = 0;
	    }
			
	    double ss = Caster.Skills[DamageSkill].Value;
	    int bonus = (int)ss / 4;
	    
	    double dmg = (double)Utility.Dice(3, 5, bonus);
	    dmg /= 2; //necessary?  

	    //we really should put this idiom into SpellHelper, I'm fucking tired of writing it --sith
	    if( Caster is PlayerMobile && ((PlayerMobile)Caster).Spec.SpecName == SpecName.Mage ){
		dmg *= ((PlayerMobile)Caster).Spec.Bonus;
	    }

	    //sith: change this, see issue tracker on gitlab
	    if ( CheckResisted( m ) )
	    {
		dmg *= 0.75;
		
		m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
	    }

	    m.Damage((int)dmg, m, DamageType.Necro);
	    m.ApplyPoison( Caster, Poison.GetPoison( level ) );
            
        Return:
            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private WyvernStrikeSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( WyvernStrikeSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
