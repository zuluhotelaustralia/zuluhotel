using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Harvest;
using Server.Engines.Gather;

namespace Server.Items {
    public class GatherSystemController : Item {

	public enum ControlledSystem{
	    None,
	    Mining,
	    Lumberjacking,
	    Fishing
	}
	
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

	private ControlledSystem m_ControlledSystem = ControlledSystem.None;
	
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
	    // coming soon
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize(writer);

	    writer.Write( (int) 0 ); //version

	    writer.Write( (int)m_ControlledSystem );

	}

	public override void Deserialize( GenericReader reader ) {
	    int version = reader.ReadInt();

	    switch( version )
	    {
		case 0:
		    {
			m_ControlledSystem = (ControlledSystem)reader.ReadInt();
			
			// the engine has to know which stone it takes orders from
			switch( m_ControlledSystem )
			{
			    case ControlledSystem.None:
				{
				    break;
				}
			    case ControlledSystem.Mining:
				{
				    Server.Engines.Gather.Mining.Setup(this);
				    break;
				}
			    case ControlledSystem.Lumberjacking:
				{
				    Server.Engines.Gather.Lumberjacking.Setup(this);
				    break;
				}
			    case ControlledSystem.Fishing:
				{
				    Server.Engines.Gather.Fishing.Setup(this);
				    break;
				}
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
