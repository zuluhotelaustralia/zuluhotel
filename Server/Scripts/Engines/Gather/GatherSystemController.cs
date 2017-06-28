using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Harvest;
using Server.Engines.Gather;

namespace Server.Items {
    public class GatherSystemController : Item {

	private enum Ores{
	    AnraOre,
	    AzuriteOre,
	    BlackDwarfOre,
	    BronzeOre,
	    CopperOre,
	    CrystalOre,
	    DarkPaganOre,
	    DarkSableRubyOre,
	    DestructionOre,
	    DoomOre,
	    DripstoneOre,
	    DullCopperOre,
	    EbonTwilightSapphireOre,
	    ExecutorOre,
	    FruityOre,
	    GoddessOre,
	    GoldOre,
	    IceRockOre,
	    IronOre,
	    LavarockOre,
	    MalachiteOre,
	    MysticOre,
	    NewZuluOre,
	    OldBritainOre,
	    OnyxOre,
	    PeachblueOre,
	    PlatinumOre,
	    PyriteOre,
	    RadiantNimbusDiamondOre,
	    RedElvenOre,
	    SilverRockOre,
	    SpectralOre,
	    SpikeOre,
	    UndeadOre,
	    VirginityOre
	}

	private List<GatherNode> m_Nodes;
	public List<GatherNode> Nodes { get { return m_Nodes; } }

	private GatherSystem m_System;
	public GatherSystem System {
	    get {
		return m_System;
	    }
	    set {
		m_System = value;
	    }
	}

	public GatherSystemController() : base ( 0xED4 ) {
	}
	public GatherSystemController( Serial serial ) : base( serial ){
	}
	
	//worldsave only serializes items, so:
	public void Initialize() {
	    Console.WriteLine("Initializing Gather System nodes...");

	    if( m_Nodes == null ) {
		m_Nodes = new List<GatherNode>();
	    }
	    
	    int i = 0;

	    if( m_System == null ) {
		Console.WriteLine("Error:  Gather System Controller not configured!");
	    }
	    else {
		m_System.Setup();
		// GatherSystem.ClearNodes(); // necessary?
		foreach( GatherNode n in m_Nodes ){
		    m_System.AddNode(n);
		    i++;
		}
	    }
	    
	    Console.WriteLine("Done!  Initialized "+ i +" GatherNodes.");
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize(writer);

	    writer.Write( (int) 0 ); //version

	    foreach(GatherNode n in m_Nodes){
		writer.Write( n.X );
		writer.Write( n.Y );
		writer.Write( n.vX );
		writer.Write( n.vY );
		writer.Write( n.Abundance );
	    }

	}

	public override void Deserialize( GenericReader reader ) {
	    int version = reader.ReadInt();

	    int x;
	    int y;
	    int vx;
	    int vy;
	    double a;

	    switch( version )
	    {
		case 0:
		    {
			foreach( var t in Enum.GetValues(typeof(Ores)) ) {
			    x = reader.ReadInt();
			    y = reader.ReadInt();
			    vx = reader.ReadInt();
			    vy = reader.ReadInt();
			    a = reader.ReadDouble();

			    //make this more elegant
			    GatherNode node = new GatherNode( x, y, vx, vy, a, new HarvestResource( 0.00, 0.00, 100.00, 1007072, Type.GetType( t.ToString() ) ) );
			    m_Nodes.Add(node);
			}

			break;
		    }
		default:
		    {
			Console.WriteLine("GatherSystemController:  This should never happen!");
			break;
		    }
	    }
	}



    }
}
