using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Regions {
    public class WildernessRegion : BaseRegion {
	//this is intended to demarcate areas where ranger skills should train faster than normal

	public TownRegion( XmlElement xml, Map map, Regtion parent ) : base(xml, map, parent)
	{
	}
    }
}
