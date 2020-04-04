using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Necromancy
{
    public class LicheFormSpell : NecromancerSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
							"Liche Form", "Umbrae Tenebrae Miserere",
							236, 9031,
							Reagent.DaemonBone, Reagent.Brimstone, Reagent.DragonsBlood,
							Reagent.VolcanicAsh
							);

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1 ); } }

        public override double RequiredSkill{ get{ return 140.0; } }
        public override int RequiredMana{ get{ return 130; } }

        public LicheFormSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }

        public override void OnCast()
        {
	    if( TransformationSpellHelper.UnderTransformation( Caster )){
		Caster.SendLocalizedMessage( 1061633 ); // you cannot polymorph because fuck you etc.
		goto Return;
	    }
	    if( DisguiseTimers.IsDisguised( Caster )){
		Caster.SendLocalizedMessage( 502167 ); //fuck off, etc.
		goto Return;
	    }
	    if( Caster.BodyMod == 183 || Caster.BodyMod == 184 ) {
		Caster.SendLocalizedMessage( 1042512 ); //no
		goto Return;
	    }
	    if( !Caster.CanBeginAction( typeof( LicheFormSpell ) ) ){
		Caster.SendLocalizedMessage( 1005559 ); //no
		goto Return;
	    }
		
            if ( ! CheckSequence() )
            {
                goto Return;
            }

	    double dexmod = Caster.Dex / 2;
	    double strmod = Caster.Str / 2;
	    double intmod = Caster.Int * 2;

	    if( Caster is PlayerMobile &&
		((PlayerMobile)Caster).Spec.SpecName == SpecName.Mage){
		double bonus = ((PlayerMobile)Caster).Spec.Bonus;
		dexmod /= bonus;
		strmod /= bonus;
		intmod *= bonus;
	    }

	    int newBody = 0x18;

	    //hocus pocus... SpellHelper sets its own timers, we only need to clean up after the hue and body mods --sith
	    Caster.BodyMod = newBody;
	    Caster.HueMod = 0;
	    SpellHelper.AddStatBonus(Caster, Caster, StatType.Int, (int)intmod, TimeSpan.FromSeconds( Caster.Skills[DamageSkill].Value * 5 ));
	    SpellHelper.AddStatCurse(Caster, Caster, StatType.Dex, (int)dexmod, TimeSpan.FromSeconds( Caster.Skills[DamageSkill].Value * 5 ));
	    SpellHelper.AddStatCurse(Caster, Caster, StatType.Str, (int)strmod, TimeSpan.FromSeconds( Caster.Skills[DamageSkill].Value * 5 ));

	    Caster.PlaySound(0x202);
	    
	    //polymorph calls these... do we need to? --sith
	    BaseArmor.ValidateMobile( Caster );
	    BaseClothing.ValidateMobile( Caster );
	    
            new InternalTimer( Caster ).Start();

        Return:
            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Caster;

            public InternalTimer( Mobile caster ) : base( TimeSpan.FromSeconds( 0 ) )
            {
                m_Caster = caster;

                double time = m_Caster.Skills[SkillName.SpiritSpeak].Value * 5;
		Delay = TimeSpan.FromSeconds( time );
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Caster.EndAction( typeof( LicheFormSpell ) );
		m_Caster.BodyMod = 0;
		m_Caster.HueMod = -1;

		BaseArmor.ValidateMobile(m_Caster);
		BaseClothing.ValidateMobile(m_Caster);
            }
        }
    }
}
