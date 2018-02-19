using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Regions {
    public class CraftingRegion : BaseRegion {
	// this is intended to denote areas where crafting skills should gain faster than normal
	// e.g. minoc mines, etc.

	public CraftingRegtion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
	{}
    }
}
