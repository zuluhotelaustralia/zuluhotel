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
	
	public override double RegionalSkillGainFactor { get { return 3.0; } } //see BaseRegion or Misc/SkillCheck.cs --sith
    }
}
