using System;
using System.Xml;
using Server;

namespace Server.Regions{
    public class CommonwealthRegion : TownRegion{
	public CommonwealthRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class OutdoorDungeonRegion : DungeonRegion {
	public OutdoorDungeonRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class MilitiaStronghold : TownRegion{
	public MilitiaStronghold( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class MilitiaNoTrapRegion : TownRegion{
	public MilitiaNoTrapRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class VorshunRegion : DungeonRegion{
	public VorshunRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class StagingAreaRegion : TownRegion{
	public StagingAreaRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class OrcRegion : DungeonRegion{
	public OrcRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class PirateRegion : DungeonRegion{
	public PirateRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class DrowRegion : DungeonRegion{
	public DrowRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class UndeadRegion : DungeonRegion{
	public UndeadRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class EventDuelPitsRegion : TownRegion{
	public EventDuelPitsRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class FigaroRegion : TownRegion{
	public FigaroRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }

    public class FigaroIslandRegion : TownRegion{
	public FigaroIslandRegion( XmlElement xml, Map map, Region parent) : base( xml, map, parent)
	{}
    }
}
