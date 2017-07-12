using System;
using System.Xml;
using Server;

namespace Server.Regions
{
    public class TownRegion : GuardedRegion
    {
	public TownRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
	{
	}

	public override double RegionalSkillGainPrimaryFactor { get { return 0.0002; } } 
	public override double RegionalSkillGainSecondaryFactor { get { return 1400.0; } }
	
    }
}
