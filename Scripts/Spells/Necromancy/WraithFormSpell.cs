using System;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
    public class WraithFormSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Wraith Form", "Manes Sollicti Mihi Infundite",
							227, 9031,
							Reagent.DaemonBone, Reagent.Brimstone, Reagent.Bloodspawn
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0 ); } }

        public override double RequiredSkill{ get{ return 0.0; } }
        public override int RequiredMana{ get{ return 0; } }

	private int m_NewBody;
	private int m_SkinHue;

        public WraithFormSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
	    if (Factions.Sigil.ExistsOn( Caster ) ){
		Caster.SendLocalizedMessage( 1010521 ); //can't do that while you have a sigil
		return false;
	    }
	    else if( TransformationSpellHelper.UnderTransformation( Caster ) )
	    {
		Caster.SendLocalizedMessage( 1061633 ); // You cannot polymorph while in that form.
		return false;
	    }
	    else if ( DisguiseTimers.IsDisguised( Caster ) )
	    {
		Caster.SendLocalizedMessage( 502167 ); // You cannot polymorph while disguised.
		return false;
	    }
	    else if ( Caster.BodyMod == 183 || Caster.BodyMod == 184 )
	    {
		Caster.SendLocalizedMessage( 1042512 ); // You cannot polymorph while wearing body paint
		return false;
	    }

            if ( ! CheckSequence() )
            {
                goto Return;
            }

            if ( ! m.BeginAction( typeof( WraithFormSpell ) ) ) {
		Caster.SendLocalizedMessage( 1005559 ); //this spell already in effect
                goto Return;
            }
	    Caster.HueMod = 0x482;
	    Caster.BodyMod = 0x1a;
	    Caster.PlaySound(0x210);

	    int interval = (int)(Caster.Skills[CastSkill].Value);
	    int duration = (int)(Caster.Skills[DamageSkill].Value);
					      
	    List<Mobile> targets = new List<Mobile>();
	    Map map = Caster.Map;
	    if( map != null){
		foreach( Mobile m in Caster.GetMobilesInRange( 1 + (int)(Caster.Skills[CastSkill].Value / 15.0 )) ){
		    if ( Caster != m &&
			 SpellHelper.ValidIndirectTarget(Caster, m) &&
			 Caster.CanBeHarmful(m, false) &&
			 Caster.InLOS(m)
			 ){			
			targets.Add(m);		
		    }
		}
	    }

	    new WraithFormTimer(Caster, targets, interval, duration).Start();

	Return:
	    FinishSequence();
	}

	private static void EndWraithForm( Mobile m )
	{
	    if( !m.CanBeginAction( typeof( WraithFormSpell ) ) )
	    {
		m.BodyMod = 0;
		m.HueMod = -1;
		m.EndAction( typeof( WraithFormShpell ) );

		BaseArmor.ValidateMobile( m );
		BaseClothing.ValidateMobile( m );
	    }
	}

	private class WraithFormTimer : Timer
	{
	    private Mobile m_Owner;

	    public WraithFormTimer( Mobile owner, List<Mobile> targets, TimeSpan interval, TimeSpan duration ) :
		base( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( interval ), TimeSpan.FromSeconds( duration ) )
	    {
		m_Owner = owner;
		Priority = TimerPriority.OneSecond;
	    }

	    protected override void OnTick()
	    {
		for(int i=0; i<targets.Count; i++) {
		    Mobile m = targets[i];

		    double dmg = Utility.Dice(2, ( m_Owner.Skills[DamageSkill].Value) /15.0 ); //roughly 8-9 at 130.0
		    if( m_Owner.Spec.SpecName == SpecName.Mage ){
			dmg *= m_Owner.Spec.Bonus;  //1.4 at spec 4
		    }

		    m_Owner.DoHarmful( m );

		    m.FixedParticles( 0x374a, 10, 30, 5052, EffectLayer.LeftFoot );
		    m.PlaySound(0x1fa);
		    m.Damage( (int)dmg, m_Owner, DamageType.Necro );  //about 12 at spec 4
		    
		    m_Owner.Mana += (int)dmg;
		    if( m_Owner.Mana > m_Owner.Int ){
			m_Owner.Mana = m_Owner.Int;
		    }
		}
		if (!Running) {
		    EndWraithForm( m_Owner );
		}
	    }
	}
    }   
}
