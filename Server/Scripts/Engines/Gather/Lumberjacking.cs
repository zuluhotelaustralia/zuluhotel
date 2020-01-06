using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Gather {
    public class Lumberjacking : Server.Engines.Gather.GatherSystem {

	public enum Logs{
	    Log,
	    PinetreeLog,
	    CherryLog,
	    OakLog,
	    PurplePassionLog,
	    GoldenReflectionLog,
	    HardrangerLog,
	    JadewoodLog,
	    DarkwoodLog,
	    StonewoodLog,
	    SunwoodLog,
	    GauntletLog,
	    SwampwoodLog,
	    StardustLog,
	    SilverleafLog,
	    StormtealLog,
	    EmeraldLog,
	    BloodwoodLog,
	    CrystalLog,
	    BloodhorseLog,
	    DoomwoodLog,
	    ZuluLog,
	    DarknessLog,
	    ElvenLog
	}

	private static int[] m_TreeTiles = new int[]
	    {
		0x4CCA, 0x4CCB, 0x4CCC, 0x4CCD, 0x4CD0, 0x4CD3, 0x4CD6, 0x4CD8,
		0x4CDA, 0x4CDD, 0x4CE0, 0x4CE3, 0x4CE6, 0x4CF8, 0x4CFB, 0x4CFE,
		0x4D01, 0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D57, 0x4D58, 0x4D59,
		0x4D5A, 0x4D5B, 0x4D6E, 0x4D6F, 0x4D70, 0x4D71, 0x4D72, 0x4D84,
		0x4D85, 0x4D86, 0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA,
		0x52BB, 0x52BC, 0x52BD,

		0x4CCE, 0x4CCF, 0x4CD1, 0x4CD2, 0x4CD4, 0x4CD5, 0x4CD7, 0x4CD9,
		0x4CDB, 0x4CDC, 0x4CDE, 0x4CDF, 0x4CE1, 0x4CE2, 0x4CE4, 0x4CE5,
		0x4CE7, 0x4CE8, 0x4CF9, 0x4CFA, 0x4CFC, 0x4CFD, 0x4CFF, 0x4D00,
		0x4D02, 0x4D03, 0x4D45, 0x4D46, 0x4D47, 0x4D48, 0x4D49, 0x4D4A,
		0x4D4B, 0x4D4C, 0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52,
		0x4D53, 0x4D5C, 0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62,
		0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, 0x4D73,
		0x4D74, 0x4D75, 0x4D76, 0x4D77, 0x4D78, 0x4D79, 0x4D7A, 0x4D7B,
		0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, 0x4D87, 0x4D88, 0x4D89, 0x4D8A,
		0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, 0x4D95, 0x4D96,
		0x4D97, 0x4D99, 0x4D9A, 0x4D9B, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA1,
		0x4DA2, 0x4DA3, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA9, 0x4DAA, 0x4DAB,
		0x52BE, 0x52BF, 0x52C0, 0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5,
		0x52C6, 0x52C7
	    };

	private GatherFXHolder m_EffectsHolder;
	
	public static void Initialize() {
	    Array.Sort( m_TreeTiles);
	}
	
	private static GatherSystemController m_Controller;
	public static GatherSystemController Controller {
	    get { return m_Controller; }
	}
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	    m_Controller.System = System; //see Mining.cs
	    m_System.SkillName = SkillName.Lumberjacking;
	    m_Controller.Name = "Lumberjacking System Control Stone";
	    m_Controller.Hue = 1045;
	    m_Controller.Movable = false;
	    
	    m_System.Nodes.Clear(); //the default constructor puts a placeholder node in

	    // see mining.cs
	    int x = 3033;
	    int y = 3406;

	    // ok fuck it I can't think of a more elegant way to do this without a massive refactor for which I have no motivation
	    // gathernode (x, y, vx, vy, abundance, difficulty, minskill, maxskill, type)
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.9, 2500.0, 0.0, 80.0, typeof(Server.Items.Log) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.9, 2500.0, 10.0, 80.0, typeof(Server.Items.PinetreeLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.9, 2500.0, 10.0, 80.0, typeof(Server.Items.CherryLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.9, 2000.0, 20.0, 90.0, typeof(Server.Items.OakLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.9, 1000.0, 20.0, 90.0, typeof(Server.Items.PurplePassionLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.8, 2000.0, 20.0, 900.0, typeof(Server.Items.GoldenReflectionLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.8, 1800.0, 30.0, 100.0, typeof(Server.Items.HardrangerLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.8, 1800.0, 30.0, 100.0, typeof(Server.Items.JadewoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.8, 500.0, 40.0, 150.0, typeof(Server.Items.DarkwoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.7, 1500.0, 40.0, 100.0, typeof(Server.Items.StonewoodLog) )); // make good sparring gear?
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.7, 1250.0, 40.0, 110.0, typeof(Server.Items.SunwoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.5, 2000.0, 40.0, 90.0, typeof(Server.Items.GauntletLog) )); // make good bows? arrows?
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.7, 1100.0, 50.0, 120.0, typeof(Server.Items.SwampwoodLog) )); // make good sparring arrows due to softness?
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.6, 1000.0, 60.0, 130.0, typeof(Server.Items.StardustLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.6, 900.0, 70.0, 130.0, typeof(Server.Items.SilverleafLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.5, 900.0, 70.0, 130.0, typeof(Server.Items.StormtealLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.5, 900.0, 80.0, 150.0, typeof(Server.Items.EmeraldwoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.4, 800.0, 90.0, 150.0, typeof(Server.Items.BloodwoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.3, 800.0, 100.0, 150.0, typeof(Server.Items.CrystalwoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.3, 700.0, 100.0, 150.0, typeof(Server.Items.BloodhorseLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.2, 500.0, 100.0, 150.0, typeof(Server.Items.DoomwoodLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.1, 300.0, 130.0, 150.0, typeof(Server.Items.ZuluLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.1, 100.0, 130.0, 150.0, typeof(Server.Items.DarknessLog) ));
	    m_System.Nodes.Add( new GatherNode(x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10),
					       0.1, 100.0, 130.0, 150.0, typeof(Server.Items.ElvenLog) ));

	}
	
        private static Lumberjacking m_System;
        public static Lumberjacking System {
            get {
                if ( m_System == null ){
		    m_System = new Lumberjacking();
		}
		
                return m_System;
            }
        }

	public bool Validate( int tileID )
	{
	    // is this fast enough?  Should it be in its own thread? TODO 
	    int dist = -1;
	    for ( int i = 0; dist < 0 && i < m_TreeTiles.Length; ++i ){
		dist = ( m_TreeTiles[i] - tileID );
		if( dist == 0){
		    return true;
		}
	    }

	    return false;
	}

	public override bool BeginGathering( Mobile from, Item tool )
	{
	    if ( !base.BeginGathering( from, tool ) )
		return false;

	    //from.SendLocalizedMessage( 1151657 ); // Where do you wish to use this?
	    // baseaxe or something else already sends a cliloc, also 1151657 is actually "where do you wish to place this?" which looks retarded
	    return true;
	}

	public int GetTileID( object targ ){
	    int tileID;
	    
	    if( targ is Static && !((Static)targ).Movable ){
		Static obj = (Static)targ;
		tileID = (obj.ItemID & 0x3FFF) | 0x4000; //what the actual fuck does this do?
	    }
	    else if( targ is StaticTarget ){
		StaticTarget obj = (StaticTarget)targ;
		tileID = (obj.ItemID & 0x3FFF) | 0x4000;
	    }
	    else if( targ is LandTarget ){
		LandTarget obj = (LandTarget)targ;
		tileID = obj.TileID;
	    }
	    else {
		tileID = 0;
	    }

	    return tileID;
	}
	
	public override void StartGathering( Mobile from, Item tool, object targeted ) {
	    
	    int tileID = GetTileID( targeted );
	    Point3D loc;
	    
	    if( tileID == 0 ){
		loc = new Point3D( from.Location );
	    }
	    else {
		if( targeted is StaticTarget ){
		    loc = new Point3D( ((StaticTarget)targeted).Location );
		}
		else if( targeted is LandTarget ){
		    loc = new Point3D( ((LandTarget)targeted).Location );
		}
		else if( targeted is Static ){
		    loc = new Point3D( ((Static)targeted).Location );
		}
		else {
		    //this is so fucking dumb, gotta rework this whole thing in the future --sith
		    loc = new Point3D( from.Location );
		}
		      
	    }
	    

	    if( Validate( tileID ) ) {
		base.StartGathering( from, tool, targeted );
		m_EffectsHolder.PlayEffects(from, loc);
	    }
	    else {
		OnBadGatherTarget(from, tool, targeted);
	    }
	}

	public void OnBadGatherTarget( Mobile from, Item tool, object toHarvest )
	{
	    if ( toHarvest is LandTarget ){
		from.SendLocalizedMessage( 500488 ); // There's not enough wood here to harvest.
	    }
	    else {
		from.SendLocalizedMessage( 500489 ); // You can't use an axe on that.
	    }

	}

	public bool CheckHarvest( Mobile from, Item tool, object toHarvest )
	{
            // TODO: No base implementation yet, do we need one?
	    // if ( !base.CheckHarvest( from, tool, toHarvest ) )
	    //     return false;

            /*	    if ( def == m_Sand && !(from is PlayerMobile && from.Skills[SkillName.Mining].Base >= 100.0 && ((PlayerMobile)from).SandMining) )
	    {
		OnBadGatherTarget( from, tool, toHarvest );
		return false;
	    }
	    else */if ( from.Mounted )
	    {
		from.SendLocalizedMessage( 501864 ); // You can't mine while riding.
		return false;
	    }
	    else if ( from.IsBodyMod && !from.Body.IsHuman )
	    {
		from.SendLocalizedMessage( 501865 ); // You can't mine while polymorphed.
		return false;
	    }

	    return true;
	}

	public override void SendFailMessage( Mobile m ) {
	    m.SendLocalizedMessage( 500495 ); // You hack at the tree for a while but...
	}

	public override void SendNoResourcesMessage( Mobile m) {
	    m.SendLocalizedMessage( 500493 ); //there's not enough wood here to harvest
	}

	public override void SendSuccessMessage( Mobile m ) {
	    m.SendLocalizedMessage( 500498 ); //you put some logs into your backpack
	}

	public override void StartGatherTimer( Mobile from, Item tool, GatherSystem system, GatherNode node, object targeted, object locked ) {
	    TimeSpan delay = TimeSpan.FromSeconds( 4.0 );
	    
	    new GatherTimer( from, tool, system, node, targeted, locked, delay ).Start();
	}

	private Lumberjacking( Serial serial) : this() {}

	private Lumberjacking() {
	    m_EffectsHolder = new GatherFXHolder();

	    //copied from Harvesting - don't blame me bro
	    m_EffectsHolder.EffectActions = new int[]{ 13 };
	    m_EffectsHolder.EffectSounds = new int[]{ 0x13E };
	    m_EffectsHolder.EffectCounts = new int[]{ 1, 2, 2, 2, 3};
	    m_EffectsHolder.EffectDelay = TimeSpan.FromSeconds(0.9);
	    m_EffectsHolder.EffectSoundDelay = TimeSpan.FromSeconds(1.6);
	    
	    m_Nodes = new List<GatherNode>();
	    GatherNode node = new GatherNode( 0, 0, Utility.RandomMinMax(0,10), Utility.RandomMinMax(0,10), Utility.RandomDouble(), 250.0, 100.0, 150.0, typeof(Log) );
	    m_Nodes.Add(node);
	}
    }
}
