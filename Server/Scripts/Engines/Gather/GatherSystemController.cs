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
	public static void Initialize() {
	    Console.WriteLine("Initializing Gather System nodes...");
	    
	    int i = 0;

	    if( m_System == null ) {
		Console.WriteLine("Error:  Gather System Controller not configured!");
	    }
	    else {
		if( m_System.Nodes == null ) {
		    m_System.Setup();
		}

		// foreach( GatherNode n in m_Nodes ){
		//     m_System.AddNode(n);
		//     i++;
		// }
	    }
	    
	    Console.WriteLine("Done!  Initialized "+ i +" GatherNodes.");
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize(writer);

	    writer.Write( (int) 0 ); //version

	}

	public override void Deserialize( GenericReader reader ) {
	    int version = reader.ReadInt();

	    switch( version )
	    {
		case 0:
		    {
			// the engine has to know which stone it takes orders from
			Server.Engines.Gather.Mining.Controller = this;
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
