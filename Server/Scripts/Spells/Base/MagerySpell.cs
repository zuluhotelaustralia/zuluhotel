using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells
{
    public abstract class MagerySpell : Spell
    {
	    
	public MagerySpell( Mobile caster, Item scroll, SpellInfo info )
	    : base( caster, scroll, info )
	{
	    m_AttackType = AttackType.Magical;
	    m_DamageType = DamageType.None;
	}

	public override bool ConsumeReagents()
	{
	    if( base.ConsumeReagents() )
		return true;

	    if( ArcaneGem.ConsumeCharges( Caster, (Core.SE ? 1 : 1 + (int)Circle) ) )
		return true;

	    return false;
	}

	private const double ChanceOffset = 20.0, ChanceLength = 100.0 / 7.0;

	public override void GetCastSkills( out double min, out double max )
	{
	    int circle = (int)Circle;

	    if( Scroll != null )
		circle -= 2;

	    double avg = ChanceLength * circle;

	    min = avg - ChanceOffset;
	    max = avg + ChanceOffset;
	}

	private static int[] m_ManaTable = new int[] { 4, 6, 9, 11, 14, 20, 40, 50 };

	public override int GetMana()
	{
	    if( Scroll is BaseWand ){
		return 0;
	    }

	    double mana = (double)m_ManaTable[(int)Circle];
	    if( Caster is PlayerMobile ){
		PlayerMobile pm = Caster as PlayerMobile;
		if( pm.Spec.SpecName == SpecName.Mage ){
		    mana /= pm.Spec.Bonus;
		}
	    }
		
	    return (int)mana;
	}

	public override double GetResistSkill( Mobile m )
	{
	    //int maxSkill = (1 + (int)Circle) * 10;
	    //maxSkill += (1 + ((int)Circle / 6)) * 25;

	    //if( m.Skills[SkillName.MagicResist].Value < maxSkill )
	    m.CheckSkill( SkillName.MagicResist, 0.0, 130.0 );

	    return m.Skills[SkillName.MagicResist].Value;
	}

	public override TimeSpan GetCastDelay()
	{
	    if( !Core.ML && Scroll is BaseWand )
		return TimeSpan.Zero;

	    if( !Core.AOS )
		return TimeSpan.FromSeconds( 0.5 + (0.25 * (int)Circle) );

	    return base.GetCastDelay();
	}

	public override TimeSpan CastDelayBase
	{
	    get
	    {
		return TimeSpan.FromSeconds( (3 + (int)Circle) * CastDelaySecondsPerTick );
	    }
	}
    }
}
