using System;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Sixth
{
    public class DispelSpell : MagerySpell
    {
	private static SpellInfo m_Info = new SpellInfo(
							"Dispel", "An Ort",
							218,
							9002,
							Reagent.Garlic,
							Reagent.MandrakeRoot,
							Reagent.SulfurousAsh
							);

	public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

	public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
	{
	}

	public override void OnCast()
	{
	    Caster.Target = new InternalTarget( this );
	}

	public class InternalTarget : Target
	{
	    private DispelSpell m_Owner;

	    public InternalTarget( DispelSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
	    {
		m_Owner = owner;
	    }

	    protected override void OnTarget( Mobile from, object o )
	    {
		if ( o is Mobile )
		{
		    Mobile m = (Mobile)o;
		    BaseCreature bc = m as BaseCreature;

		    if ( !from.CanSee( m ) )
		    {
			from.SendLocalizedMessage( 500237 ); // Target can not be seen.
		    }
		    else if ( m_Owner.CheckHSequence( m ) && ( bc != null && bc.IsDispellable ) )
		    {
			SpellHelper.Turn( from, m );

			double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus*2))) / 100;

			if ( dispelChance > Utility.RandomDouble() )
			{
			    Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
			    Effects.PlaySound( m, m.Map, 0x201 );

			    m.Delete();
			}
			else
			{
			    m.FixedEffect( 0x3779, 10, 20 );
			    from.SendLocalizedMessage( 1010084 ); // The creature resisted the attempt to dispel it!
			}
			return; //trust me
		    }
		    else if( m_Owner.CheckHSequence( m ) ){
			SpellHelper.Turn( from, m );
			SpellHelper.CheckReflect( (int)m_Owner.Circle, from, ref m );

			if( !m_Owner.CheckResisted(m) ) {
			    //if the buff is applied by SpellHelper it prepends "[Magic]" to the statmod's name
			    // so we can hopefully safely assume this is a magic buff and not e.g. a ring of +25 dex
			    
			    if( m.StatMods != null ){
				List<StatMod> sms = m.StatMods;
				foreach( StatMod mod in m.StatMods.ToArray() ){
				    if( mod.Name.Contains("magic", StringComparison.OrdinalIgnoreCase ) ) {
					// get yeeted on
					m.RemoveStatMod( mod.Name );
				    }
				}
			    }// foreach
			}//if not resisted
			else {
			    //they must therefore have resisted
			    m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy
			}
		    }
		    // for casting it on a player I want it to proc the v/sfx irrespective of whether or not the target was wiped
		    Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
		    Effects.PlaySound( m, m.Map, 0x201 );
		}
		
	    }

	    protected override void OnTargetFinish( Mobile from )
	    {
		m_Owner.FinishSequence();
	    }
	}
    }
}
