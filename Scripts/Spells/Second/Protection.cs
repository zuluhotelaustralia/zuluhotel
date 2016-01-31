using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
    public class ProtectionSpell : MagerySpell
    {
	private static Hashtable m_Registry = new Hashtable();
	public static Hashtable Registry { get { return m_Registry; } }

	private static SpellInfo m_Info = new SpellInfo(
							"Protection", "Uus Sanct",
							236,
							9011,
							Reagent.Garlic,
							Reagent.Ginseng,
							Reagent.SulfurousAsh
							);

	public override SpellCircle Circle { get { return SpellCircle.Second; } }

	public ProtectionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
	{
	}

	public override bool CheckCast()
	{
	    if ( Core.AOS )
		return true;

	    if ( m_Registry.ContainsKey( Caster ) )
	    {
		Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
		return false;
	    }
	    else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
	    {
		Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
		return false;
	    }

	    return true;
	}

	public override void OnCast()
	{
	   
	    if ( m_Registry.ContainsKey( Caster ) )
	    {
		Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
	    }
	    else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
	    {
		Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
	    }
	    else if ( CheckSequence() )
	    {
		if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
		{
		    int value = (int)(Caster.Skills[SkillName.EvalInt].Value +
					 Caster.Skills[SkillName.Meditation].Value +
					 Caster.Skills[SkillName.Inscribe].Value);
		    value /= 4;

		    if ( value < 0 )
			value = 0;
		    else if ( value > 75 )
			value = 75;

		    Caster.VirtualArmorMod += value;
		    
		    new InternalTimer( Caster, value ).Start();

		    Caster.FixedParticles( 0x375A, 9, 20, 5016, EffectLayer.Waist );
		    Caster.PlaySound( 0x1ED );
		}
		else
		{
		    Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
		}
	    }
		
	    FinishSequence();
	}
    

	private class InternalTimer : Timer
	{
	    private Mobile m_Caster;
	    private int m_Value;
	    
	    public InternalTimer( Mobile caster, int amount ) : base( TimeSpan.FromSeconds( 0 ) )
	    {
		m_Value = amount;
		
		double val = caster.Skills[SkillName.Magery].Value * 2.0;
		if ( val < 15 )
		    val = 15;
		else if ( val > 240 )
		    val = 240;

		m_Caster = caster;
		Delay = TimeSpan.FromSeconds( val );
		Priority = TimerPriority.OneSecond;
	    }

	    protected override void OnTick()
	    {
		m_Caster.EndAction( typeof( ProtectionSpell ) );
		m_Caster.VirtualArmorMod -= m_Value;
	    }
	}
    }
}
