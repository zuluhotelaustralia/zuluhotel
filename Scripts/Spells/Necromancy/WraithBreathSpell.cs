using System;
using System.Collections;
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

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 0.0; } }
        public override int RequiredMana{ get{ return 0; } }

        public WraithBreathSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
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

	    List<Mobile> targets = new List<Mobile>();
	    Map map = Caster.Map;
	    if( map != null ){
		foreach( Mobile m in Caster.GetMobilesInRange( 1 + (int)(Caster.Skills[CastSkill].Value / 15.0)) ){
		    if( Caster != m &&
			SpellHelper.ValidIndirectTarget(Caster, m) &&
			Caster.CanBeHarmful(m, false)
			&& Caster.InLOS(m) &&
			!m.Paralyzed &&
			!m.Frozen){
			targets.Add(m);
		    }
		}
	    }

	    Caster.PlaySound( 0x204 );

	    for ( int i=0; i<targets.Count; i++ ) {
		Mobile m = targets[i];
		Caster.DoHarmful(m);

		double duration = 10.0 + (Caster.Skills[DamageSkill].Value * 0.2);
		if ( CheckResisted(m) ){
		    duration *= 0.5;
		}
		
		m.FixedEffect( 0x376A, 6, 1 );
		m.PlaySound(0x204);
		m.Paralyze( TimeSpan.FromSeconds( duration ) );
	    }
	    
            if ( ! m.BeginAction( typeof( WraithBreathSpell ) ) ) {
                goto Return;
            }
	    
        Return:
            FinishSequence();
        }
    }
}
