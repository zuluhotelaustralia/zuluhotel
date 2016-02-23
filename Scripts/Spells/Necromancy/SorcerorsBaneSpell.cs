using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
    public class SorcerorsBaneSpell : NecromancerSpell
    {
	protected const int waterfallEW = 13591; //0x3517
	protected const int waterfallNS = 13561; //0x34f9

	//in hex:                              {17a6, 17a3, 179f, 17a8, 1797, 17a5, 17a1, 179d, 17a7}
	protected const int[] pool = new int[] {6054, 6051, 6047, 6056, 6039, 6053, 6049, 6045, 6055};

	//0,0 is the north-west corner, so:
	/*
	  {           x--y--, 
	          x--y,     xy--,
	    x--y++,     xy,     x++y--,
                  xy++,     x++y, 
                      x++y++            }
	 */

	
	private static SpellInfo m_Info = new SpellInfo(
							"Sorceror's Bane", "Fluctus Perturbo Magus Navitas"
							227, 9031,
							Reagent.VolcanicAsh, Reagent.WyrmsHeart, Reagent.DaemonBone,
							Reagent.Pumice, Reagent.DragonsBlood, Reagent.DeadWood
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 0.0; } }
        public override int RequiredMana{ get{ return 0; } }

        public SorcerorsBaneSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget( this );
        }

        public void Target( Mobile m )
        {
	    // this is the classic "waterfall spell"
            // as usual, in zhf this spell does 3d5 + ( magery / 4 ) which is then divided by 2 and
	    //then multiplied by your class bonus.  so, for a spec 4 mage @ 10%/level and 130 magery
	    //it looks like an average of 20.5*1.4 = ~29 damage
	    // however there is a cap of (spellcircle * (13+spellcircle))
	    // zulu being overcomplicated means for single-target spells you subtract 3 from the circle.
	    // why?  because.
	    // so the cap on damage in this case ends up being 20*33=660 because that's a reasonable cap amirite :^)
	    // that first half damage is dealt as elemental fire because everyone knows water is fire.
	    //THEN, it attempts to "deal" (targets_mana * casters_class_bonus) amount of damage,
	    //modified by both caster's and target's spec bonuses and target's resist skill, "damage"
	    //is then floored and stolen from the target's mana pool and given to the caster
	    //THEN it applies the other half of the real damage from the dice roll as elemental water.

	    //What the literal fuck.
	    
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

            if ( ! m.BeginAction( typeof( SorcerorsBaneSpell ) ) ) {
                goto Return;
            }

            SpellHelper.Turn( Caster, m );

            m.PlaySound( 0x209 ); //sorc's bane sound
	    //play 0x218 on the target when you steal the mana

            Caster.DoHarmful( m );

            new WaterfallTimer( m, Caster ).Start(); //this timer should wait half a second and then create the waterfall and the pool
	    //it should then play the sound 0x11 on the caster
	    //it should then start a second timer(s) to clean up the waterfall after 5 seconds and the pool at the base 5 seconds after that

        Return:
            FinishSequence();
        }

        private class WaterfallTimer : Timer
        {
            private Mobile m_Target;

            public WaterfalllTimer( Mobile target, Mobile caster ) : base( TimeSpan.FromSeconds( 0 ) )
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
                m_Target.EndAction( typeof( SorcerorsBaneSpell ) );
            }
        }

        private class InternalTarget : Target
        {
            private SorcerorsBaneSpell m_Owner;

            // TODO: What is thie Core.ML stuff, is it needed?
            public InternalTarget( SorcerorsBaneSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
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
