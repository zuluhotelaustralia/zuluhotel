using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Regions {
    public class WildernessRegion : BaseRegion {
	//this is intended to demarcate areas where ranger skills should train faster than normal

	public SkillSpecificPrimaryFactor { get { return 0.5; } } //see DungeonRegion

	public override double GetSkillSpecificFactor(Skill skill){
	    if( skill.SkillName == SkillName.Tracking ||
		skill.SkillName == SkillName.AnimalTaming ||
		skill.SkillName == SkillName.Camping ||
		skill.SkillName == SkillName.Herding ) {
		return SkillSpecificPrimaryFactor;
	    }
	    else {
		return RegionalSkillGainPrimaryFactor;
	    }
	}
	
	public WildernessRegion( XmlElement xml, Map map, Regtion parent ) : base(xml, map, parent)
	{
	}
    }
}
