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

	public static void Initialize() {
	    Array.Sort( m_TreeTiles);
	}
	
	private static GatherSystemController m_Controller;
	public static GatherSystemController Controller {
	    get { return m_Controller; }
	}
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	    m_Controller.System = m_System;
	    m_System.SkillName = SkillName.Lumberjacking;
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
	    // is this fast enough?  Should it be in its own thread?
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

	    from.SendLocalizedMessage( 503033 ); // Where do you wish to dig?
	    return true;
	}

	public override void PlayGatherEffects(){
	}

	public override void StartGathering( Mobile from, Item tool, object targeted) {
	    int tileID;

	    if( targeted is Static && !((Static)targeted).Movable ){
		Static obj = (Static)targeted;
		tileID = (obj.ItemID & 0x3FFF) | 0x4000; //what the actual fuck does this do?
	    }
	    else if( targeted is StaticTarget ){
		StaticTarget obj = (StaticTarget)targeted;
		tileID = (obj.ItemID & 0x3FFF) | 0x4000;
	    }
	    else if( targeted is LandTarget ){
		LandTarget obj = (LandTarget)targeted;
		tileID = obj.TileID;
	    }
	    else {
		tileID = 0;
	    }

	    if( Validate( tileID ) ) {
		PlayGatherEffects();
		base.StartGathering( from, tool, targeted );
	    }

	}

	public void OnBadGatherTarget( Mobile from, Item tool, object toHarvest )
	{
	    if ( toHarvest is LandTarget )
		from.SendLocalizedMessage( 501862 ); // You can't mine there.
	    else
		from.SendLocalizedMessage( 501863 ); // You can't mine that.
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

	private Lumberjacking( Serial serial) : this() {}

	private Lumberjacking() {
	    m_Nodes = new List<GatherNode>();

	    GatherNode node = new GatherNode( 0, 0, Utility.RandomMinMax(0,10), Utility.RandomMinMax(0,10), Utility.RandomDouble(), 250.0, 100.0, 150.0, typeof(Log) );
	    m_Nodes.Add(node);
	}
    }
}
