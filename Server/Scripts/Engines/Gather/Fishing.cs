using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Gather {
    public class Fishing : Server.Engines.Gather.GatherSystem {

	public enum Fish {
	    Fish,
	    BigFish
	}

	public override void SendFailMessage( Mobile m ) {
	    m.SendLocalizedMessage( 503171 ); // You fish for a while but...
	}

	public override void SendNoResourcesMessage( Mobile m) {
	    m.SendLocalizedMessage( 5031 ); //the fish don't seem to be biting
	}

	public override void SendSuccessMessage( Mobile m ) {
	    m.SendLocalizedMessage( 1042635 ); //you extract some bla bla bla
	}

	public override void StartGatherTimer( Mobile from, Item tool, GatherSystem system, GatherNode node, object targeted, object locked ) {
	    TimeSpan delay = m_EffectsHolder.EffectDelay;
	    int which = TimeSpan.Compare(m_EffectsHolder.EffectDelay, m_EffectsHolder.EffectSoundDelay);
	    // if which == -1, argument 1 shorter than arg2
	    // if which == 0, they're equal
	    // if which == 1, arg1 longer than arg2
	    // therefore
	    
	    if ( which < 1 ) {
		delay = m_EffectsHolder.EffectSoundDelay;
	    }
	    
	    new GatherTimer( from, tool, system, node, targeted, locked, delay ).Start();
	}

	private Fishing( Serial serial ) : this() {
	}

	private Fishing() {
	    m_EffectsHolder = new GatherFXHolder();
	    
	    m_EffectsHolder.EffectActions = new int[]{ 12 };
	    m_EffectsHolder.EffectSounds = new int[0];
	    m_EffectsHolder.EffectCounts = new int[]{ 1 };
	    m_EffectsHolder.EffectDelay = TimeSpan.Zero;
	    m_EffectsHolder.EffectSoundDelay = TimeSpan.FromSeconds( 8.0 );
	    
	    m_Nodes = new List<GatherNode>();
	    GatherNode node = new GatherNode (0, 0, Utility.RandomMinMax(0,10), Utility.RandomMinMax(0,10), Utility.RandomDouble(), 250.0, 100.0, 150.0, typeof( Items.Fish ) );
	    m_Nodes.Add(node);
	}

	private GatherFXHolder m_EffectsHolder;
	
	private static GatherSystemController m_Controller;
	public static GatherSystemController Controller {
	    get { return m_Controller; }
	}
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	    m_Controller.System = System; //see Mining.cs
	    m_System.SkillName = SkillName.Fishing;
	}
	
        private static Fishing m_System;
        public static Fishing System {
            get {
                if ( m_System == null ) m_System = new Fishing();
                return m_System;
            }
            set {
                m_System = value;
            }
        }
    }
}
