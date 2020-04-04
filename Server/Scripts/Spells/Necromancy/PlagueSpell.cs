using System;
using System.Collections;
using System.Collections.Generic;
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
							Reagent.Bloodspawn, Reagent.Pumice, Reagent.NoxCrystal
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 140.0; } }
        public override int RequiredMana{ get{ return 130; } }

        public PlagueSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

	    int level = 0;
	    double pStr = Caster.Skills[DamageSkill].Value;
	   
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
	    
	    if( map != null ){
		foreach( Mobile m in Caster.GetMobilesInRange( 1 + (int)(Caster.Skills[CastSkill].Value / 15.0)) ){
		    if( Caster != m &&
			SpellHelper.ValidIndirectTarget(Caster, m) &&
			Caster.CanBeHarmful(m, false)
			&& Caster.InLOS(m)){
			
			if ( ! m.BeginAction( typeof( PlagueSpell ) ) ) {
			    break;
			}
			
			Caster.DoHarmful(m);
						
			m.ApplyPoison( Caster, Poison.GetPoison( level ) );
		    }
		}
	    }
	
	    Caster.PlaySound( 0x1e2 );
	    
        Return:
            FinishSequence();
        }
    }
}
