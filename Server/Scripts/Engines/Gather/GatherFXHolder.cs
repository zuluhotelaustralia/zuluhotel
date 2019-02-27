using Server;
using System;
using System.Collections;
using System.Collections.Generic;

//all this does is make it convenient to specify vfx/sfx for gathering

namespace Server.Engines.Gather {
    public class GatherFXHolder {

	private int[] m_EffectActions;
	private int[] m_EffectCounts;
	private int[] m_EffectSounds;

	private TimeSpan m_EffectSoundDelay;
	private TimeSpan m_EffectDelay;
	
	public int[] EffectActions{ get{ return m_EffectActions; } set{ m_EffectActions = value;} }
	public int[] EffectCounts{ get{ return m_EffectCounts; } set{ m_EffectCounts = value;} }
	public int[] EffectSounds{ get{ return m_EffectSounds; } set{ m_EffectSounds = value;} }

	public TimeSpan EffectSoundDelay{ get{ return m_EffectSoundDelay; } set{ m_EffectSoundDelay = value;} }
	public TimeSpan EffectDelay{ get{ return m_EffectDelay; } set{ m_EffectDelay = value;} }
	
	public GatherFXHolder(){}

	public void PlayEffects(Mobile from, Point3D loc ) {
	    //start a harvest sound timer and a harvest vfx timer, call playvfx and play sfx when they tick
	    new GatherSFXTimer( from, this ).Start();
	    new GatherVFXTimer( from, this, loc ).Start();
	}

	public void PlaySFX( Mobile from ){
	    if ( EffectSounds.Length > 0 ){
		from.PlaySound(Utility.RandomList( EffectSounds ) );
	    }
	}

	public void PlayVFX( Mobile from, Point3D loc ) {
	    from.Direction = from.GetDirectionTo( loc );
	    
	    if( EffectActions.Length > 0 && !from.Mounted ){
		from.Animate(Utility.RandomList(EffectActions), 5, 1, true, false, 0);
	    }
	}
    }
    
    public class FishingSplashFXTimer : Timer {
	private Mobile m_From;
	private GatherFXHolder m_Holder;
	private Point3D m_Loc;

	public FishingSplashFXTimer( Mobile from, GatherFXHolder holder, Point3D loc ) : base( holder.EffectSoundDelay ) {
	    m_From = from;
	    m_Holder = holder;
	    m_Loc = loc;
	}

	protected override void OnTick(){
	    Effects.SendLocationEffect(m_Loc, m_From.Map, 0x352D, 16, 4);
	}
    }
    
    public class GatherSFXTimer : Timer {
	private Mobile m_From;
	private GatherFXHolder m_Holder;

	public GatherSFXTimer( Mobile from, GatherFXHolder gfxh ) : base( gfxh.EffectSoundDelay ) {
	    m_From = from;
	    m_Holder = gfxh;
	}

	protected override void OnTick(){
	    m_Holder.PlaySFX( m_From );
	}
    }

    public class GatherVFXTimer: Timer {
	private Mobile m_From;
	private GatherFXHolder m_Holder;
	private Point3D m_Target;

	public GatherVFXTimer( Mobile from, GatherFXHolder gfxh, Point3D loc ) : base ( gfxh.EffectDelay ) {
	    m_From = from;
	    m_Target = loc;
	    m_Holder = gfxh;
	}

	protected override void OnTick() {
	    m_Holder.PlayVFX( m_From, m_Target );
	}
    }
}
