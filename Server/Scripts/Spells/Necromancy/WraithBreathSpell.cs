using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
    public class WraithBreathSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Wraith Breath", "Manes Sollicti Mi Compellere",
							227, 9031,
							Reagent.Pumice, Reagent.Obsidian, Reagent.Bone,
							Reagent.Blackmoor
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1 ); } }

        public override double RequiredSkill{ get{ return 100.0; } }
        public override int RequiredMana{ get{ return 60; } }

        public WraithBreathSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
	    double duration = 10.0 + (Caster.Skills[DamageSkill].Value * 0.2);
	
	    if( map != null ){
		foreach( Mobile m in Caster.GetMobilesInRange( 1 + (int)(Caster.Skills[CastSkill].Value / 15.0)) ){
		    if( Caster != m &&
			SpellHelper.ValidIndirectTarget(Caster, m) &&
			Caster.CanBeHarmful(m, false)
			&& Caster.InLOS(m) &&
			!m.Paralyzed &&
			!m.Frozen){
		       			
			Caster.DoHarmful(m);
			
			if ( CheckResisted(m) ){
			    duration *= 0.5;
			}
			
			m.FixedEffect( 0x376A, 6, 1 );
			m.PlaySound(0x204);
			m.Paralyze( TimeSpan.FromSeconds( duration ) );	
		    }
		}
	    }

	    Caster.PlaySound( 0x204 );
	    
        Return:
            FinishSequence();
        }
    }
}
