using System;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

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
	    if (Factions.Sigil.ExistsOn( Caster ) ) {
		Caster.SendLocalizedMessage( 1010521 ); //can't do that while you have a sigil
		return;
	    }
	    else if( TransformationSpellHelper.UnderTransformation( Caster ) )
	    {
		Caster.SendLocalizedMessage( 1061633 ); // You cannot polymorph while in that form.
		return;
	    }
	    else if ( DisguiseTimers.IsDisguised( Caster ) )
	    {
		Caster.SendLocalizedMessage( 502167 ); // You cannot polymorph while disguised.
		return;
	    }
	    else if ( Caster.BodyMod == 183 || Caster.BodyMod == 184 )
	    {
		Caster.SendLocalizedMessage( 1042512 ); // You cannot polymorph while wearing body paint
		return;
	    }

            if ( ! CheckSequence() )
            {
                goto Return;
            }

            if ( ! Caster.BeginAction( typeof( WraithFormSpell ) ) ) {
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

	    double dmg = Utility.Dice(2, (int)(Caster.Skills[DamageSkill].Value / 15.0), 0 ); //roughly 9 at 130.0
	    if( Caster is PlayerMobile && ((PlayerMobile)Caster).Spec.SpecName == SpecName.Mage ){
		dmg *= ((PlayerMobile)Caster).Spec.Bonus;  //1.4 at spec 4
	    }
	    

	    new WraithFormTimer(Caster, targets, TimeSpan.FromSeconds(interval), duration, dmg ).Start();

	Return:
	    FinishSequence();
	}

	private static void EndWraithForm( Mobile m )
	{
	    if( !m.CanBeginAction( typeof( WraithFormSpell ) ) )
	    {
		m.BodyMod = 0;
		m.HueMod = -1;
		m.EndAction( typeof( WraithFormSpell ) );

		BaseArmor.ValidateMobile( m );
		BaseClothing.ValidateMobile( m );
	    }
	}

	private class WraithFormTimer : Timer
	{
	    private Mobile m_Owner;
	    private List<Mobile> m_Targets;
	    private double m_Damage;

	    public WraithFormTimer( Mobile owner, List<Mobile> targets, TimeSpan interval, int duration, double damage ) :
		base( TimeSpan.FromSeconds( 1 ), interval, duration )
	    {
		m_Owner = owner;
		m_Targets = targets;
		Priority = TimerPriority.OneSecond;
	    }

	    protected override void OnTick()
	    {
		for(int i=0; i<m_Targets.Count; i++) {
		    Mobile m = m_Targets[i];

		    m_Owner.DoHarmful( m );

		    m.FixedParticles( 0x374a, 10, 30, 5052, EffectLayer.LeftFoot );
		    m.PlaySound(0x1fa);
		    m.Damage( (int)m_Damage, m_Owner, DamageType.Necro );  //about 12 at spec 4
		    
		    m_Owner.Mana += (int)m_Damage;
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
