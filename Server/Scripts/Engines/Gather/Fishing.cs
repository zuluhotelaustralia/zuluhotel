using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;

namespace Server.Engines.Gather {
    public class Fishing : Server.Engines.Gather.GatherSystem {

	public enum Fish {
	    Fish,
	    BigFish
	}

	private static readonly int[] m_WaterTiles = new int[]
	    {
		0x00A8, 0x00AB,
		0x0136, 0x0137,
		0x5797, 0x579C,
		0x746E, 0x7485,
		0x7490, 0x74AB,
		0x74B5, 0x75D5
	    };

	public override void SendFailMessage( Mobile m ) {
	    m.SendLocalizedMessage( 503171 ); // You fish for a while but...
	}

	public override void SendNoResourcesMessage( Mobile m) {
	    m.SendLocalizedMessage( 5031 ); //the fish don't seem to be biting
	}

	public override void SendSuccessMessage( Mobile m ) {
	    m.SendLocalizedMessage( 1042635 ); //you extract some bla bla bla
	}

	public override void StartGathering( Mobile from, Item tool, object targeted) {
	    int tileID;
	    Point3D loc;
	    
	    if( targeted is Static && !((Static)targeted).Movable ){
		Static obj = (Static)targeted;
		loc = new Point3D(obj.Location);
		tileID = (obj.ItemID & 0x3FFF) | 0x4000; //what the actual fuck does this do?
	    }
	    else if( targeted is StaticTarget ){
		StaticTarget obj = (StaticTarget)targeted;
		loc = new Point3D(obj.Location);
		tileID = (obj.ItemID & 0x3FFF) | 0x4000;
	    }
	    else if( targeted is LandTarget ){
		LandTarget obj = (LandTarget)targeted;
		loc = new Point3D(obj.Location);
		tileID = obj.TileID;
	    }
	    else {
		loc = new Point3D(from.Location);
		tileID = 0;
	    }

	    if( Validate( tileID ) ) {
		m_EffectsHolder.PlayEffects( from, loc );
		new FishingSplashFXTimer(from, m_EffectsHolder, loc).Start();
		base.StartGathering( from, tool, targeted );
	    }
	}

	public bool Validate( int tileID )
	{
	    bool contains = false;

	    for ( int i = 0; !contains && i < m_WaterTiles.Length; i += 2 ) {
		contains = ( tileID >= m_WaterTiles[i] && tileID <= m_WaterTiles[i + 1] );
	    }

	    return contains;
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

	public override bool BeginGathering( Mobile from, Item tool )
	{
	    if ( !base.BeginGathering( from, tool ) )
		return false;

	    from.SendLocalizedMessage( 500974 ); // Where do you wish to fish?
	    return true;
	}

	public void OnBadGatherTarget( Mobile from, Item tool, object toHarvest )
	{
	    if ( toHarvest is LandTarget )
		from.SendLocalizedMessage( 500977 ); // You can't reach the water there.
	    else
		from.SendLocalizedMessage( 500978 ); // You need water to fish brah.
	}

	private Fishing() {
	    m_EffectsHolder = new GatherFXHolder();
	    
	    m_EffectsHolder.EffectActions = new int[]{ 12 };
	    m_EffectsHolder.EffectSounds = new int[] { 0x364 };
	    m_EffectsHolder.EffectCounts = new int[]{ 1 };
	    m_EffectsHolder.EffectDelay = TimeSpan.Zero;
	    m_EffectsHolder.EffectSoundDelay = TimeSpan.FromSeconds( 1.5 );
	    
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
	    m_Controller.Name = "Fishing System Control Stone";
	    m_Controller.Hue = 0x493;
	    m_Controller.Movable = false;

	    m_System.m_Nodes.Clear();

	    //see mining.cs
	    int x = 3033;
	    int y = 3406;
	    
	    Console.WriteLine("Gather Engine: Setting up fishing nodes...");
	    m_System.m_Nodes.Add(new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
						0.9, 2500.0, 0.0, 150.0, typeof(Server.Items.Fish)));
	    m_System.m_Nodes.Add(new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
						0.9, 2500.0, 50.0, 150.0, typeof(Server.Items.Fish)));
	    Console.WriteLine("Complete.");
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
